using UnityEngine;

public class LevelSelectionState : BaseGameState
{
    public override void Enter()
    {
        base.Enter();

        Game.Container.gameObject.SetActive(false);
    }
}