using UnityEngine;

public class AvatarController : MonoBehaviour 
{

	private Animator animator;
	private Collider coll;
	private Rigidbody rbody;

	private float waveOffset;
	private float waveTime;
	private float waveDuration = 2.5f;
	private bool didWave;
	private bool isWaving;

	private bool isJumping;
	private float jumpTime;
	private float jumpDuration = 1.5f;

	void Awake()
	{
		animator = GetComponent<Animator>();
		coll = GetComponent<Collider>();
		rbody = GetComponent<Rigidbody>();
	}

	void Start() 
	{
		waveOffset = Random.Range(2.0f, 4.0f);
	}
	
	void Update() 
	{
		if (!isWaving && !didWave && Time.timeSinceLevelLoad > waveOffset) {
			isWaving = true;
			waveTime = Time.time;
			animator.SetTrigger("Wave");
		}

		if (isWaving && Time.timeSinceLevelLoad - waveTime > waveDuration) {
			didWave = true;
			isWaving = false;
		}

		if (didWave && !isJumping && Input.GetMouseButtonDown(0)) {
			Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			point.z = 0.0f;
			if (coll.bounds.Contains(point)) {
				isJumping = true;
				jumpTime = Time.time;
				animator.SetTrigger("Jump");
				rbody.AddForce(new Vector3(0.0f, 500.0f, 0.0f));
			}
		}

		if (isJumping && Time.time - jumpTime > jumpDuration) {
			isJumping = false;
		}
	}

}
