using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Barrera;
    void Start()
    {

    }

    private void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(Barrera);
        
    }

  
}



