using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector2 move;
    public float speed;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool faceLeft;
    public BoxCollider2D weaponColl;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        faceLeft = false;
        weaponColl.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        #region flip
        if (Input.GetAxis("Horizontal")<0 && !faceLeft)
        {
            flip();
        }else if (Input.GetAxis("Horizontal") > 0 && faceLeft)
        {
            flip();
        }
        #endregion
        #region movement
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
        #endregion

        if (Input.GetAxis("Jump") != 0)
        {
            anim.SetBool("attack", true);
            weaponColl.enabled = true;
            speed = 0f;

        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("triggerEnter "+ other.gameObject.name);
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<enemyLife>().damage(50);
            
        }
    }

    public void startAttack()
    {
       
    }
    public void finishAttack()
    {
        anim.SetBool("attack", false);
        
        weaponColl.enabled = false;
        speed = 3;
    }

    public void hurtStop()
    {
        anim.SetBool("hurt", false);
    }
    public void flip()
    {
        faceLeft = !faceLeft;
        //sprite.flipX = faceLeft;
        this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); 
       
    }
}
