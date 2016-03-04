using UnityEngine;
using System.Collections;

public class iBitController_Cardboard : MonoBehaviour 
{
	public CardboardHead carboardHead;
	public GameObject cardboardMain;
	public float headTiltThreshold = -0.2f;
	public float force;
	public float headHeight = 2;

	Rigidbody rigidBody;

	void Start () 
	{
		rigidBody = GetComponent<Rigidbody> ();
		//rigidBody.drag = 5;
	}

	void Update()
	{
		Debug.DrawRay (carboardHead.transform.position, carboardHead.Gaze.direction * 1000);
	}

	void FixedUpdate () {
		if (rigidBody != null && carboardHead != null) 
		{
			Ray ray = carboardHead.Gaze;
			Vector3 direction = ray.direction;

			float tiltAngle = direction.y;
			if (tiltAngle < headTiltThreshold) 
			{				
				Vector3 movement = direction * force * Time.deltaTime;
				rigidBody.AddForce (movement);
			}
			cardboardMain.transform.position = transform.position + new Vector3(0f, headHeight, 0f);
		}
	}
}
