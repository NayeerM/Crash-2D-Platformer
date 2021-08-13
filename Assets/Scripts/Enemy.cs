using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    [SerializeField] protected AudioSource deathSound;
    public AudioClip killSound;
    
    protected virtual void Start() {
        anim=GetComponent<Animator>();
        rb=GetComponent<Rigidbody2D>();
    }
    public void jumpedOn(){
        rb.bodyType=RigidbodyType2D.Static;
        deathSound.clip=killSound;
        deathSound.Play();
        rb.velocity= new Vector2 (0,0);
        anim.SetTrigger("Death");

    }

    private void disableCollider(){
        GetComponent<Collider2D>().enabled = false;
    }

    private void death(){
        Destroy(this.gameObject);
    }
}
