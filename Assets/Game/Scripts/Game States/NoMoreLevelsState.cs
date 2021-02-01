using System;
using UnityEngine;
using UnityEngine.UI;

public class NoMoreLevelsState : BaseGameState
{
    [SerializeField] private Button m_button;

    public override void Enter()
    {
        base.Enter();

        Game.Container.gameObject.SetActive(false);

        m_button.onClick.AddListener(OnClick);
    }

    public override void Exit()
    {
        base.Exit();

        m_button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Main.ShowLastLevel();
    }
}