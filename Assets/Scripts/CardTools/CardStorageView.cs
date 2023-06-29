using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CardStorageView : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private GameObject _mainCardObject;
    [SerializeField] private HorizontalLayoutGroup _cardsLayout;
    [SerializeField] private List<Sprite> _cardSprites;

    private bool _captured;
    private List<Sprite> _cardSpritesLeft = new();
    private readonly List<CardView> _cards = new();

    public event Action<CardStorageView> Click;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
            _captured = Input.mousePosition.GetObject<CardStorageView>() == this;

        if (Input.GetMouseButtonUp(0) && _captured)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!mousePosition.IsInBounds(_boxCollider2D.bounds))
                return;
            
            _captured = false;
            Click?.Invoke(this);
        }
    }

    public async void AddCard(bool last)
    {
        using var _ = new BlockerSpace(LayerMask.NameToLayer("Blocker"));
        
        if (_cards.Count >= 3)
        {
            Pool.Release(_cards[0]);
            _cards.RemoveAt(0);
        }

        for (var i = 0; i < _cards.Count; i++) 
            _cards[i].transform.SetLocalPositionZ(-i - 1);

        var cardView = Pool.Get<CardView>();
        cardView.transform.SetParent(_cardsLayout.transform);
        cardView.transform.SetLocalPositionZ(-_cards.Count - 1);
        cardView.Hide();
        
        _cards.Add(cardView);
        _mainCardObject.SetActive(!last);
        
        var cardSprite = _cardSpritesLeft.Random();
        _cardSpritesLeft.Remove(cardSprite);
        
        var floatingCardView = Pool.Get<CardView>();
        floatingCardView.transform.SetParent(_boxCollider2D.transform, false);
        floatingCardView.transform.SetLocalPositionZ(-_cards.Count - 2);
        floatingCardView.Show();
        
        await floatingCardView.SpawnWithAnimation(_boxCollider2D.transform.position + Vector3.left * 3f, cardSprite);
        Pool.Release(floatingCardView);
        
        cardView.Spawn(cardSprite);
        cardView.Show();
    }

    public async void RevertCards()
    {
        using var _ = new BlockerSpace(LayerMask.NameToLayer("Blocker"));
        
        var tasks = new List<UniTask>();
        var floatingCards = new List<CardView>();

        for (var i = 0; i < _cards.Count; i++)
        {
            var card = _cards[i];
            card.Hide();
            
            var floatingCardView = Pool.Get<CardView>();
            floatingCardView.transform.SetPositionXY(card.transform.position);
            floatingCardView.transform.SetLocalPositionZ(-i - 2);
            floatingCardView.Show();
            floatingCards.Add(floatingCardView);
            tasks.Add(floatingCardView.RevertWithAnimation(_boxCollider2D.transform.position).AsAsyncUnitUniTask());
        }

        await UniTask.WhenAll(tasks);
        
        Dispose();
        
        foreach (var card in floatingCards)
            Pool.Release(card);
    }
    
    public void Dispose()
    {
        _cardSpritesLeft = new List<Sprite>(_cardSprites);

        _mainCardObject.SetActive(true);
        
        foreach (var card in _cards)
            Pool.Release(card);
        
        _cards.Clear();
    }
}