using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int enemiesDefeated = 0;

    public void DefeatEnemy()
    {
        enemiesDefeated++;
    }

    public int GetEnemies()
    {
        return enemiesDefeated;
    }

    private void Start()
    {
        // Llama a LoadPlayer cuando inicia la escena
        //LoadPlayer();

        //// Llama a SavePlayer cada segundo después de un segundo de la inicialización
        //InvokeRepeating("SavePlayer", 1f, 1f);
    }
    private void Update()
    {

        
        //SavePlayer();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Savepoint"))
        {
            SavePlayer();
        }
    }

        private void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
            transform.position = position;
            enemiesDefeated = data.enemiesDefeated;
        }
    }
}
