using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Audio : MonoBehaviour
{   
    private AudioSource ad;
    // Start is called before the first frame update
    void Start()
    {
        ad=GetComponent<AudioSource>();
        ad.Play();
        StartCoroutine(waiter());
    }

    IEnumerator waiter(){
        float startVolume = ad.volume;
        float FadeTime=2;
        yield return new WaitForSeconds(4);
        while (ad.volume > 0)
        {
            ad.volume -= startVolume * Time.deltaTime / FadeTime;
 
            yield return null;
        }
        
        ad.Stop();
        ad.volume = startVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
