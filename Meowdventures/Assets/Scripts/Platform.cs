using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform : MonoBehaviour


{
    public PlayerMovement playerMovementScript;
    public Transform[] backgroundObjects; // Array para almacenar las imágenes de fondo
    private Vector3[] backgroundInitialPositions; // Array para guardar las posiciones iniciales de las imágenes de fondo

    public Transform respawnPoint;  // Asigna el punto de respawn desde el inspector.

    void Start()
    {
        // Inicializa el array de posiciones iniciales
        backgroundInitialPositions = new Vector3[backgroundObjects.Length];
        for (int i = 0; i < backgroundObjects.Length; i++)
        {
            backgroundInitialPositions[i] = backgroundObjects[i].position;
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            // Respawn del jugador
            if (playerMovementScript != null)
            {
                playerMovementScript.RespawnPlayer();
            }

            // Reposiciona todas las imágenes de fondo al punto inicial
            for (int i = 0; i < backgroundObjects.Length; i++)
            {
                backgroundObjects[i].position = backgroundInitialPositions[i];
            }

            Debug.Log("Player Respawned");
            Debug.Log("Sound");
            Debug.Log("ResetTimer");
        }
    }
}

//{
//    public PlayerMovement playerMovementScript;
//    private float timer = 0f;
//    public Transform respawnPoint;  // Asigna el punto de respawn desde el inspector.
//    //public Transform playerSpawnPoint;  // Añade esta línea

//    public void OnTriggerEnter2D(Collider2D collider)
//    {
//        if(collider.gameObject.tag == "Player")
//        {
//            FindObjectOfType<AudioManager>().Play("PlayerDeath");

//            //collider.gameObject.transform.position = respawnPoint.position;


//            if (playerMovementScript != null)
//            {
//                playerMovementScript.RespawnPlayer();
//            }
//            Debug.Log("Player Respawned");
//            Debug.Log("Sound");
//            Debug.Log("ResetTimer");



//        }
//    }
//    //public void SpawnPlayerAtSpawnPoint()
//    //{
//    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
//    //    if (player != null)
//    //    {
//    //        player.transform.position = playerSpawnPoint.position;
//    //    }
//    //}

//    //public void RestartTimer()
//    //{
//    //    timer = 0f;
//    //    Debug.Log("ResetTimer");
//    //}
//}
