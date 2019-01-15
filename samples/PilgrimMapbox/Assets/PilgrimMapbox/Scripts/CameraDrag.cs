using UnityEngine;

public class CameraDrag : MonoBehaviour
{

    void Update()
    {
        if (Input.touches.Length > 0) {
            if (Input.touches[0].phase == TouchPhase.Moved) {
                 Vector2 delta = Input.touches[0].deltaPosition;
                 Vector3 move = new Vector3(-delta.x, 0.0f, -delta.y);
                 transform.position += move;
             }
         }
    }

}
