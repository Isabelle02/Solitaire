using UnityEngine;

public static class ObjectExtensions
{
    public static void SetPositionXY(this Transform transform, Vector2 targetPosition)
    {
        var position = transform.position;
        position.x = targetPosition.x;
        position.y = targetPosition.y;
        transform.position = position;
    }
    
    public static void SetPositionXY(this Transform transform, float x, float y)
    {
        var position = transform.position;
        position.x = x;
        position.y = y;
        transform.position = position;
    }
    
    public static void SetLocalPositionZ(this Transform transform, float z)
    {
        var position = transform.localPosition;
        position.z = z;
        transform.localPosition = position;
    }
    
    public static void SetScaleXY(this Transform transform, float x, float y)
    {
        var scale = transform.localScale;
        scale.x = x;
        scale.y = y;
        transform.localScale = scale;
    }
    
    public static bool IsInBounds(this Vector3 target, Bounds bounds)
    {
        return target.x >= bounds.min.x && target.x <= bounds.max.x &&
               target.y >= bounds.min.y && target.y <= bounds.max.y;
    }
}