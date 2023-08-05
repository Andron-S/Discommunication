using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerControl : MonoBehaviour
{
    protected PlayerInput PlayerInput;

    public virtual void Awake()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Player.Enable();
    }

    private void OnEnable()
    {
        PlayerInput.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
    }
}
