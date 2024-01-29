using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public Transform playerRespawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().Play("Click");
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
       


    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        RespawnPlayer();

        new WaitForSeconds(4f);

        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().StopPlaying("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("Level2");        
        FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
        FindObjectOfType<AudioManager>().Play("Click");
        FindObjectOfType<AudioManager>().Play("Menu");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Theme");
        FindObjectOfType<AudioManager>().StopPlaying("Level2");
        FindObjectOfType<AudioManager>().StopPlaying("Menu");
        FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
        FindObjectOfType<AudioManager>().Play("Click");
        Debug.Log("quitting game");
        Application.Quit();
    }

    private void RespawnPlayer()
    {
        // Busca el objeto del jugador en la escena actual
        Player player = FindObjectOfType<Player>();

        // Si se encuentra el jugador, colócalo en el punto de respawn
        if (player != null && playerRespawnPoint != null)
        {
            player.transform.position = playerRespawnPoint.position;
        }
        Debug.Log("Respawn");

    }

}
