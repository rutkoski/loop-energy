using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private static MainController m_instance;
    public static MainController Instance => m_instance;

    public List<LevelData> m_levels;

    private StateMachine m_stateMachine;

    private void Awake()
    {
        if (m_instance) Destroy(gameObject);
        m_instance = this;

        m_stateMachine = gameObject.AddComponent<StateMachine>();
    }

    private void Start()
    {
        TryShowNextLevel();
    }

    public void TryShowNextLevel()
    {
        LevelData levelData = ApplicationController.Instance.CurrentLevelData();

        if (levelData)
        {
            GameController.Instance.LoadLevel(levelData);

            ShowGameStarted();
        }
        else
        {
            ShowNoMoreLevels();
        }
    }

    public void ShowGameStarted()
    {
        m_stateMachine.ChangeState<GameStartedState>();
    }

    public void ShowGameEnded()
    {
        m_stateMachine.ChangeState<GameEndedState>();
    }

    public void ShowLevelSelection()
    {
        m_stateMachine.ChangeState<LevelSelectionState>();
    }

    public void ShowNoMoreLevels()
    {
        m_stateMachine.ChangeState<NoMoreLevelsState>();
    }
}
