using UnityEngine;

public class CameraDrag : MonoBehaviour
{

    public bool DragEnabled { get; set; }

    public Vector3 Center { get; set; }

    private bool _isCentering;

    private float _time;

    private float _duration;

    private Vector3 _fromPosition;

    private Vector3 _dragOrigin;

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
#if UNITY_EDITOR
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
            Vector3 move = new Vector3(pos.x * 100.0f, 0, pos.y * 100.0f);

            transform.Translate(move, Space.World);
        }
#else
        else if (Input.touches.Length > 0 && DragEnabled)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Vector2 delta = Input.touches[0].deltaPosition;
                Vector3 move = new Vector3(-delta.x, 0.0f, -delta.y);
                transform.position += move;
            }
        }
#endif
    }

    public void Recenter()
    {
        _fromPosition = transform.position;
        var distance = (_fromPosition - Center).magnitude;
        if (Mathf.Approximately(distance, 0.0f))
        {
            return;
        }
        _duration = 100.0f / distance;
        _isCentering = true;
    }

}
