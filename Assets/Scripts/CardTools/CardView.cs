using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _currentCardSprite;
    [SerializeField] private SpriteRenderer _backCardSprite;
    
    public void Spawn(Sprite sprite)
    {
        _currentCardSprite.sprite = sprite;
        transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
    }
    
    public async UniTask SpawnWithAnimation(Vector3 to, Sprite sprite)
    {
        _currentCardSprite.sprite = sprite;
        await DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 180), 0.5f, RotateMode.LocalAxisAdd))
            .Join(transform.DOMoveX(to.x, 0.5f)).AsyncWaitForCompletion();
    }

    public async UniTask RevertWithAnimation(Vector3 to)
    {
        await DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0), 0.4f, RotateMode.LocalAxisAdd))
            .Join(transform.DOMoveX(to.x, 0.4f)).AsyncWaitForCompletion();
    }

    public void Show()
    {
        _currentCardSprite.SetAlpha(1f);
        _backCardSprite.SetAlpha(1f);
    }

    public void Hide()
    {
        _currentCardSprite.SetAlpha(0f);
        _backCardSprite.SetAlpha(0f);
    }
}
