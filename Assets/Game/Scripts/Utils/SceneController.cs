using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public bool autoFadeIn = true;

    protected static Dictionary<Scene, SceneController> controllers = new Dictionary<Scene, SceneController>();

    public static SceneController GetController(Scene scene)
    {
        if (!controllers.ContainsKey(scene))
        {
            return null;
        }

        return controllers[scene];
    }

    public void RegisterSceneController()
    {
        if (!controllers.ContainsKey(gameObject.scene))
        {
            controllers[gameObject.scene] = this;
        }
    }

    protected virtual void Awake()
    {
        RegisterSceneController();
    }

    public virtual IEnumerator TransitionIn()
    {
        return CameraFade.Instance.FadeIn();
    }

    public virtual IEnumerator TransitionOut()
    {
        return CameraFade.Instance.FadeOut();
    }

}
