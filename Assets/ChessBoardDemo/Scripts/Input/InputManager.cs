using System;
using ChessBoardDemo.Scripts;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InputManager : MonoBehaviour
{
  public event Action MarkerCreated;
  [SerializeField] private ObjectFactory _objectFactory;
  [SerializeField] private Camera _arCamera;
  private IInput _input;

  public LayerMask LayerMask;

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
    RaycastHit hit;

    Ray ray = _arCamera.ScreenPointToRay(_input.GetInteractionPos());
    if (Physics.Raycast(ray, out hit, 50f, LayerMask))
    {
      _objectFactory.SpawnMarker(hit.point);
      MarkerCreated?.Invoke();
      Debug.Log("hit");
    }
    else
    {
      Debug.Log("not hit");
    }
  }
}