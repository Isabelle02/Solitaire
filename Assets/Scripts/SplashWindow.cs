using UnityEngine;

public class SplashWindow : MonoBehaviour
{
    [SerializeField] private Transform _cardStorageContainer;
    
    private void Awake()
    {
        var rightUpAnchor = Camera.main.Bounds().max;
        _cardStorageContainer.SetPositionXY(rightUpAnchor.x - 2, rightUpAnchor.y - 3);
        
        CardManager.AddCardStorage(_cardStorageContainer);
    }
}