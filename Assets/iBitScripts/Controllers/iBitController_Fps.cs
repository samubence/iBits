using UnityEngine;
using System.Collections;

public class iBitController_Fps : MonoBehaviour 
{
	public GameObject myCamera;
	public float speed = 1000;
	public float mouseSensitivity = 2;

	Rigidbody myBody;

	void Start () 
	{
		myBody = GetComponent<Rigidbody> ();	
	}

	void Update () {
		float dx = Input.GetAxis ("Mouse X") * mouseSensitivity;
		float dy = Input.GetAxis ("Mouse Y") * mouseSensitivity;

		transform.Rotate (new Vector3 (0f, dx, 0f));
		myCamera.transform.Rotate (new Vector3 (-dy, 0f, 0f));

		Vector3 direction = myCamera.transform.forward;
		Vector3 force = direction * speed * Time.deltaTime * Input.GetAxis ("Vertical");
		Vector3 up = myCamera.transform.up;
		Vector3 side = Vector3.Cross(up, direction);//Quaternion.AngleAxis (90, up) * up;
		force += side * speed * Time.deltaTime * Input.GetAxis ("Horizontal");
		myBody.AddForce(force);
	}
}
