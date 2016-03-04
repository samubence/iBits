using UnityEngine;
using System.Collections;

public class iBitTrigger_Gaze : MonoBehaviour 
{
	public float lookAtInterval = 1;
	public Material timerMaterial;
	public bool displayTimer = true;

	GameObject lookAtObject;
	float lookAtStarted;
	GameObject timerObject;
	MeshFilter timerMeshFilter;

	float timerAnimPct;

	void Start () 
	{
		lookAtStarted = -1;
		createTimer ();
	}

	void Update () 
	{		
		Ray gaze = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		bool canInteract = false;

		if (Physics.Raycast (gaze.origin, gaze.direction, out hit, 100)) 
		{						
			if (iBitGlobal.CanInteract(hit.collider.gameObject, iBitGlobal.TYPE_GAZE)) canInteract = true;
			if (lookAtObject != hit.collider.gameObject) 
			{				
				if (lookAtObject != null) 
				{
					iBitEventListener[] pListeners = lookAtObject.GetComponents<iBitEventListener> ();
					foreach (iBitEventListener l in pListeners) 
					{						
						l.OnOut ();
					}
				}

				lookAtObject = hit.collider.gameObject;
				lookAtStarted = Time.time;

				iBitEventListener[] listeners = lookAtObject.GetComponents<iBitEventListener> ();
				foreach (iBitEventListener l in listeners) 
				{			
					if (canInteract) 
					{
						l.OnOver ();
					}
				}					
			}
		}
		else 
		{
			if (lookAtObject != null) 
			{
				iBitEventListener[] listeners = lookAtObject.GetComponents<iBitEventListener> ();
				foreach (iBitEventListener l in listeners) 
				{					
					l.OnOut ();
				}
			}
			lookAtObject = null;
		}

		if (lookAtObject && lookAtStarted != -1 && canInteract) 
		{
			float d = (Time.time - lookAtStarted) / lookAtInterval;

			if (displayTimer) updateTimer (d);

			if (d >= 1) {
				lookAtStarted = -1;
				iBitEventListener[] listeners = lookAtObject.GetComponents<iBitEventListener> ();
				foreach (iBitEventListener l in listeners) 
				{
					l.OnTrigger (transform.position);
				}	
			}
		}
		else 
		{
			if (displayTimer)
				updateTimer (0);
		}
	}

	void createTimer()
	{
		timerObject = new GameObject ();
		MeshRenderer mr = timerObject.AddComponent<MeshRenderer> ();
		timerMeshFilter = timerObject.AddComponent<MeshFilter> ();
		mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
		if (timerMaterial == null) {
			Material mat = new Material (Shader.Find ("Diffuse"));
			mat.color = Color.black;
			mr.material = mat;
		} else {
			mr.material = timerMaterial;
		}
		updateTimer (0);
	}

	void updateTimer(float pct)
	{		
		if (pct > 1)
			pct = 1;
		if (pct < 0)
			pct = 0;

		timerAnimPct += ((pct == 0 ? 0f : 1f) - timerAnimPct) * 0.4f;
			
		Mesh mesh = new Mesh ();

		int res = 100;

		float r1Open = 0.08f;
		float r2Open = 0.1f;
		float r1Close = 0f;
		float r2Close = 0.03f;

		float step = Mathf.PI * 2 / (float)res;
		float numOpen = res * pct;
		float numClose = res;
		int n = (int)((timerAnimPct * numOpen) + (1 - timerAnimPct) * numClose);
		float r1 = timerAnimPct * r1Open + (1 - timerAnimPct) * r1Close;
		float r2 = timerAnimPct * r2Open + (1 - timerAnimPct) * r2Close;

		Vector3[] newVertices = new Vector3[n * 2];
		int[] newTriangles = new int[n * 3 * 2];

		for (int i = 0; i < n; i++) 
		{
			int index = i * 2;
			float a = -i * step + Mathf.PI / 2f;
			newVertices [index + 0] = new Vector3 (Mathf.Cos (a) * r1, Mathf.Sin (a) * r1, 0f);
			newVertices [index + 1] = new Vector3 (Mathf.Cos (a) * r2, Mathf.Sin (a) * r2, 0f);
		}
		for (int i = 0; i < n - 1; i++) 
		{
			int index = i * 3 * 2;
			newTriangles [index + 0] = i * 2 + 0;
			newTriangles [index + 1] = i * 2 + 1;
			newTriangles [index + 2] = (i + 1) * 2 + 0;

			newTriangles [index + 3] = (i + 1) * 2 + 0;
			newTriangles [index + 4] = i * 2 + 1;
			newTriangles [index + 5] = (i + 1) * 2 + 1;
		}

		mesh.vertices = newVertices;
		mesh.triangles = newTriangles;

		timerMeshFilter.mesh = mesh;

		//timerObject.transform.position = transform.position + (lookAtObject.transform.position - transform.position).normalized * 0.6f;
		/*
		 * TODO:
		 * 	draw this on onverlay mode
		 */
		timerObject.transform.position = transform.position + transform.forward * 2f;
		timerObject.transform.rotation = transform.rotation;


	}
}
