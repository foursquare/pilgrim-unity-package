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
    private Vector3 _targetCameraPosition;

    private bool _isMoving;

    private float _time;

    void Start()
    {
        _arrow.gameObject.SetActive(false);
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialCameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        if (!_isMoving)
        {
            return;
        }

        _time += Time.deltaTime;

        transform.position = Vector3.Lerp(_initialPosition, _targetPosition, _time / _animDuration);
        transform.rotation = Quaternion.Slerp(_initialRotation, Quaternion.identity, _time / _animDuration);

        Camera.main.transform.position = Vector3.Lerp(_initialCameraPosition, _targetCameraPosition, _time / _animDuration);

        if (_time > _animDuration)
        {
            _isMoving = false;
            _arrow.gameObject.SetActive(true);
        }
    }

    public float Move(float elevation)
    {
        _targetPosition = new Vector3(0.0f, 100.0f + elevation, 0.0f);

        _targetCameraPosition = Camera.main.transform.position;
        _targetCameraPosition.y += elevation;

        Camera.main.GetComponent<CameraDrag>().Center = _targetCameraPosition;

        _isMoving = true;

        return _animDuration;
    }

}
