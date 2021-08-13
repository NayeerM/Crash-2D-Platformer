using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State { idle, running, jumping,falling,hurt,death,dead}
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float hurt=10f;
    [SerializeField] private float health=100f;
    public AudioClip footSound;
    public AudioClip jumpEffect;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    public AudioClip cherriesAudioClip;
    [SerializeField] private AudioSource ad;
    [SerializeField] private AudioSource bg_Audio;
    [SerializeField] private float cherries=0;
    [SerializeField] private Text score;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll=GetComponent<Collider2D>();
    }
    private void Update()
    {

        if(state!=State.hurt){
            Movement();
        }

        AnimationState();
        anim.SetInteger("state", (int)state);

    }

    
    //Enemy Collision
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Enemy" ){
            Enemy enemy=other.gameObject.GetComponent<Enemy>();
            if(state==State.falling){
                enemy.jumpedOn();
                Jump();
            }
            else
            {
                state=State.hurt;
                
                takeDamage(10);

                if (other.gameObject.transform.position.x > transform.position.x){
                    
                    //Enemy is to right, therefore receive damage and move left
                    rb.velocity = new Vector2(-hurt, rb.velocity.y);
                }

                else
                {
                    rb.velocity = new Vector2(hurt, rb.velocity.y);
                    //Enemy is to left, therefore take damage and move right
                }

                
                
            }
            
        }
        else if (other.gameObject.tag=="Eagle")
        {
            takeDamage(100);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag=="Collectable" ){
            ad.PlayOneShot(cherriesAudioClip);
            ScoreManager.instance.AddPoint();
            Destroy(collision.gameObject);
        }

        
    }

    

   

    private void takeDamage(int damageAmount){
            ad.clip=hurtSound;
            ad.Play();
            health-=damageAmount;

            if(health<=0){
                gameOver();
            }
        
    }

    private void gameOver(){
        bg_Audio.Stop();
        anim.SetTrigger("death");
        coll.enabled = false;
        rb.bodyType=RigidbodyType2D.Static;
        ad.clip=deathSound;
        ad.Play();
        StartCoroutine(gameOverScreen());
       
    }

    IEnumerator gameOverScreen(){
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("GameOver");
    }

    

    //plays in animation event
    private void death(){

    }

    
    private void Movement(){
        float hDirection = Input.GetAxis("Horizontal");
        
                
        //Move left

        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        

        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {   
            ad.PlayOneShot(jumpEffect);
            Jump();
        }
    }

    private void Jump(){

        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        state = State.jumping;
    }

    private void AnimationState()
    {
        
        if(state == State.jumping)
        {
            if (rb.velocity.y<.1f)
            {
                state=State.falling;
            }

        }
        else if (state==State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state=State.idle;
            }
        }
        else if (state==State.hurt){

            if(Mathf.Abs(rb.velocity.x)<.1f){

                state=State.idle;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state=State.idle;
        }

        
    }
    private void footStep(){
        ad.clip=footSound;
        ad.Play();
    } 
}
    

