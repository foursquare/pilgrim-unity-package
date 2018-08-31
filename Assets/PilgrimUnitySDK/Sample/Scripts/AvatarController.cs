using UnityEngine;

public class AvatarController : MonoBehaviour 
{

	private Animator animator;
	private Collider coll;
	private Rigidbody rbody;

	private bool shouldWave = true;

	private const float waveDuration = 2.5f;
	private const float jumpDuration = 1.5f;
	private float moveDuration;

	private bool isMoving;
	private float moveStartTime;

	void Awake()
	{
		animator = GetComponent<Animator>();
		coll = GetComponent<Collider>();
		rbody = GetComponent<Rigidbody>();
	}

	void Start() 
	{
		animator.SetTrigger("Idle");
	}
	
	void Update() 
	{
		if (shouldWave && !isMoving && Input.GetMouseButtonDown(0)) {
			Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			point.z = 0.0f;
			if (coll.bounds.Contains(point)) {
				shouldWave = false;
				isMoving = true;
				moveStartTime = Time.time;
				moveDuration = waveDuration;
				animator.SetTrigger("Wave");
			}
		}

		if (!shouldWave && !isMoving && Input.GetMouseButtonDown(0)) {
			Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			point.z = 0.0f;
			if (coll.bounds.Contains(point)) {
				shouldWave = true;
				isMoving = true;
				moveStartTime = Time.time;
				moveDuration = jumpDuration;
				animator.SetTrigger("Jump");
				rbody.AddForce(new Vector3(0.0f, 500.0f, 0.0f));
			}
		}

		if (isMoving && Time.time - moveStartTime > moveDuration) {
			isMoving = false;
		}
	}

}
