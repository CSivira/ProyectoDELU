using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkimmerEnemyController : MonoBehaviour {

	public GameObject player;
	public GameObject weapon;
	public float speed = 2f;
	public float maxDistance = 10f;
	public float minDistance = 4f;
	public int count = 0;

	private Vector3 actualDistance;
	private float actualMagnitude;
	private float[] level;
	private int actualLevel;

	// Use this for initialization
	void Start () {
		weapon = GameObject.Find("Weapon");
	}

	void FixedUpdate() {
		level = GameObject.Find("GameController").GetComponent<GameController> ().Lane;
		actualLevel = player.GetComponent<PlayerController> ().actualLevel;
		actualDistance = transform.position - player.transform.position;
		actualMagnitude = actualDistance.magnitude;
		float fixedSpeed = speed * Time.deltaTime;

		if (actualMagnitude < maxDistance && actualMagnitude >= minDistance) {
			if (transform.position.y != level [actualLevel]) {
				Vector3 actual = new Vector3 (transform.position.x, level[actualLevel], transform.position.z);
				transform.position = Vector3.MoveTowards (transform.position, actual, fixedSpeed);
			}else {
				Vector3 actual = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
				transform.position = Vector3.MoveTowards (transform.position, actual, fixedSpeed);
			}
			Debug.Log (actualMagnitude);
			if (count == 7) {
				weapon.SendMessage ("ProjectileCreator");
				count = 0;
			} else
				count++;

		}else if (actualMagnitude < maxDistance && actualMagnitude < minDistance){
			
		}
	}
}
