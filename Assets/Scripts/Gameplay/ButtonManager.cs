using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private bool isPressed { get; set; }

    public bool IsPressed
    {
        get => isPressed;
        set => isPressed = value;
    }
    void Awake()
    {
        isPressed = false;
    }
    public void Press()
    {
        IsPressed = true;
    }

    public void Release()
    {
        IsPressed = false;
    }
}
