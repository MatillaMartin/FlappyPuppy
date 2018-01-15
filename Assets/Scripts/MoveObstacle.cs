using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour {
	
	public float speed = 0;
	public float distanceToLive = 10f;

	private float currentDistance = 0f;
	
	// Update is called once per frame
	void Update () {
		float distance = speed * Time.deltaTime;
		this.transform.Translate (Vector3.left * distance);

		currentDistance += distance;
		if (currentDistance > distanceToLive) {
			Destroy (gameObject);
		}
	}
}
