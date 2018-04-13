using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControl : MonoBehaviour {

	Transform perseguir;

	void Start () {
		perseguir = GameObject.Find ("Jugador").transform;
	}

	void Update () {
		transform.position = new Vector3 (
			perseguir.position.x,
			transform.position.y,
			transform.position.z);
	}
}
