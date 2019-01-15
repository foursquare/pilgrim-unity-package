using UnityEngine;

public class Bounce : MonoBehaviour
{

    private Vector3 _initialPosition;

    private bool _movingUp = true;

    void Start()
    {
        _initialPosition = transform.position;
    }

    void Update()
    {
        if (_movingUp) {
            var endPosition = _initialPosition + transform.up * 10.0f;
            transform.position = Vector3.Lerp(transform.position, endPosition, 3.0f * Time.deltaTime);
        } else {
            var endPosition = _initialPosition - transform.up * 10.0f;
            transform.position = Vector3.Lerp(transform.position, endPosition, 3.0f * Time.deltaTime);
        }

        if ((transform.position - _initialPosition).magnitude > 9.0f) {
            _movingUp = !_movingUp;
        }
    }

}
