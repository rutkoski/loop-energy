using UnityEngine;

public class NoMoreLevelsState : BaseGameState
{
    public override void Enter()
    {
        base.Enter();

        Game.Container.gameObject.SetActive(false);
    }
}