using UnityEngine;
using System.Collections;

public class iBitController_Gyro : MonoBehaviour
{
	public float speed = 500f;
	public float headTiltThreshold = -0.2f;
	public float rotationDamp = 0.4f;
	public float headHeight = 1;
	public GameObject myCamera;
	public GameObject myCameraRotate;
	public bool useHeadTiltToWalk = false;
	public bool useTouchToWalk = true;

	Rigidbody myBody;
	bool isWalking;
	bool isRotating;
	float movementPct;

	Quaternion referenceOrientation;
	Quaternion initialOrientation;
	Vector2 pTouch;

	void Start () 
	{		
		Input.gyro.enabled = true;
		referenceOrientation = Quaternion.Inverse (Quaternion.Euler(90, 0, 0));
		initialOrientation = transform.rotation;
		myBody = GetComponent<Rigidbody> ();
		isWalking = false;
		isRotating = false;
	}
		
	void FixedUpdate()
	{			
		
		if (Input.GetMouseButton (0)) {			
			if (useTouchToWalk) {
				isWalking = true;
			}
			if (isRotating == false) {
				pTouch = Input.mousePosition;
			}
			isRotating = true;
		} else {
			isWalking = isRotating = false;
			movementPct = 0;
		}

		Vector3 direction = GetRay ().direction;
		if (useHeadTiltToWalk && direction.y < headTiltThreshold)
			isWalking = true;

		if (isWalking) {
			Walk ();
		}
		if (isRotating) {
			Rotate ();
		}

		myCamera.transform.rotation = Quaternion.Slerp (transform.rotation, 
			initialOrientation * ConvertRotation (referenceOrientation * Input.gyro.attitude), rotationDamp);
							
		myCamera.transform.position = transform.position + new Vector3 (0f, headHeight, 0f);

	}

	void Rotate()
	{
		float s = 0.1f;
		Vector2 p = Input.mousePosition;
		
		float dx = 0;
		float dy = 0;

		dx = (p.x - pTouch.x) * s;
		dy = (p.y - pTouch.y) * s;

		pTouch = p;

		myCameraRotate.transform.Rotate (new Vector3 (0f, -dx, 0f));
		transform.Rotate (new Vector3 (dy, 0f, 0f));
	}

	void Walk()
	{
		Vector3 direction = GetRay ().direction;
		Vector3 movement = Time.deltaTime * direction * speed;
		myBody.AddForce (movement * movementPct);
		movementPct += (1 - movementPct) * 0.05f;
	}

	void UpdateReference()
	{
		referenceOrientation = Quaternion.Inverse(Input.gyro.attitude);
	}

	Ray GetRay()
	{
		return new Ray (myCamera.transform.position, myCamera.transform.forward);
	}

	static Quaternion ConvertRotation(Quaternion q)
	{
		return new Quaternion(q.x, q.y, -q.z, -q.w);	
	}
}
