using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    private static MainController m_instance;
    public static MainController Instance => m_instance;

    private StateMachine m_stateMachine;

    [SerializeField] private CanvasGroup m_ui;
    [SerializeField] private CanvasGroup m_levelSelection;
    [SerializeField] private Button m_levelSelectionClickOutsideButton;
    [SerializeField] private Button m_levelSelectionButton;
    [SerializeField] private Button m_prevButton;
    [SerializeField] private Button m_nextButton;
    [SerializeField] private Text m_levelText;
    private bool m_showLevelSelectionButton;

    private void Awake()
    {
        if (m_instance) Destroy(gameObject);
        m_instance = this;

        m_stateMachine = gameObject.AddComponent<StateMachine>();

        m_levelSelection.alpha = 0;
        m_levelSelection.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        m_levelSelectionButton.onClick.AddListener(LevelSelectionButton_OnClick);
        m_levelSelectionClickOutsideButton.onClick.AddListener(LevelSelectionClickOutsideButton_OnClick);

        m_prevButton.onClick.AddListener(PrevButton_OnClick);
        m_nextButton.onClick.AddListener(NextButton_OnClick);
    }

    private void OnDisable()
    {
        m_levelSelectionButton.onClick.RemoveListener(LevelSelectionButton_OnClick);
        m_levelSelectionClickOutsideButton.onClick.RemoveListener(LevelSelectionClickOutsideButton_OnClick);

        m_prevButton.onClick.RemoveListener(PrevButton_OnClick);
        m_nextButton.onClick.RemoveListener(NextButton_OnClick);
    }

    private void Start()
    {
        UpdateNavigation();

        AnimationController.Instance.FadeOut(() =>
        {
            TryShowCurrentLevel();
        }, true);
    }

    private void LevelSelectionButton_OnClick()
    {
        m_showLevelSelectionButton = !m_showLevelSelectionButton;

        m_levelSelection.blocksRaycasts = m_showLevelSelectionButton;

        m_levelSelection.DOFade(m_showLevelSelectionButton ? 1f : 0f, 1f).SetEase(Ease.OutQuad);
    }

    private void LevelSelectionClickOutsideButton_OnClick()
    {
        LevelSelectionButton_OnClick();
    }

    private void PrevButton_OnClick()
    {
        int currentLevel = ApplicationController.Instance.CurrentLevel;

        currentLevel = Math.Max(0, currentLevel - 1);

        ApplicationController.Instance.CurrentLevel = currentLevel;

        AnimationController.Instance.FadeOut(() =>
        {
            if (TryShowCurrentLevel())
            {
                AnimationController.Instance.FadeIn(() =>
                {
                    GameController.Instance.StartGame();
                });
            }
        });
    }

    private void NextButton_OnClick()
    {
        int currentLevel = ApplicationController.Instance.CurrentLevel;

        currentLevel = Math.Min(currentLevel + 1, ApplicationController.Instance.Levels.Length);

        ApplicationController.Instance.CurrentLevel = currentLevel;

        AnimationController.Instance.FadeOut(() =>
        {
            if (TryShowCurrentLevel())
            {
                AnimationController.Instance.FadeIn(() =>
                {
                    GameController.Instance.StartGame();
                });
            }
        });
    }

    public void UpdateNavigation()
    {
        int currentLevel = ApplicationController.Instance.CurrentLevel;
        int currentSavedLevel = ApplicationController.Instance.CurrentSavedLevel;
        int numLevels = ApplicationController.Instance.Levels.Length;

        m_prevButton.enabled = currentLevel > 0;
        m_nextButton.enabled = currentLevel < numLevels - 1 && currentLevel < currentSavedLevel;

        int level = Math.Min(currentLevel, numLevels - 1);

        m_levelText.text = $"Level {level}";
    }

    public void ToggleUI(bool active)
    {
        m_ui.DOFade(active ? 1f : 0f, 1f).SetEase(Ease.OutQuad);
    }

    public bool TryShowCurrentLevel()
    {
        LevelData levelData = ApplicationController.Instance.CurrentLevelData();

        UpdateNavigation();

        if (levelData)
        {
            GameController.Instance.LoadLevel(levelData);

            ToggleUI(true);

            ShowGameStarted();

            return true;
        }
        else
        {
            ToggleUI(false);

            ShowNoMoreLevels();

            return false;
        }
    }

    public void ShowLastLevel()
    {
        ApplicationController.Instance.CurrentLevel = ApplicationController.Instance.CurrentSavedLevel;

        TryShowCurrentLevel();
    }

    public void ShowGameStarted()
    {
        m_stateMachine.ChangeState<GameStartedState>();
    }

    public void ShowGameEnded()
    {
        m_stateMachine.ChangeState<GameEndedState>();
    }

    public void ShowNoMoreLevels()
    {
        m_stateMachine.ChangeState<NoMoreLevelsState>();
    }
}
