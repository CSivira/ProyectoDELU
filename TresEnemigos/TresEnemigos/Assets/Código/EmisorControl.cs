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
	private Transform A;
	private Transform B;
	private Transform C;
	private Transform D;
	private Transform E;
	private Transform posicion;

	void Start() {
		A = this.gameObject.transform.GetChild (0);
		B = this.gameObject.transform.GetChild (1);
		C = this.gameObject.transform.GetChild (2);
		D = this.gameObject.transform.GetChild (3);
		E = this.gameObject.transform.GetChild (4);

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
}
