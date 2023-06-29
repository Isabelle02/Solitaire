// Copyright (c) 2012-2018 FuryLion Group. All Rights Reserved.

using UnityEditor.IMGUI.Controls;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
public class BoxCollider2DScaler : MonoBehaviour
{
    [SerializeField] protected PrimitiveBoundsHandle.Axes Axes;

    private BoxCollider2D _collider;

    private BoxCollider2D Collider
    {
        get
        {
            if (_collider == null)
                _collider = GetComponent<BoxCollider2D>();
            
            return _collider;
        }
    }
    
    private void Awake()
    {
        UpdateSize();
    }

    private void OnEnable()
    {
        UpdateSize();
    }

    private void UpdateSize()
    {
        if (Camera.main == null)
            return;

        var size = Camera.main.Bounds().size;

        var colliderSize = Collider.size;

        size.x = (Axes & PrimitiveBoundsHandle.Axes.X) != 0 ? size.x : colliderSize.x;
        size.y = (Axes & PrimitiveBoundsHandle.Axes.Y) != 0 ? size.y : colliderSize.y;

        Collider.size = size;
    }

    private void Update()
    {
        UpdateSize();
    }
}