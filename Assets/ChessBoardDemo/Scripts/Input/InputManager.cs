using System;
using System.Collections.Generic;
using ChessBoardDemo.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
    public event Action MarkerCreated;
    [SerializeField] private ObjectFactory _objectFactory;
    [SerializeField] private ARRaycastManager _arRaycastManager;
    [SerializeField] private Camera _arCamera;
    private IInput _input;

    private void Awake()
    {
#if UNITY_EDITOR
        _input = new MouseInput();
#endif
    }

    void Update()
    {
        if (_input.IsPressed())
            DrawRaycast();
    }

    private void DrawRaycast()
    {
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
        Ray ray = _arCamera.ScreenPointToRay(_input.GetInteractionPos());

        if (_arRaycastManager.Raycast(ray, hitResults))
        {
            foreach (var hit in hitResults)
            {
                _objectFactory.SpawnMarker(hit.pose.position);
                MarkerCreated?.Invoke();
            }
        }
        else
        {
            Debug.Log("Not hit");
            Debug.DrawRay(ray.origin, ray.direction * 100000, Color.red);
        }
    }
}