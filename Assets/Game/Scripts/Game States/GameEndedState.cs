using UnityEngine;
using UnityEngine.UI;

public class GameEndedState : BaseGameState
{
    [SerializeField] private Button m_button;

    public override void Enter()
    {
        base.Enter();

        App.NextLevel();

        if (App.CurrentLevel > App.CurrentSavedLevel)
        {
            App.SaveProgress();
        }

        m_button.onClick.AddListener(OnClick);
    }

    public override void Exit()
    {
        base.Exit();

        m_button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Main.TryShowCurrentLevel();
    }
}