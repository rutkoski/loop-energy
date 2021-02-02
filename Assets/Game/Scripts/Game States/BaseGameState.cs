using UnityEngine;

public class BaseGameState : BaseCanvasState
{
    public ApplicationController App => ApplicationController.Instance;

    public MainController Main => MainController.Instance;

    public GameController Game => GameController.Instance;
}