using UnityEngine;
using System.Collections;

public interface iBitEventListener
{
	void OnTrigger (Vector3 position);
	void OnOver ();
	void OnOut ();
	bool CanInteract(int type);
}
