using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fall : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D coll;
    [SerializeField] private float deathVel;
    [SerializeField] private AudioSource ad;
    [SerializeField] private AudioSource bg_Audio;
    [SerializeField] string sceneToLoad;
    public AudioClip fallAudioClip;

    private void Start() {
        
    }

    IEnumerator WaitForSound(AudioClip Sound){
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitWhile(() => ad.isPlaying == true);
        yield return new WaitForSeconds(1f);
        //Destroy(this.gameObject);
        
    }

    

    IEnumerator Fell(){
        
        StartCoroutine(WaitForSound(fallAudioClip));
        bg_Audio.Stop();
        ad.Play();
        anim.SetBool("isDead",true);
        coll.enabled=false;
        rb.velocity=new Vector2(0,deathVel);
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(sceneToLoad);
        

    }

    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            StartCoroutine(Fell());
            
            
            
            
        }
    }
}
