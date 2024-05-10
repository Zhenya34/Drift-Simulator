using UnityEngine;

public class Camera_Motion : MonoBehaviour
{
    [SerializeField] private Transform _frontPosition;
    [SerializeField] private Transform _rearPosition;
    [SerializeField] private float _delay = 2;

    private float _timer = 0f;
    private float _verticalInput;

    private void Update()
    {
        _verticalInput = Input.GetAxis("Vertical");
        _timer += Time.deltaTime;

        if (_verticalInput > 0)
        {
            _timer += Time.deltaTime;
            if (_timer >= _delay)
            {
                transform.SetPositionAndRotation(_rearPosition.position, _rearPosition.rotation);
                _timer = 0;
            }
        }
        else if (_verticalInput < 0)
        {
            _timer += Time.deltaTime;
            if (_timer >= _delay + 2)
            {
                transform.SetPositionAndRotation(_frontPosition.position, _frontPosition.rotation);
                _timer = 0;
            }
        }
    }
}