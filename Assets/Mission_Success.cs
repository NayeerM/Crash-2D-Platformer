using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mission_Success : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
       SceneManager.LoadScene("GameSuccess");
   }
}
