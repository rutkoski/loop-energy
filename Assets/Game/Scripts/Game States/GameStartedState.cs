using UnityEngine;

public class GameStartedState : BaseGameState
{
    public override void Enter()
    {
        base.Enter();

        Game.Container.gameObject.SetActive(true);

        Game.StartGame();
    }

    public override void Exit()
    {
        base.Exit();
    }
}