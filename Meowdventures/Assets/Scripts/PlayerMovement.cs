using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
	//public GameObject Barrera;

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

    bool isTouchingEnemy = false;


    // Update is called once per frame
    void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}

        //if (Input.GetButtonDown("Crouch"))
        //{
        //	crouch = true;
        //} else if (Input.GetButtonUp("Crouch"))
        //{
        //	crouch = false;
        //}

        if (Input.GetKeyDown(KeyCode.E) && isTouchingEnemy)
        {
            // Inicia el combate por turnos
            SceneManager.LoadScene("Game");
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            FindObjectOfType<AudioManager>().Play("CombatMusic");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemys"))
		{
            isTouchingEnemy = true;
            //SceneManager.LoadScene(2);
            //FindObjectOfType<AudioManager>().StopPlaying("Theme");
            //FindObjectOfType<AudioManager>().Play("CombatMusic");
        }
        if (other.gameObject.CompareTag("Portal"))
        {
            
            SceneManager.LoadScene("Menu");
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            FindObjectOfType<AudioManager>().Play("CombatMusic");
        }
        //if (other.gameObject.CompareTag("Barrier"))
        //{

        //   Destroy(other.gameObject);
            
        //}
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isTouchingEnemy = false;
        }
    }


    public void OnLanding ()
	{
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}


    
}
