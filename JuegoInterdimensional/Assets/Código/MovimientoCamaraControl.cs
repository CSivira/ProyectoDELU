using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamaraControl : MonoBehaviour {

	public GameObject jugador;
	public Vector2 min, max;

	void Start () {
		
	}

	void Update () {
		transform.position = new Vector3(
			Mathf.Clamp(jugador.transform.position.x,min.x,max.x),
			Mathf.Clamp(jugador.transform.position.y,min.y,max.y), 
			transform.position.z);
	}
}
