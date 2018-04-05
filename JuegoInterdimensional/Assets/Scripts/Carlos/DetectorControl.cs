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

	void OnDrawGizmosSelected() {
		Vector3 arriba = new Vector3 (transform.position.x,transform.position.y + 6f,transform.position.z);
		Vector3 abajo = new Vector3 (transform.position.x,transform.position.y - 6f,transform.position.z);

		Gizmos.color = Color.white;
		Gizmos.DrawLine (abajo,arriba);
	}
}
