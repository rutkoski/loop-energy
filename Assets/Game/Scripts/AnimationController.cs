using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static AnimationController m_instance;
    public static AnimationController Instance => m_instance;

    [SerializeField] private CanvasGroup m_fadeCanvasGroup;

    [SerializeField] private float m_fadeDuration = 1f;

    private void Awake()
    {
        if (m_instance) Destroy(gameObject);
        m_instance = this;
    }

    public void FadeOut(Action complete = null, bool instant = false)
    {
        m_fadeCanvasGroup.DOKill();

        m_fadeCanvasGroup.blocksRaycasts = true;

        m_fadeCanvasGroup
            .DOFade(1f, instant ? 0f : m_fadeDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                complete?.Invoke();
            });
    }

    public void FadeIn(Action complete = null, bool instant = false)
    {
        m_fadeCanvasGroup.DOKill();

        m_fadeCanvasGroup.blocksRaycasts = false;

        m_fadeCanvasGroup
            .DOFade(0f, instant ? 0f : m_fadeDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                complete?.Invoke();
            });
    }
}
