using UnityEngine;

public class GameEndedState : BaseGameState
{
    public override void Enter()
    {
        base.Enter();
    }

    private void Update()
    {
        if (Active)
        {
            if (Input.anyKeyDown)
            {
                App.NextLevel();

                Main.TryShowNextLevel();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

    }
}