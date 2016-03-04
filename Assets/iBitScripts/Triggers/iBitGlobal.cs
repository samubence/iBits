using UnityEngine;
using System.Collections;

public class iBitGlobal
{
	public static int TYPE_GAZE = 1;
	public static int TYPE_COLLISION = 2;
	public static int TYPE_TOUCH = 3;

	public static bool CanInteract(GameObject go, int type)
	{
		iBitEventListener listener = go.GetComponent<iBitEventListener> ();

		if (listener != null) 
		{
			return listener.CanInteract (type);
		}
		return false;
	}
}
