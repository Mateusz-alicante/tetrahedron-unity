using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float _zoomSensitivity = 0.1f;

    private float _rotationY;
    private float _rotationX;
    
    private Vector2 _delta;
    private Vector2 _deltaZoom;
    private bool _rotateCenter;
    private bool _rotateLast;
    
    private Vector3 _around;

    [SerializeField]
    private float _distanceFromTarget = 3.0f;

    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;

    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField]
    private Vector2 _rotationXMinMax = new Vector2(-40, 40);

    public Vector3 center;
    public Vector3 last;

    public void onLook(InputAction.CallbackContext context)
     {
         _delta = context.ReadValue<Vector2>();
     }
    
    public void onZoom(InputAction.CallbackContext context)
    {
        _deltaZoom = context.ReadValue<Vector2>();
    }

     public void onRotateCenter(InputAction.CallbackContext context)
     {
         _rotateCenter = context.started || context.performed;
         _around = center;
     }
     
     public void onRotateLast(InputAction.CallbackContext context)
     {
         _rotateLast = context.started || context.performed;
         _around = last;
     }

    void Update()
    {

        if (_deltaZoom.y > 0)
        {
            _distanceFromTarget += 50 * Time.deltaTime;
        } else if (_deltaZoom.y < 0)
        {
            _distanceFromTarget -= 50 * Time.deltaTime;
        }

        if (_distanceFromTarget < 1)
        {
            _distanceFromTarget = 1;
        }

        float mouseX = _delta.x;
        float mouseY = -_delta.y;

        if (_rotateCenter || _rotateLast)
        {
            _rotationY += mouseX;
            _rotationX += mouseY;
        }

        // Apply clamping for x rotation 
        _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

        Vector3 nextRotation = new Vector3(_rotationX, _rotationY);

        // Apply damping between rotation changes
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
        transform.localEulerAngles = _currentRotation;

        // Substract forward vector of the GameObject to point its forward vector to the target
        transform.position = _around - transform.forward * _distanceFromTarget;
    }
}
