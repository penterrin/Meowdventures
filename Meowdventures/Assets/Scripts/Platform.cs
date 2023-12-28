using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform : MonoBehaviour

    
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            SceneManager.LoadScene(1);

           
            Debug.Log("Sound");
        }
    }
   
}
