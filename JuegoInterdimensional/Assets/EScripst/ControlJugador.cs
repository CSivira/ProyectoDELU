using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour {

	Vector2 move;
	GameObject arma;
	Rigidbody2D rb;
	float speed = 200f;
	float fixedSpeed = 0f;
	public int vida = 100;
	public int armaJugador = 15;


	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		arma = GameObject.Find ("Arma");
	}
	

	void Update () {
		fixedSpeed = Time.deltaTime * speed;

		move = new Vector2 (Input.GetAxis("Horizontal") * fixedSpeed, Input.GetAxis("Vertical") * 	fixedSpeed);
		rb.velocity = move;


		if (vida < 0) {
			Debug.Log("Jugador muerto");
		}
	}

	void Daño(int fuerza) {
		vida -= fuerza;
	}
}
