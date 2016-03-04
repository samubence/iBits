using UnityEngine;
using System.Collections;

public class iBitEvent_Push : MonoBehaviour, iBitEventListener {

	public bool interactGaze = true;
	public bool interactCollision = true;
	public bool interactTouch = true;

	public float force = 400;

	public void OnTrigger (Vector3 position)
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (rb != null) 
		{
			Vector3 direction = (transform.position - position).normalized;
			rb.AddForce(direction * force);
		}
	}

	public void OnOver ()
	{

	}

	public void OnOut ()
	{

	}

	public bool CanInteract (int type)
	{
		if (interactGaze && type == iBitGlobal.TYPE_GAZE)
			return true;
		if (interactCollision && type == iBitGlobal.TYPE_COLLISION)
			return true;
		if (interactTouch && type == iBitGlobal.TYPE_TOUCH)
			return true;
		return false;
	}
}
