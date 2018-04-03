using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	public GameObject projectile;
	public Transform skimmer;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void ProjectileCreator(){
			Instantiate (projectile, skimmer.position,Quaternion.identity);
	}
}
