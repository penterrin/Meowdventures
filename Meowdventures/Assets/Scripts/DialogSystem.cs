using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
    public TMP_Text dialogText;
    public string[] dialogs;
    private int currentDialogIndex = 0;

   

    void Start()
    {
        ShowDialog();
        //FindObjectOfType<AudioManager>().StopPlaying("Theme");
        //FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
        //FindObjectOfType<AudioManager>().Play("Level2");


        if (SceneManager.GetActiveScene().name == "Dialogues")
        {
            // Iniciar la m�sica del nivel 2
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            FindObjectOfType<AudioManager>().StopPlaying("Level2");
            FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
            FindObjectOfType<AudioManager>().StopPlaying("Level3");
            FindObjectOfType<AudioManager>().Play("Level2");
            Debug.Log("Sonando");

        }
        if (SceneManager.GetActiveScene().name == "Dialogues1")
        {
            // Iniciar la m�sica del nivel 2
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            FindObjectOfType<AudioManager>().StopPlaying("Level2");
            FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
            FindObjectOfType<AudioManager>().StopPlaying("Level3");
            FindObjectOfType<AudioManager>().Play("Level2");
            Debug.Log("Sonando");

        }
        if (SceneManager.GetActiveScene().name == "Dialogues2")
        {
            // Iniciar la m�sica del nivel 2
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            FindObjectOfType<AudioManager>().StopPlaying("Level2");
            FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
            FindObjectOfType<AudioManager>().StopPlaying("Level3");
            FindObjectOfType<AudioManager>().Play("Level2");
            Debug.Log("Sonando");

        }
        if (SceneManager.GetActiveScene().name == "Dialogues3")
        {
            // Iniciar la m�sica del nivel 2
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            FindObjectOfType<AudioManager>().StopPlaying("Level2");
            FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
            FindObjectOfType<AudioManager>().StopPlaying("Level3");
            FindObjectOfType<AudioManager>().Play("Level2");
            Debug.Log("Sonando");

        }
        if (SceneManager.GetActiveScene().name == "Dialogues4")
        {
            // Iniciar la m�sica del nivel 2
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            FindObjectOfType<AudioManager>().StopPlaying("Level2");
            FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
            FindObjectOfType<AudioManager>().StopPlaying("Level3");
            FindObjectOfType<AudioManager>().Play("Level2");
            Debug.Log("Sonando");

        }

    }

    void Update()
    {
        // Avanzar al siguiente di�logo al hacer clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            ShowNextDialog();
        }
    }

    void ShowDialog()
    {
        if (currentDialogIndex < dialogs.Length)
        {
            dialogText.text = dialogs[currentDialogIndex];
        }
        else
        {
            // Si todos los di�logos se han mostrado, cambiar a la siguiente escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void ShowNextDialog()
    {
        currentDialogIndex++;
        ShowDialog();
    }




    
}
