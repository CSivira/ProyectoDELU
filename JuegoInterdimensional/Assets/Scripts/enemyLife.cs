using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLife : MonoBehaviour {
    public float life;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void damage(float amount)
    {
        Debug.Log("WeaponTrigger");
        life -= amount;
    }
}
