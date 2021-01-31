using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFXSettings")]
public class SFXData : ScriptableObject
{

    public enum Type
    {
        ButtonDefault = 0,
        Positive = 100,
        Negative = 200,
        Win = 300,
        Applause = 340,
        ToggleMenu = 400,
        Capture = 500,
        Pop = 600,
    }

    public AudioClip buttonDefaultSound;
    public AudioClip positiveSound;
    public AudioClip negativeSound;
    public AudioClip winSound;
    public AudioClip applauseSound;
    public AudioClip captureSound;
    public AudioClip popSound;

    public AudioClip GetClip(Type type)
    {
        switch (type)
        {
            case Type.ButtonDefault:
                return buttonDefaultSound;
            case Type.Positive:
                return positiveSound;
            case Type.Negative:
                return negativeSound;
            case Type.Win:
                return winSound;
            case Type.Applause:
                return applauseSound;
            case Type.ToggleMenu:
                return buttonDefaultSound;
            case Type.Capture:
                return captureSound;
            case Type.Pop:
                return popSound;
        }

        return null;
    }
}
