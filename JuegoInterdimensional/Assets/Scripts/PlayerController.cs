using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector2 move;
    public float speed;
    private Animator anim;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
       move = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
       rb.velocity = move;
        if (Input.GetAxis("Horizontal")==0  && Input.GetAxis("Vertical") == 0)
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }
        if(Input.GetAxis("Jump")!=0)
        {
            anim.SetBool("attack", true);
        }
    }

    public void finishAttack()
    {
        anim.SetBool("attack", false);
    }
}
