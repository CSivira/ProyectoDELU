using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorControl : MonoBehaviour {

	public int vida = 100;
	public int fuerza = 5;
	public int velocidad = 100;

	GameObject guardian;
	GameObject tirador;
	GameObject soldado;
	GameObject cuerpo;
	Rigidbody2D rigido;
	Animator animador;
	Vector2 movimiento;
	float velocidadReal;
	bool sentido = true;

	void Start () {
		cuerpo = GameObject.FindGameObjectWithTag ("JugadorCuerpo");
		guardian = GameObject.Find ("Guardian");
		soldado = GameObject.Find ("Soldado");
		tirador = GameObject.Find ("Tirador");
		rigido = GetComponent<Rigidbody2D> ();
		animador = cuerpo.GetComponent<Animator> ();
	}

	void FixedUpdate () {
		//Movimiento
		velocidadReal = velocidad;
		movimiento = new Vector2 (Input.GetAxis("Horizontal") * velocidadReal,Input.GetAxis("Vertical") * (velocidadReal / 1.5f));
		rigido.velocity = movimiento;

		if (Input.GetAxis("Horizontal") != 0) {
			Animacion ("Andando", true);
		} else {
			Animacion ("Andando", false);
		}

		if (vida < 0) {
			Debug.Log ("Destruir jugador");
			vida = 100;
			SceneManager.LoadScene ("TresEnemigos");
		}

		if (Input.GetAxis ("Horizontal") > 0 && !sentido) {
			Mirada ();
		} else if (Input.GetAxis ("Horizontal") < 0 && sentido) {
			Mirada ();
		}

		//Ataque
		if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space)){
			Ataque ();
		}

		if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space)){
			DetenerAtaque ();
		}
	}

	void Animacion(string animacion, bool accion) {
		animador.SetBool (animacion,accion);
	}

	void Ataque(){
		Animacion ("Ataque", true);
	}

	void DetenerAtaque(){
		Animacion ("Ataque", false);
	}

	void Mirada(){
		sentido = !sentido;
		this.transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	void OnTriggerEnter2D(Collider2D otro) {
		if(soldado != null && tirador != null && guardian != null){
			if (otro.gameObject.tag == "SoldadoArma") {
				int fuerza = soldado.GetComponent<SoldadoControl> ().fuerza;
				vida -= fuerza;
			} 

			if (otro.gameObject.tag == "Proyectil") {
				int fuerza = tirador.GetComponent<TiradorControl> ().fuerza;
				vida -= fuerza;
			} 

			if (otro.gameObject.tag == "GuardianArma") {
				int fuerza = guardian.GetComponent<GuardianControl> ().fuerza;
				vida -= fuerza;
			} 
		}
	}

}
