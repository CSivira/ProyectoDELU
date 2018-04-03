using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiradorControl : MonoBehaviour {

	public int vida = 50;
	public int fuerza = 5;
	public float velocidad = 1f;
	public int distanciaMinima = 1;
	public int distanciaMaxima = 12;

	GameObject jugador;
	Transform arma;
	//Transform cuerpo;

	Rigidbody2D rigido;
	Animator animador;

	Vector3 direccion;

	float distancia;
	float velocidadReal = 0;

	bool sentido = true;

	void Start () {
		jugador = GameObject.Find ("Jugador");
		arma = this.gameObject.transform.GetChild(1);
		//cuerpo = this.gameObject.transform.GetChild(0);	
		animador = GetComponent<Animator> ();
		rigido = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		velocidadReal = velocidad * Time.deltaTime;

		if (jugador != null) {
			direccion = transform.position - jugador.transform.position;
			distancia = direccion.magnitude;
		}

		if (jugador != null && vida >= 0) {

			if (transform.position.x < jugador.transform.position.x && !sentido) {
				Mirada ();
			} else if (transform.position.x > jugador.transform.position.x && sentido) {
				Mirada ();
			}

		
			if (distanciaMinima > distancia) {
				transform.position = Vector3.MoveTowards (transform.position, jugador.transform.position, velocidadReal * 0f);

				Animacion ("Quieto", true);
				Animacion ("Andando", false);
				Animacion ("Golpeado", false);

				rigido.isKinematic = true;

				Ataque ();

			} else {
				transform.position = Vector3.MoveTowards (transform.position, jugador.transform.position, velocidadReal);

				Animacion ("Quieto", false);
				Animacion ("Andando", true); 
				Animacion ("Golpeado", false);

				rigido.isKinematic = false;

				DetenerAtaque ();
			}

		} else if (vida < 0) {
			Animacion ("Golpeado", false);
			Animacion ("Quieto", false);
			Animacion ("Andando", false);

			rigido.isKinematic = true;
			DetenerAtaque();

			Animacion ("Muerto", true);
			Destroy (gameObject, 3f);
		}
	}

	void Mirada(){
		sentido = !sentido;
		transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	void Animacion(string animacion, bool accion) {
		animador.SetBool (animacion,accion);

		/*if (accion) {
			Debug.Log (animacion);
		}*/
	}

	void OnTriggerEnter2D(Collider2D otro) {
		if (otro.gameObject.tag == "JugadorArma") {
			if (jugador != null) {
				vida -= jugador.GetComponent<JugadorControl> ().fuerza;

				if (animador.GetBool ("Muerto") != true) {
					animador.Play ("TiradorGolpeado");
				} else {
					animador.Play ("TiradorMuerto");
				}
				Animacion ("Atacando", false);
			}
		}
	}

	void OnCollisionStay2D(Collision2D otro) {
		if (otro.gameObject.tag == "Player") {
			Reposicionar (0f, 0f);
		} else {
			if (transform.position.y > otro.gameObject.transform.position.y) {
				Reposicionar (0f, 0.01f);
			} else {
				Reposicionar (0f, -0.01f);
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

}
