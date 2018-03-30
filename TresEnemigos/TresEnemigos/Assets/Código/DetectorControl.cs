using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorControl : MonoBehaviour {

	public GameObject emisor;

	void Start () {
		
	}

	void OnTriggerEnter2D(Collider2D otro) {
		if (otro.gameObject.tag == "JugadorCuerpo") {
			emisor.SendMessage("IniciarEmision",true);
			Destroy (gameObject);
		}
	}
}
