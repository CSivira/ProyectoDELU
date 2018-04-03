using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilControl : MonoBehaviour {

	void FixedUpdate () {
		
	}

	void OnTriggerEnter2D(Collider2D otro) {
		if (otro.gameObject.tag == "JugadorCuerpo" || otro.gameObject.tag == "Borde") {
			Destroy (this.gameObject);
		}
	}
}
