using UnityEngine;

public class BaseGameState : BaseCanvasState
{
    public ApplicationController App => ApplicationController.Instance;

    public MainController Main => MainController.Instance;

    public GameController Game => GameController.Instance;

    private bool m_active;
    public bool Active => m_active;

    public override void Enter()
    {
        base.Enter();

        m_active = true;
    }

    public override void Exit()
    {
        m_active = false;

        base.Exit();
    }
}