using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObstacle : MonoBehaviour {

	[SerializeField]
	[MinMaxRangeAttribute(0f, 5f)]
	private MinMaxRange period = new MinMaxRange(2f, 2f);

	[SerializeField]
	[MinMaxRangeAttribute(0f, 1f)]
	private MinMaxRange space = new MinMaxRange(0.2f, 0.3f);

	[SerializeField]
	[MinMaxRangeAttribute(-1f,1f)]
	private MinMaxRange height = new MinMaxRange (-0.4f, 0.4f);

	[SerializeField]
	private MoveObstacle obstacle;
	private float obsHeight;
	private float obsHalfHeight;

	[SerializeField]
	private float speed;
	[SerializeField]
	private float distanceToLive = 10f;

	// Use this for initialization
	void Awake () {
		BoxCollider2D collider = obstacle.GetComponent<BoxCollider2D> ();
		if(collider != null){
			obsHeight = collider.size.y;
			obsHalfHeight = obsHeight / 2.0f;
		}

		StartCoroutine (RepeatingSpawn());
	}
	
	// Update is called once per frame
	void Update () {
	}

	private IEnumerator RepeatingSpawn()
	{
		while (true) {
			//Debug.Log ("Starting spawn");
			// wait for timeout, create a delay between spawns
			float timeout = period.GetRandomValue();
			//Debug.Log (string.Format("Watining {0} seconds", timeout));
			yield return new WaitForSeconds (timeout);
			//Debug.Log ("Done");
			SpawnObstacle ();
		}
	}
	private void SpawnObstacle()
	{
		// spawn the obstacle with random spacing
		float spaceRand = space.GetRandomValue();
		float heightRand = height.GetRandomValue();
		float xpos = this.transform.position.x; // the starting point of the obstacles is where the spawner is placed
		float ypos = this.transform.position.y;

		// scale to obstacle size
		spaceRand *= obsHeight;
		heightRand *= obsHeight;

		float bottomEdge = ypos + heightRand/2f - spaceRand/2f ; 
		float topEdge = ypos + heightRand/2f + spaceRand/2f;

		Vector3 bottomSpawn = new Vector3(xpos, bottomEdge - obsHalfHeight);
		Vector3 topSpawn = new Vector3(xpos, topEdge + obsHalfHeight);

		// spawn obstacles, no need to rotate topObs, texture already rotated
		MoveObstacle bottomObs = Instantiate(obstacle, bottomSpawn, Quaternion.identity);
		MoveObstacle topObs = Instantiate(obstacle, topSpawn, Quaternion.identity);

		// set initial speed of moving obstacles
		bottomObs.speed = speed;
		topObs.speed = speed;

		// rotate texture of topObs, instead of whole object
		SpriteRenderer renderer = topObs.GetComponent<SpriteRenderer> ();
		if (renderer != null) {
			renderer.flipY = true;
		}

	}
}
