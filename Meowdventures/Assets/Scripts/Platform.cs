using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform : MonoBehaviour    
{
    public PlayerMovement playerMovementScript;
    private float timer = 0f;
    public Transform respawnPoint;  // Asigna el punto de respawn desde el inspector.
    //public Transform playerSpawnPoint;  // Añade esta línea

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");

            //collider.gameObject.transform.position = respawnPoint.position;


            if (playerMovementScript != null)
            {
                playerMovementScript.RespawnPlayer();
            }
            Debug.Log("Player Respawned");
            Debug.Log("Sound");
            Debug.Log("ResetTimer");

            

        }
    }
    //public void SpawnPlayerAtSpawnPoint()
    //{
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    if (player != null)
    //    {
    //        player.transform.position = playerSpawnPoint.position;
    //    }
    //}

    //public void RestartTimer()
    //{
    //    timer = 0f;
    //    Debug.Log("ResetTimer");
    //}
}
