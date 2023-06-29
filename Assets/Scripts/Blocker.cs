using System;
using UnityEngine;

public class Blocker : MonoBehaviour
{
    public void Show(int layer)
    {
        gameObject.layer = layer;
        gameObject.SetActive(true);
        
        var targetCamera = Camera.main;
        if (targetCamera == null)
            return;

        var position = targetCamera.transform.position;
        position.z += targetCamera.nearClipPlane + 1.0f;
        transform.position = position;
    }

    public void Hide()
    {
        Pool.Release(this);
    }
}

public struct BlockerSpace : IDisposable
{
    private readonly Blocker _blocker;
    
    public BlockerSpace(int layer)
    {
        _blocker = Pool.Get<Blocker>();
        _blocker.Show(layer);
    }
    
    void IDisposable.Dispose()
    {
        _blocker.Hide();
    }
}