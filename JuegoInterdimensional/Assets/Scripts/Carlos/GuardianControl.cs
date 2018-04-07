using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianControl : MonoBehaviour {

	public int vida = 100;
	public int fuerza = 20;
	public float velocidad = 0.5f;
	public float radioPersecusion = 1;
	public float radioAtaque = 8;
	public float alturaAtaque = 0.5f;
	public Transform objetivo;

	GameObject jugador;
	Transform arma;
	Animator animador;
	Vector3 direccion;
	Vector3 inicio;
	Vector3 final;
	float distancia;
	float velocidadReal = 0;
	bool sentido = false;

	void Awake () {
		jugador = GameObject.Find ("Jugador");
		arma = this.gameObject.transform.GetChild(1);	
		animador = GetComponent<Animator> ();

		if (objetivo != null) {
			objetivo.parent = null;

			inicio = transform.position;
			final = objetivo.position;
		}

	}

	void Update () {
		velocidadReal = velocidad;
		direccion = transform.position - jugador.transform.position;
		distancia = direccion.magnitude;

		//Ataque
		if (radioAtaque > distancia) {
			//Atacar
			velocidadReal = 0f;

		} else {
			//Dejar de atacar
		}

		//Muerte
		if (vida <= 0) {
			objetivo.parent = gameObject.transform;
			Destroy (gameObject,1f);
		}

		if (distancia > radioPersecusion) {
			//En guardia
			if (objetivo != null) {
				transform.position = Vector3.MoveTowards (transform.position, objetivo.position, velocidadReal);
			} 
				
			//Mirada en guardia
			if (transform.position.x < objetivo.position.x && !sentido) {
				Mirada ();
			} else if (transform.position.x > objetivo.position.x && sentido){
				Mirada ();
			}

			//Posicion del objetivo
			if (transform.position == objetivo.position) {
				if (objetivo.position == inicio) {
					objetivo.position = final;
				} else {
					objetivo.position = inicio;
				}
			}

			} else {
				//Persecusion
				if (distancia < radioPersecusion) {
					transform.position = Vector3.MoveTowards (transform.position, jugador.transform.position, velocidadReal);
				}
			
				//Mirada en persecusion
				if (transform.position.x < jugador.transform.position.x && !sentido) {
					Mirada ();
				} else if (transform.position.x > jugador.transform.position.x && sentido){
					Mirada ();
				}
			}

		if (velocidadReal != 0) {
			Animacion ("Andando", true);
		} else {
			Animacion ("Andando", false);
		}

	}

	//Funciones

	void Mirada(){
		sentido = !sentido;
		transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	void Animacion(string animacion, bool accion) {
		animador.SetBool (animacion,accion);
	}

	void OnTriggerEnter2D(Collider2D otro) {
		if (otro.gameObject.tag == "JugadorArma") {
			if (jugador != null) {
				vida -= jugador.GetComponent<JugadorControl> ().fuerza;
			}
		}
	}

	void OnCollisionStay2D(Collision2D otro) {
		if (otro.gameObject.tag == "Player") {
			Reposicionar (0f, 0f);
		} else {
			if (transform.position.y > otro.gameObject.transform.position.y) {
				Reposicionar (0f, 0.02f);
			} else {
				Reposicionar (0f, -0.02f);
			}

			if (transform.position.x > otro.gameObject.transform.position.x) {
				Reposicionar (0.01f, 0f);
			} else {
				Reposicionar (-0.01f, 0f);
			}
		}
	}

	void Ataque(){
		arma.SendMessage ("Ataque",true);
		Animacion ("Atacando", true);
	}

	void DetenerAtaque(){
		arma.SendMessage ("Ataque",false);
		Animacion ("Atacando", false);
	}

	void Reposicionar(float avanceX, float avanceY) {
		transform.position = new Vector3 (transform.position.x + avanceX, transform.position.y + avanceY, transform.position.z);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position,radioPersecusion);
		Gizmos.DrawWireSphere (transform.position,radioAtaque);
	}

}
