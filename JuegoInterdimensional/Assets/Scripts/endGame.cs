using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour {
    public Canvas endGameUI;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("FINISH");
        if (coll.gameObject.tag == "Player")
            endGameUI.enabled = true;

    }
}
