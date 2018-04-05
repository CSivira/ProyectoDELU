using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {
    public Transform player;
    public float smooth;
    public Vector3 offset;
    public float poZ;
    public float poY;
    public float offstX;
    public float offstY;
    public float offstZ;
    public bool htY = true;
    public float vLY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        poZ = Mathf.Lerp(transform.position.z, player.transform.position.z + offstZ, smooth * Time.deltaTime);
        poY = Mathf.Lerp(transform.position.y, player.transform.position.y + offstY, vLY * Time.deltaTime);

        transform.position = new Vector3(player.position.x + offstX, poY, poZ);


    }
}
