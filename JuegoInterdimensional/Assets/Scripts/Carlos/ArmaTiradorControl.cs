using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaTiradorControl : MonoBehaviour {

	public Rigidbody2D proyectil;
	public float rafaga = 30f;
	public float velocidadProyectil = 10f;
	GameObject jugador;

	Vector3 direccion;
	Animator animador;
	int contador = 0;
	float angulo;

	void Start() {
		jugador = GameObject.Find ("Jugador");
		//animador = GetComponent<Animator> ();
	}

	void Update () {
		//Mirada de arma
			direccion = transform.position - jugador.transform.position;
			angulo = Mathf.Atan2 (direccion.y, direccion.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angulo, Vector3.forward);
	
	}

	void AtacarJugador() {
		if (rafaga == contador) {
			Rigidbody2D clon = Instantiate (proyectil, transform.position, Quaternion.identity);
			clon.velocity = (jugador.transform.position - transform.position).normalized * velocidadProyectil;
			contador = 0;
		} else {
			contador++;
		}
	}

	void Ataque(bool accion) {
		if (accion) {
			AtacarJugador ();
		}else{
			
		}
	}
}
