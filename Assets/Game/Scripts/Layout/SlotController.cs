using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour
{
    private bool m_focus;
    public bool Focus
    {
        get => m_focus;
        set => m_focus = value;
    }

    private PieceCoordinates m_coordinates;
    public PieceCoordinates Coordinates
    {
        get => m_coordinates;
        set => m_coordinates = value;
    }

    [SerializeField] private SpriteRenderer m_background;
    [SerializeField] private SpriteRenderer m_outline;

    private bool m_interactable;
    public bool Interactable
    {
        get => m_interactable;
        set => m_interactable = value;
    }

    private enum State
    {
        Init,
        Idle,
        FadeIn,
        FadeOut,
        FocusIn,
        FocusOut,
    }

    private State m_state = State.Init;

    private void Awake()
    {
        UpdateState();
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        GameController.Instance.OnGameEnded += Instance_OnGameEnded;
    }

    private void OnDisable()
    {
        if (GameController.Instance)
            GameController.Instance.OnGameEnded -= Instance_OnGameEnded;
    }

    private void Instance_OnGameEnded(object sender, System.EventArgs args)
    {
        m_interactable = false;

        m_state = State.FadeOut;
    }

    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        switch (m_state)
        {
            case State.Init:
                m_background.DOFade(0f, 0f).OnComplete(() =>
                {
                    m_state = State.FadeIn;
                    m_interactable = true;
                });
                m_outline.DOFade(0f, 0f);
                break;

            case State.FadeIn:
                m_background.DOFade(0.1f, 2f);
                m_outline.DOFade(0.1f, 2f);
                m_state = State.Idle;
                break;

            case State.FadeOut:
                m_background.DOFade(0.0f, 1f);
                m_outline.DOFade(0.0f, 1f);
                m_state = State.Idle;
                break;

            case State.FocusIn:
                m_background.DOFade(0.4f, 1f);
                m_outline.DOFade(0.4f, 1f);
                m_state = State.Idle;
                break;

            case State.FocusOut:
                m_background.DOFade(0.1f, 1f);
                m_outline.DOFade(0.1f, 1f);
                m_state = State.Idle;
                break;
        }
    }

    public void SetFocus(bool focus)
    {
        if (m_interactable)
        {
            m_focus = focus;

            m_state = focus ? State.FocusIn : State.FocusOut;
        }
    }
}
