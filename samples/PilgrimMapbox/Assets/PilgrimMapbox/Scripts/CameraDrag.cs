using UnityEngine;

public class CameraDrag : MonoBehaviour
{

    public bool DragEnabled { get; set; }

    public Vector3 Center { get; set; }

    private bool _isCentering;

    private float _time;

    private float _duration;

    private Vector3 _fromPosition;

    void Start()
    {
        DragEnabled = false;
    }

    void Update()
    {
        if (_isCentering)
        {
            _time += Time.deltaTime;
            transform.position = Vector3.Lerp(_fromPosition, Center, _time / _duration);

            if (_time > _duration)
            {
                _isCentering = false;
                _time = 0.0f;
            }
        }
        else if (Input.touches.Length > 0 && DragEnabled)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Vector2 delta = Input.touches[0].deltaPosition;
                Vector3 move = new Vector3(-delta.x, 0.0f, -delta.y);
                transform.position += move;
            }
        }
    }

    public void Recenter()
    {
        _fromPosition = transform.position;
        var distance = (_fromPosition - Center).magnitude;
        _duration = 100.0f / distance;
        _isCentering = true;
    }

}
