using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldadoControl : MonoBehaviour {

	public int vida = 50;
	public int fuerza = 5;
	public float velocidad = 1f;
	public int distanciaMinima = 1;
	public int distanciaMaxima = 8;
	public float alturaAtaque = 0.5f;

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
		//rigido = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		velocidadReal = velocidad * Time.deltaTime;
		//Debug.Log ("velocidadReal");

		if (jugador != null) {
			direccion = transform.position - jugador.transform.position;
			distancia = direccion.magnitude;
		}

		if (jugador != null && vida >= 0) {

			if (transform.position.x < jugador.transform.position.x && !sentido) {
				Mirada ();
			} else if (transform.position.x > jugador.transform.position.x && sentido){
				Mirada ();
			}


			if (distanciaMinima > distancia) {
				transform.position = Vector3.MoveTowards (transform.position, jugador.transform.position, velocidadReal * 0f);

				Animacion ("Andando", false);
				Animacion ("Quieto", true);
				Animacion ("Golpeado", false);

				//rigido.isKinematic = true;

				/*if (transform.position.y > jugador.transform.position.y + alturaAtaque) {
					Animacion ("Andando", true);
					Reposicionar (0.02f, -0.01f);
					DetenerAtaque ();
				} else if(transform.position.y < jugador.transform.position.y - alturaAtaque){
					Animacion ("Andando", true);
					Reposicionar (0.02f, 0.01f);
					DetenerAtaque ();
				} else {*/
					Animacion ("Andando", false);
					Ataque ();
				//}

			} else {
				transform.position = Vector3.MoveTowards (transform.position, jugador.transform.position, velocidadReal);

				//rigido.isKinematic = false;

				Animacion ("Quieto", false);
				Animacion ("Andando", true);
				Animacion ("Golpeado", false);

				DetenerAtaque ();
			}
		} else if (vida < 0) {
			Animacion ("Muerto", true);
			Animacion ("Golpeado", false);

			//rigido.isKinematic = true;

			DetenerAtaque ();
			Destroy (gameObject,3f);
		}
			
	}

	//Funciones

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

				Animacion ("Golpeado", true);
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

		if (otro.gameObject.tag == "SoldadoCuerpo") {
			velocidadReal = 0f;
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
