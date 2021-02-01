using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationController : MonoBehaviour
{
    private static ApplicationController m_instance;
    public static ApplicationController Instance => m_instance;

    [SerializeField] private LevelData[] m_levels;
    public LevelData[] Levels => m_levels;

    private int m_currentLevel;
    public int CurrentLevel
    {
        get => m_currentLevel;
        set => m_currentLevel = value;
    }

    public int CurrentSavedLevel => PlayerPrefs.GetInt("current_level", 0);

    private void Awake()
    {
        if (m_instance) Destroy(gameObject);
        m_instance = this;
        DontDestroyOnLoad(this);

        m_currentLevel = CurrentSavedLevel;
    }

    private void OnDestroy()
    {
        m_instance = null;
    }

    public LevelData CurrentLevelData()
    {
        return m_currentLevel >= 0 && m_currentLevel < m_levels.Length ? m_levels[m_currentLevel] : null;
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt("current_level", m_currentLevel < m_levels.Length ? m_currentLevel : m_levels.Length - 1);
    }

    public void NextLevel()
    {
        m_currentLevel = Math.Min(m_currentLevel + 1, m_levels.Length);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Clear player prefs"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
