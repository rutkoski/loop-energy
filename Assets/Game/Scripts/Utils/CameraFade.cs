using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.XR;

public class CameraFade : Singleton<CameraFade>
{

    private Color m_fadeColor = Color.black;
    public Color fadeColor
    {
        get { return m_fadeColor; }
        set
        {
            if (m_fadeColor == value) return;

            m_fadeColor = value;
            
            SetDirty();
        }
    }

    private float m_fadeDuration = 0.3f;
    public float fadeDuration
    {
        get { return m_fadeDuration; }
        set
        {
            if (m_fadeDuration == value) return;

            m_fadeDuration = value;

            SetDirty();
        }
    }

    private GameObject m_canvasObject;
    private Canvas m_canvas;
    private Image m_image;
    private CanvasGroup m_canvasGroup;

    private bool m_dirty;
    
    public GameObject GetCanvas()
    {
        if (m_canvasObject && m_dirty)
        {
            Destroy(m_canvasObject);
            m_canvasObject = null;
        }
        
        if (m_canvasObject == null)
        {
            m_canvasObject = new GameObject();
            
            m_canvas = m_canvasObject.AddComponent<Canvas>();
            m_canvas.sortingOrder = 1000;
            m_canvas.gameObject.SetActive(false);
            m_canvas.transform.position = new Vector3(0, 0, 1);
            m_canvas.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);

            m_image = m_canvasObject.AddComponent<Image>();
            m_image.color = m_fadeColor;

            m_canvasGroup = m_canvasObject.AddComponent<CanvasGroup>();
            m_canvasGroup.alpha = 0;

            m_canvasObject.transform.SetParent(Camera.main.transform, false);
        }

        if (XRSettings.loadedDeviceName == "")
        {
            m_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
        else
        {
            m_canvas.renderMode = RenderMode.WorldSpace;
        }

        return m_canvasObject;
    }

    public IEnumerator FadeOut(float duration = -1f)
    {
        GetCanvas();

        m_canvasObject.SetActive(true);

        if (duration < 0)
        {
            duration = m_fadeDuration;
        }

        m_canvasGroup.alpha = 0;

        yield return m_canvasGroup.DOFade(1, duration).WaitForCompletion();
    }

    public IEnumerator FadeIn(float duration = -1f)
    {
        GetCanvas();

        m_canvasObject.SetActive(true);

        if (duration < 0)
        {
            duration = m_fadeDuration;
        }

        m_canvasGroup.alpha = 1;

        yield return m_canvasGroup.DOFade(0, duration).WaitForCompletion();
        
        m_canvasObject.SetActive(false);
    }

    private void SetDirty()
    {
        m_dirty = true;
    }
}
