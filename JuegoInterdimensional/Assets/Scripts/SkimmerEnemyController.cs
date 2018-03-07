using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkimmerEnemyController : MonoBehaviour {

	public Transform target;
	public Transform initialPosition;
	public GameObject player;
	public float speed = 2f;
	public float maxDistance = 10f;
	public float minDistance = 5f;

	private Vector3 actualDistance;
	private float actualMagnitude;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		actualDistance = transform.position - player.transform.position;
		actualMagnitude = actualDistance.magnitude;
		float fixedSpeed = speed * Time.deltaTime;
		Debug.Log (actualDistance.normalized);

		if (actualMagnitude < maxDistance && actualMagnitude > minDistance) {
			//Dispara
		}else if (actualMagnitude < maxDistance && actualMagnitude < minDistance){
			target.position = actualDistance;
			transform.position = Vector3.MoveTowards (transform.position,target.position,fixedSpeed);
		}
	}
}
