using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 50;
    [SerializeField] private float _maxSpeed = 15;
    [SerializeField] private float _drag = 0.98f;
    [SerializeField] private float _rotationAngle = 20;
    [SerializeField] private float _clutch = 1;
    [SerializeField] private float _brakingForce = 0.9f;
    [SerializeField] private float _rotationSpeed = 5;
    [SerializeField] private float _flipRotationSpeed = 30;

    public float currentSpeed;
    private bool _isUpsideDown = false;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _isBraking;

    private void Update()
    {
        _horizontalInput = Input.GetAxis(Directions.Horizontal.ToString());
        _verticalInput = Input.GetAxis(Directions.Vertical.ToString());
        _isBraking = Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        ApplyDrag();
        CheckUpsideDown();
    }

    private enum Directions
    {
        Horizontal,
        Vertical
    }

    private void Move()
    {
        if (!_isUpsideDown)
        {
            if (!_isBraking)
            {
                currentSpeed = Mathf.Clamp(currentSpeed + (_verticalInput * _moveSpeed * Time.fixedDeltaTime * _clutch), -_maxSpeed * 0.5f, _maxSpeed);
                transform.Translate(currentSpeed * Time.fixedDeltaTime * Vector3.forward);
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.fixedDeltaTime * _brakingForce);
                transform.Translate(currentSpeed * Time.fixedDeltaTime * Vector3.forward);
            }
        }
    }

    private void Rotate()
    {
        if (!_isUpsideDown)
        {
            float rotationSpeedFactor = currentSpeed / _maxSpeed;
            float currentRotationSpeed = _rotationSpeed * rotationSpeedFactor;
            float targetAngle = _horizontalInput * _rotationAngle;
            transform.rotation *= Quaternion.Euler(0, targetAngle * Time.fixedDeltaTime * currentRotationSpeed, 0);
        }
        else
        {
            if (_verticalInput > 0 && _horizontalInput < 0)
            {
                Quaternion targetRotation = Quaternion.Euler(180f, 0f, 90f);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _flipRotationSpeed * Time.deltaTime);
            }
            else if (_verticalInput > 0 && _horizontalInput > 0)
            {
                Quaternion targetRotation = Quaternion.Euler(180f, 0f, -90f);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _flipRotationSpeed * Time.deltaTime);
            }
        }
    }

    private void ApplyDrag()
    {
        if (!_isBraking)
        {
            currentSpeed *= _drag;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.fixedDeltaTime * _brakingForce);
        }
    }

    private void CheckUpsideDown()
    {
        Vector3 downDirection = Physics.gravity.normalized;
        Vector3 upDirection = -transform.up;

        float angle = Vector3.Angle(downDirection, upDirection);
        _isUpsideDown = angle > 80 || angle < -80;
    }
}