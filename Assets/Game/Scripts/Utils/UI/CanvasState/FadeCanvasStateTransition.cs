using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class FadeCanvasStateTransition : BaseCanvasStateTransition
{
    public float duration = 0.2f;

    public override void Init(BaseCanvasState state)
    {
        base.Init(state);
        
        DoTransition(state, false, true);
    }

    public override void Enter(BaseCanvasState state)
    {
        base.Enter(state);
        
        DoTransition(state, true, false);
    }

    public override void Exit(BaseCanvasState state)
    {
        base.Exit(state);
        
        DoTransition(state, false, false);
    }

    private void DoTransition(BaseCanvasState state, bool active, bool instant)
    {
        if (!state.canvas)
            return;

        if (instant)
        {
            if (state.canvasGroup) state.canvasGroup.alpha = active ? 1 : 0;
            if (state.canvas) state.canvas.SetActive(active);
            return;
        }

        if (active)
        {
            state.canvas.transform.SetAsLastSibling();
        }

        if (state.canvasGroup)
        {
            if (active)
            {
                state.canvas.SetActive(true);
            }

            state.canvasGroup.DOKill();
            
            state.canvasGroup.DOFade(active ? 1 : 0, duration).OnComplete(() =>
            {
                if (!active) state.canvas.SetActive(false);
            });
        }
        else if (state.canvas)
        {
            state.canvas.SetActive(active);
        }
    }
}
