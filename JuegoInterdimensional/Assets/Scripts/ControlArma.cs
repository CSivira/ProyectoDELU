using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlArma : MonoBehaviour {
	
	GameObject jugador;
	Animator anim;
	int fuerza;

	void Start () {
		anim = GetComponent<Animator> ();
		fuerza = GameObject.Find ("Guardian").GetComponent<ControlGuardian> ().fuerza;
		jugador = GameObject.Find ("Player");
	}

	public void Ataque(bool ataque){
		if (ataque == true) {
			anim.SetBool ("Ataque", true);
		} else {
			anim.SetBool ("Ataque",false);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Jugador") {
				jugador.SendMessage ("Daño", fuerza);
		}
	}
}
