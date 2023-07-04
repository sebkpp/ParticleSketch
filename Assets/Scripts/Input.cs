using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Sketch.Input
{
    public class Input : MonoBehaviour
{
    [SerializeField] private InputActionProperty drawStartedInput;
    [SerializeField] private InputActionProperty drawingInput;

    [SerializeField] private UnityEvent drawingStarted;
    [SerializeField] private UnityEvent drawingStopped;

    [SerializeField] private UnityEvent<Vector3> drawing;

    private void OnEnable()
    {
        drawStartedInput.action.started += OnDrawStarted;
        drawStartedInput.action.canceled += OnDrawStopped;
        drawingInput.action.performed += OnDrawing;
    }

    private void OnDisable()
    {
        drawStartedInput.action.started -= OnDrawStarted;
        drawStartedInput.action.canceled -= OnDrawStopped;
        drawingInput.action.performed -= OnDrawing;
    }

    private void OnDrawStarted(InputAction.CallbackContext obj)
    {
        drawingStarted.Invoke();
    }

    private void OnDrawing(InputAction.CallbackContext obj)
    {
        Vector3 position = obj.ReadValue<Vector3>();
        drawing.Invoke(position);
    }
    
    private void OnDrawStopped(InputAction.CallbackContext obj)
    {
        drawingStopped.Invoke();
    }
}
}