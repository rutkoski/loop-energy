using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateTransition")]
public class StateTransition : ScriptableObject
{
    public float enterDelay;

    public virtual void Transition(State from, State to, Action transitionDone)
    {
        if (from != null && to != null && enterDelay > 0)
        {
            from.Exit();

            DOVirtual.DelayedCall(enterDelay, () =>
            {
                to.Enter();

                transitionDone.Invoke();
            });
        }
        else
        {
            if (from != null)
            {
                from.Exit();
            }

            if (to != null)
            {
                to.Enter();
            }

            transitionDone.Invoke();
        }
    }
}
