using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	public float speed = 5f;

	private GameObject player;
	private Rigidbody2D rb2d;
	private Vector2 direction;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Player");
		direction = player.transform.position;
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb2d.velocity = new Vector3 (1f,1f,1f) * speed;
	}
}
