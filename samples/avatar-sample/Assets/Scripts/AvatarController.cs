using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AvatarController : MonoBehaviour 
{

	public Transform flag;

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
		if (!MainSceneManager.isFirstRun) {
			animator.SetTrigger("Idle");
		}

		StartCoroutine(LoadTopCategoryIcon());
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

	private IEnumerator LoadTopCategoryIcon()
    {
		List<EventStore.Event> events = EventStore.GetEvents();
		EventStore.Event lastEvent = events.First();
		if (lastEvent != null) {
			Texture2D tex;
        	tex = new Texture2D(88, 88, TextureFormat.RGBA32, false);
			using (WWW www = new WWW(lastEvent.Category.Icon))
			{
				yield return www;
				www.LoadImageIntoTexture(tex);
				flag.GetComponent<Renderer>().material.SetTexture("_MainTex", tex);
			}
		}
    }

}
