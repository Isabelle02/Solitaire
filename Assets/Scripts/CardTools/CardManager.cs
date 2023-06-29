using UnityEngine;

public static class CardManager
{
    private const int AllCardsCount = 52;
    private static int _spawnedCardsCount;
    
    public static void AddCardStorage(Transform cardStorageContainer)
    {
        var cardStorageView = Pool.Get<CardStorageView>();
        cardStorageView.transform.SetParent(cardStorageContainer, false);
        cardStorageView.Dispose();
        cardStorageView.Click += OnCardStorageClick;
    }

    private static void OnCardStorageClick(CardStorageView cardStorageView)
    {
        if (_spawnedCardsCount < AllCardsCount)
        {
            _spawnedCardsCount++;
            cardStorageView.AddCard(_spawnedCardsCount == AllCardsCount);
        }
        else
        {
            _spawnedCardsCount = 0;
            cardStorageView.RevertCards();
        }
    }
}