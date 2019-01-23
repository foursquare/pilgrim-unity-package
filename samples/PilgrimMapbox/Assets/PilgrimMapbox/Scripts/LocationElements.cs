using System.Collections;
using UnityEngine;

public class LocationElements : MonoBehaviour
{

    [SerializeField]
    private PlaceBubble _placeBubble;

    public PlaceBubble PlaceBubble { get { return _placeBubble; } }

    [SerializeField]
    private Transform _arrow;

    [SerializeField]
    private float _animDuration = 0.75f;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _initialCameraPosition;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private Vector3 _targetCameraPosition;

    private bool _isMoving;

    private float _time;

    void Update()
    {
        if (!_isMoving)
        {
            return;
        }

        _time += Time.deltaTime;

        transform.position = Vector3.Lerp(_initialPosition, _targetPosition, _time / _animDuration);
        transform.rotation = Quaternion.Slerp(_initialRotation, _targetRotation, _time / _animDuration);

        Camera.main.transform.position = Vector3.Lerp(_initialCameraPosition, _targetCameraPosition, _time / _animDuration);

        if (_time > _animDuration)
        {
            _isMoving = false;
        }
    }

    public float MoveToMap(float elevation)
    {
        Reset();

        _targetPosition = new Vector3(0.0f, 100.0f + elevation, 0.0f);
        _targetRotation = Quaternion.identity;
        _targetCameraPosition = Camera.main.transform.position;
        _targetCameraPosition.y += elevation;

        Camera.main.GetComponent<CameraDrag>().Center = _targetCameraPosition;

        _isMoving = true;

        return _animDuration;
    }

    public float MoveFromMap()
    {
        Reset();

        _targetPosition = new Vector3(-88.0f, 314.0f, -409.0f);
        _targetRotation = Quaternion.Euler(35.7f, 12.513f, 0.0f);
        _targetCameraPosition = new Vector3(-140.0f, 519.0f, -646.0f);

        _isMoving = true;

        return _animDuration;
    }

    public void ShowArrow()
    {
        _arrow.gameObject.SetActive(true);
    }

    public void HideArrow()
    {
        _arrow.gameObject.SetActive(false);
    }

    private void Reset()
    {
        _time = 0.0f;
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialCameraPosition = Camera.main.transform.position;
    }

}
