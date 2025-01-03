using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour, IInput
{
    public bool IsPressed() =>
        Input.GetMouseButtonDown(0);

    public Vector3 GetInteractionPos() =>
        Input.mousePosition;
}