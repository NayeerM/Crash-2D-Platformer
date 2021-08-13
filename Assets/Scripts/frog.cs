using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frog : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float jumplength=10f;
    [SerializeField] private float jumpHeight=10f;
    [SerializeField] private LayerMask ground;
    private enum State { idle, jumping,falling}

    private State state=State.idle;

    private Collider2D coll;

    private bool facingLeft=true;

    protected override void Start() {
        base.Start();
        coll=GetComponent<Collider2D>();
    }
    

    private void Update() {

        if(anim.GetInteger("state")==1){

            if (Mathf.Abs(rb.velocity.y)<.1f)
            {
                state=State.falling;
            }
        }
        else if(coll.IsTouchingLayers(ground) && state==State.falling){

                state=State.idle;
        }

        anim.SetInteger("state", (int)state);
    }



    private void Move(){
        if(facingLeft){
            //check if more than leftCap
            if(transform.position.x>leftCap)
            {
                //check if sprite is facing right direction
                if(transform.localScale.x!=1){
                    transform.localScale= new Vector3(1,1,1);
                }
                if(coll.IsTouchingLayers(ground))
                {
                    state=State.jumping;
                    rb.velocity = new Vector2(-jumplength, jumpHeight);
                }

            }
            else
            {
                facingLeft=false;
            }
        }
        
        
        else  {

            if(transform.position.x<rightCap)
            {
                //check if sprite is facing right direction
                if(transform.localScale.x!=-1){
                    transform.localScale= new Vector3(-1,1,1);
                }
                if(coll.IsTouchingLayers(ground))
                {
                    state=State.jumping;
                    rb.velocity = new Vector2(jumplength, jumpHeight);
                }

            }
            else
            {
                
                facingLeft=true;
            }
        }
        
        
    }
    
    
}
