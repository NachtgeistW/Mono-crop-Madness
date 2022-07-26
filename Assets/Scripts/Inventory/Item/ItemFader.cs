using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Change colour back
    /// </summary>
    public void FadeIn()
    {
        Color targetColor = new(1, 1, 1, 1);
        spriteRenderer.DOColor(targetColor, Settings.fadeDuration);
    }

    /// <summary>
    /// Change colour to transparency
    /// </summary>
    public void FadeOut()
    {
        Color targetColor = new(1, 1, 1, Settings.fadeAlpha);
        spriteRenderer.DOColor(targetColor, Settings.fadeDuration);
    }
}
