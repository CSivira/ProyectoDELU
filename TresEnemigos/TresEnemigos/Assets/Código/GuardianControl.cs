using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianControl : MonoBehaviour {

	public int vida = 100;
	public int fuerza = 20;
	public float velocidad = 0.5f;
	public float distanciaMinima = 1;
	public float distanciaMaxima = 8;
	public float alturaAtaque = 0.5f;
	public Transform objetivo;

	GameObject jugador;
	Transform arma;
	//Transform cuerpo;

	Rigidbody2D rigido;
	Animator animador;

	Vector3 direccion;
	Vector3 inicio;
	Vector3 final;

	float distancia;
	float velocidadReal = 0;

	bool sentidoGuardia = false;

	void Start () {
		jugador = GameObject.Find ("Jugador");
		arma = this.gameObject.transform.GetChild(1);
		//cuerpo = this.gameObject.transform.GetChild(0);	
		animador = GetComponent<Animator> ();
		//rigido = GetComponent<Rigidbody2D> ();

		if (objetivo != null) {
			objetivo.parent = null;

			inicio = transform.position;
			final = objetivo.position;
		}

		if (transform.position.x < objetivo.position.x) {
			sentidoGuardia = true;
		} else {
			sentidoGuardia = false;
		}
	}

	void FixedUpdate () {
		velocidadReal = velocidad * Time.deltaTime;

		if (jugador != null) {
			direccion = transform.position - jugador.transform.position;
			distancia = direccion.magnitude;
		}

		if (jugador != null && vida >= 0) {

			if (distancia > distanciaMaxima) {
				if (objetivo != null) {

					transform.position = Vector3.MoveTowards (transform.position, objetivo.position, velocidadReal);
					Animacion ("Andando", true);
					Animacion ("Quieto", false);
					Animacion ("Golpeado", false);

					DetenerAtaque ();
					//rigido.isKinematic = false;
				} 

				if (sentidoGuardia == true) {
					if (objetivo.position == final) {
						MiradaGuardia (1f);
					} else {
						MiradaGuardia (-1f);
					}
				} else {
					if (objetivo.position == final) {
						MiradaGuardia (-1f);
					} else {
						MiradaGuardia (1f);
					}
				}

				if (transform.position == objetivo.position) {
					if (objetivo.position == inicio) {
						objetivo.position = final;
					} else {
						objetivo.position = inicio;
					}
				}

			} else {
				if (transform.position.x < jugador.transform.position.x) {
					MiradaGuardia (1f);
				} else if (transform.position.x > jugador.transform.position.x){
					MiradaGuardia (-1f);
				}

				if (distancia < distanciaMinima) {
					velocidadReal = 0f;

					Animacion ("Andando", false);
					Animacion ("Quieto", true);
					Animacion ("Golpeado", false);

					/*if (transform.position.y > jugador.transform.position.y + alturaAtaque) {
						Animacion ("Andando", true);
						Reposicionar (0.02f, -0.01f);
						DetenerAtaque ();
					}else if(transform.position.y < jugador.transform.position.y - alturaAtaque){
						Animacion ("Andando", true);
						Reposicionar (0.02f, 0.01f);
						DetenerAtaque ();
					} else {*/
						Animacion ("Andando", false);
						Ataque ();
					//}


					//rigido.isKinematic = true;
				} else {
					transform.position = Vector3.MoveTowards (transform.position, jugador.transform.position, velocidadReal);
					
					Animacion ("Andando", true);
					Animacion ("Quieto", false);
					Animacion ("Golpeado", false);

					DetenerAtaque ();
					//rigido.isKinematic = false;
				}
			}
				
		} else if (vida < 0) {
			Animacion ("Andando", false);
			Animacion ("Quieto", false);
			Animacion ("Golpeado", false);

			animador.Play ("GuardianMuerto");
			DetenerAtaque ();
			//rigido.isKinematic = true;

			objetivo.parent = gameObject.transform;

			Destroy (gameObject, 3f);
		}

	}

	//Funciones

	void MiradaGuardia(float signo){
		transform.localScale = new Vector3 (signo, 1f, 1f);
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

				animador.Play ("GuardianGolpeado");
				Animacion ("Atacando", false);
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
}
