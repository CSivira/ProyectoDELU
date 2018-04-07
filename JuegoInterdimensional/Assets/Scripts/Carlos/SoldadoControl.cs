using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldadoControl : MonoBehaviour {

	public int vida = 0;
	public int fuerza = 0;
	public float velocidad = 0f;
	public float alturaAtaque = 0f;
	public float anchoAtaque = 0f;
	public float radioPersecusion = 5;
	public float radioAtaque = 1;

	GameObject jugador;
	Transform arma;
	Animator animador;
	Vector3 direccion;
	Vector3 posicionInicial;
	float distancia;
	float velocidadReal = 0;
	bool sentido = false;

	void Awake () {
		jugador = GameObject.Find ("Jugador");
		arma = this.gameObject.transform.GetChild(1);
		animador = GetComponent<Animator> ();

		//Capturando posicion inicial
		posicionInicial = transform.position;
	}
		
	void Update () {
		// Velocidad y dirección
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
			Destroy (gameObject,1f);
		}

		//En espera
		if (radioPersecusion < distancia) {

			//Volviendo a la posicion inicial
			if (transform.position != posicionInicial) {
				transform.position = Vector3.MoveTowards (transform.position, posicionInicial, velocidadReal);

				//Mirada sin persecusion
				if (transform.position.x < posicionInicial.x && !sentido) {
					Mirada ();
				} else if (transform.position.x > posicionInicial.x && sentido){
					Mirada ();
				}

			} else {
				velocidadReal = 0f;
			}

		//Persecusión
		} else {
			transform.position = Vector3.MoveTowards (transform.position, jugador.transform.position, velocidadReal);

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
				Reposicionar (0.02f, 0f);
			} else {
				Reposicionar (-0.02f, 0f);
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
