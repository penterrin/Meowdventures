using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Cinematics";   
    

    public Transform playerSpawnPoint;

    public void PlayGame ()
    {
        SceneManager.LoadScene("Cinematics");
        FindObjectOfType<AudioManager>().StopPlaying("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("Level2");
        FindObjectOfType<AudioManager>().StopPlaying("Menu");
        FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
        FindObjectOfType<AudioManager>().Play("Click");        


    }
    public void QuitGame ()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("Level2");
        FindObjectOfType<AudioManager>().StopPlaying("Menu");
        FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
        FindObjectOfType<AudioManager>().Play("Click");

        Debug.Log("QUIT");
        Application.Quit();
    }

   
}
