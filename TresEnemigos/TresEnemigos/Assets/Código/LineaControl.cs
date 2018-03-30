using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineaControl : MonoBehaviour {

	public Transform desde;
	public Transform hasta;

	void OnDrawGizmosSelected() {
		if (desde != null && hasta != null) {
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(desde.position,0.15f);
			Gizmos.DrawSphere(hasta.position,0.15f);
			Gizmos.DrawLine(desde.position,hasta.position);
		}
	}
}
