using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public UnityEvent Interact;
    private Vector2 mousePosition;

    public void OnUse()
    {
        Interact.Invoke();
    }

    public void OnClick(InputValue button)
    {

    }

    public void SwapBack()
    {

    }

    public void OnCursorMove(InputValue value)
    {
        mousePosition = value.Get<Vector2>();
    }
}
