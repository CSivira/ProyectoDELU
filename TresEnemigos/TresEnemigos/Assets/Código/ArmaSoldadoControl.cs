using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaSoldadoControl : MonoBehaviour {

	Animator animador;

	void Start(){
		animador = GetComponent<Animator> ();
	}

	void Ataque (bool accion) {
		if (accion) {
			animador.SetBool ("Ataque", true);
		} else {
			animador.SetBool ("Ataque", false);
		}
	}
}
