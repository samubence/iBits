using UnityEngine;
using System.Collections;

public class iBitEvent_Highlight : MonoBehaviour, iBitEventListener
{
	public bool interactGaze = true;
	public bool interactCollision = true;
	public bool interactTouch = true;

	public Material matHightlight;
	Material matOrig;
	Renderer renderer;

	void Start()
	{
		renderer = GetComponent<Renderer> ();
		if (renderer != null)
		{			
			matOrig = renderer.material;
		}
		if (matHightlight == null) 
		{
			matHightlight = new Material (Shader.Find ("Diffuse"));
			matHightlight.color = Color.cyan;
		}
	}
	void iBitEventListener.OnTrigger (Vector3 position)
	{

	}
	void iBitEventListener.OnOver ()
	{		
		if (renderer != null) 
		{
			renderer.material = matHightlight;
		}
	}
	void iBitEventListener.OnOut ()
	{
		if (renderer != null) 
		{
			renderer.material = matOrig;
		}
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
