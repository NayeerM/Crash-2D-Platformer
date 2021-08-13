using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public TextMeshProUGUI pointText;
    private float score;

    private void Awake() {
        score=PlayerPrefs.GetFloat("score",0);
        Debug.Log(score);
        Debug.Log("Game Over Screen Enabled");
        pointText.text=score.ToString()+" POINTS";
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void MainMenu(){
         SceneManager.LoadScene("MainMenu");
    }
    public void RestartLvl(){
         SceneManager.LoadScene("TestScene");
    }

    
}
