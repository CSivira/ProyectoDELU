using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGuardian : MonoBehaviour {

	public float alturaAt = 0.5f;
	public float distanciaMax = 10f;
	public float distanciaMin = 0f;
	public float speed = 0.25f;
	public int vida = 50;
	public int fuerza = 10;

	GameObject jugador;
	GameObject target;
	GameObject arma;
	Vector3 inicio;
	Vector3 final;
	Vector3 direccion;
	Animator anim;
	float fixedSpeed = 0f;
	float distancia;
	bool mirada = false;

	void Start () {
		jugador = GameObject.Find ("Player");
		target = GameObject.Find ("Objetivo");
		arma = GameObject.Find ("Arma");
		anim = GetComponent<Animator> ();

		if (target != null) {
			target.transform.parent = null;
			inicio = transform.position;
			final = target.transform.position;
		}

		if (target.transform.position.x > transform.position.x) {
			mirada = true;
		} else {
			mirada = false;
		}
	}

	void FixedUpdate () {
		fixedSpeed = Time.deltaTime * speed;
		direccion = transform.position - jugador.transform.position;
		distancia = direccion.magnitude;

		if (vida <= 0) {
			anim.SetBool ("Muerte",true);
			Destroy (gameObject);
		}
			
			if (distancia < distanciaMax) {
				if (distancia > distanciaMin) {
					arma.SendMessage ("Ataque",false);

					if (transform.position.x < jugador.transform.position.x) {
						transform.localScale = new Vector3 (1f, 1f, 1f);
					} else {
						transform.localScale = new Vector3 (-1f, 1f, 1f);
					}

					transform.position = Vector3.MoveTowards (transform.position, jugador.transform.position, fixedSpeed);	
					anim.SetBool ("Andando",true);
					anim.SetBool ("Golpe",false);
				} else {
					if (transform.position.y < jugador.transform.position.y + alturaAt && transform.position.y > jugador.transform.position.y - alturaAt) {
						arma.SendMessage ("Ataque", true);

						transform.position = transform.position;
						anim.SetBool ("Daño",false);
						anim.SetBool ("Golpe", true);
						anim.SetBool ("Andando", false);
					}
				}
			} else {
				transform.position = Vector3.MoveTowards (transform.position, target.transform.position, fixedSpeed);
				anim.SetBool ("Andando",true);
				anim.SetBool ("Golpe",false);

				if (mirada == true) {
					if (target.transform.position == final) {
						transform.localScale = new Vector3 (1f, 1f, 1f);
					} else {
						transform.localScale = new Vector3 (-1f, 1f, 1f);
					}
				} else {
					if (target.transform.position == final) {
						transform.localScale = new Vector3 (-1f, 1f, 1f);
					} else {
						transform.localScale = new Vector3 (1f, 1f, 1f);
					}
				}

				if (transform.position == target.transform.position){
					if (target.transform.position == inicio) {
						target.transform.position = final;
					}else{
						target.transform.position = inicio;
					}
				}
			}		
		}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "armaJugador") {
			vida -= 5;
			anim.SetBool ("Daño",true);
		}
	}
}
