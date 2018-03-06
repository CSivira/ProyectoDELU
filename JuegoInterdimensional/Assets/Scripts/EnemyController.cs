using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	public Transform target;
	public Transform initialPosition;
	public GameObject player;
	public float speed = 0.15f;

	private Vector3 start, end, actualDistance;
	private float maxDistance = 10f;
	private float minDistance = 2.5f;
	private float actualMagnitude;
	private float aux;

	// Use this for initialization
	void Start () {
		if (target != null) {
			target.parent = null;
			start = initialPosition.position;
			aux = initialPosition.position.y;
			end = target.position;
		}
	}

	void FixedUpdate() {

		actualDistance = transform.position - player.transform.position;
		actualMagnitude = actualDistance.magnitude;
		float fixedSpeed = speed * Time.deltaTime;
		Debug.Log (actualMagnitude);

		if (actualMagnitude < maxDistance && actualMagnitude > minDistance) {
			start = new Vector3 (initialPosition.position.x, aux, transform.position.z);
			end = target.position;
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, fixedSpeed);
		}else {
			if (target != null) {
				transform.position = Vector3.MoveTowards (transform.position, target.position, fixedSpeed);
			}

			if (transform.position == target.position) {
				if (target.position == start) {
					target.position = end;
				} else if (target.position == end) {
					target.position = start;
				}
			}
		}
	}
}
