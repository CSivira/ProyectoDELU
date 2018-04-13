using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmisorControl : MonoBehaviour {
	[Header ("Prefabricado")]
	public GameObject enemigo;
	[Space]

	[Header ("Secuencia de creacion")]
	[Tooltip ("Llenar con las letras de la A-E, siendo A la primera posicion y E la ultima posicion del emisor. Se pueden repetir letras")]
	public string[] secuencia;

	private Transform[] posiciones = new Transform[5];
	public Transform A;
	public Transform B;
	public Transform C;
	public Transform D;
	public Transform E;
	private Transform posicion;

	void Awake() {
		posiciones = new Transform[5] {A,B,C,D,E};
	}

	void IniciarEmision() {
		if (secuencia.Length != 0) {
			for (int i = 0; i < secuencia.Length; i++){

				switch(secuencia[i]){
				case "A":
					posicion = posiciones [0];
					break;
				case "B":
					posicion = posiciones [1];
					break;
				case "C":
					posicion = posiciones [2];
					break;
				case "D":
					posicion = posiciones [3];
					break;
				case "E":
					posicion = posiciones [4];
					break;
				}

				Instantiate (enemigo,posicion.position,Quaternion.identity);
			}
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere (A.position,0.15f);
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (B.position,0.15f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (C.position,0.15f);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere (D.position,0.15f);
		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere (E.position,0.15f);

		Vector3 arriba = new Vector3 (transform.position.x,transform.position.y + 6f,transform.position.z);
		Vector3 abajo = new Vector3 (transform.position.x,transform.position.y - 6f,transform.position.z);

		Gizmos.color = Color.white;
		Gizmos.DrawLine (abajo,arriba);
	}
}
