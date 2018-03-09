using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyLife : MonoBehaviour {
    public float life, aux;
    private Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator anim;
    private bool faceLeft;

    // Use this for initialization
    void Start() {
        faceLeft = false;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        if (this.transform.position.x > aux && faceLeft)
        {
            flip();
        } else if (this.transform.position.x < aux && !faceLeft)
        {
            flip();
        }
        aux = this.transform.position.x;
        //Debug.Log(rb.velocity);

    }

    public void damage(float amount)
    {
        //Debug.Log("WeaponTrigger");
        anim.SetBool("hurt", true);
        life -= amount;
        if (life <= 0)
        {
            anim.SetBool("die", true);
            Destroy(this.gameObject, 0.5f);
        }
    }
    public void flip()
    {
        faceLeft = !faceLeft;
        sprite.flipX = faceLeft;
        //this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    
}
