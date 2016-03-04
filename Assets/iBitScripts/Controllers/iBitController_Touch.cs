using UnityEngine;
using System.Collections;

public class iBitController_Touch : MonoBehaviour {

	public GameObject myCamera;
	public float speed = 1000;
	public float mouseSensitivity = 2;
	public float headHeight = 1;

	Rigidbody myBody;
	float walkWeight;

	void Start () 
	{
		myBody = GetComponent<Rigidbody> ();	
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) {
		}
		else if (Input.GetMouseButton (0)) 
		{
			float dx = Input.GetAxis ("Mouse X") * mouseSensitivity / (float)Screen.width;
			float dy = Input.GetAxis ("Mouse Y") * mouseSensitivity / (float)Screen.height;

			transform.Rotate (new Vector3 (0f, -dx, 0f));
			myCamera.transform.Rotate (new Vector3 (dy, 0f, 0f));
			walkWeight += (1f - walkWeight) * 1f * Time.deltaTime;
		}
		else
		{
			walkWeight = 0;
		}
			
		Vector3 direction = myCamera.transform.forward;

		Vector3 force = direction * speed * Time.deltaTime * Mathf.Pow(walkWeight, 4f);
		myCamera.transform.position = transform.position + new Vector3 (0f, headHeight, 0f);
		myBody.AddForce(force);
	}
}
