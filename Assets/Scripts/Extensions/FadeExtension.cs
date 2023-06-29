using UnityEngine;

public static class FadeExtension
{
    public static void SetAlpha(this SpriteRenderer image, float value)
    {
        var color = image.color;
        color.a = value;
        image.color = color;
    }
}