using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkimmerEnemyController : MonoBehaviour {

	public Transform target;
	public GameObject player;
	public float speed = 2f;
	public float maxDistance = 10f;
	public float minDistance = 4f;
	public GameObject projectile;

	private Vector3 actualDistance;
	private float actualMagnitude;
	private Rigidbody2D rb2d;
	private float burst = 5f;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		projectile = GameObject.Find ("Projectile");
		target.parent = null;
	}

	void FixedUpdate() {
		actualDistance = transform.position - player.transform.position;
		actualMagnitude = actualDistance.magnitude;
		float fixedSpeed = speed * Time.deltaTime;

		if (actualMagnitude < maxDistance && actualMagnitude > minDistance) {
			InvokeRepeating("CreateProjectile",0f,burst);
		}else if (actualMagnitude < maxDistance && actualMagnitude < minDistance){
			CancelInvoke ("CreateProjectile");
			target.position = actualDistance;
			transform.position = Vector3.MoveTowards (transform.position,target.position,fixedSpeed);
			Debug.Log (actualMagnitude);
		}
	}

	void CreateProjectile() {
		Instantiate (projectile,transform.position,Quaternion.identity);
	}
}
