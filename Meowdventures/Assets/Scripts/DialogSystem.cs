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
    }

    void Update()
    {
        // Avanzar al siguiente diálogo al hacer clic izquierdo
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
            // Si todos los diálogos se han mostrado, cambiar a la siguiente escena
            SceneManager.LoadScene("Main");
        }
    }

    void ShowNextDialog()
    {
        currentDialogIndex++;
        ShowDialog();
    }
}
