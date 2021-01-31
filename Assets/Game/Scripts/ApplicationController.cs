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

    private void Awake()
    {
        if (m_instance) Destroy(gameObject);
        m_instance = this;
        DontDestroyOnLoad(this);

        PlayerPrefs.DeleteAll();
    }

    private void OnDestroy()
    {
        m_instance = null;
    }

    public LevelData CurrentLevelData()
    {
        int currentLevel = CurrentLevel();

        return currentLevel >= 0 && currentLevel < m_levels.Length ? m_levels[currentLevel] : null;
    }

    public int CurrentLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("current_level", 0);

        return currentLevel < m_levels.Length ? currentLevel : -1;
    }

    public void NextLevel()
    {
        int currentLevel = CurrentLevel();

        currentLevel = Math.Min(currentLevel + 1, m_levels.Length);

        PlayerPrefs.SetInt("current_level", currentLevel);
    }
}
