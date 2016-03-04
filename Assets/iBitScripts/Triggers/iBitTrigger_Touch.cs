using UnityEngine;
using System.Collections;

public class iBitTrigger_Touch : MonoBehaviour 
{	
	Camera myCamera;
	float touchStarted;
	float touchInterval = 0.2f;

	void Start () {
		myCamera = GetComponent<Camera> ();
		touchStarted = -1;
	}

	void Update () 
	{		
		if (Input.GetMouseButtonDown (0)) {
			touchStarted = Time.time;
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (Time.time - touchStarted < touchInterval) {
				
				RaycastHit hit;
				Ray ray = myCamera.ScreenPointToRay (Input.mousePosition);//new Ray (transform.position, transform.forward);
				if (Physics.Raycast (ray.origin, ray.direction, out hit, 100)) {
					iBitEventListener[] listeners = hit.collider.GetComponents<iBitEventListener> ();
					foreach (iBitEventListener l in listeners) {								
						if (iBitGlobal.CanInteract (hit.collider.gameObject, iBitGlobal.TYPE_TOUCH)) {
							l.OnTrigger (transform.position);				
						}
					}
				}
			}
		}
	}
}
