using UnityEngine;
using System.Collections;

public class iBitBehaviour_Follow : MonoBehaviour {

	public GameObject objectToFollow;
	Vector3 offset;

	void Start () {
		offset = transform.position - objectToFollow.transform.position;
	}

	void Update () {
		transform.position += ((objectToFollow.transform.position + offset) - transform.position) * 5f * Time.deltaTime;
	}
}
