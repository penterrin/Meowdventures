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
    public float wallJumpForce = 20f;  // Fuerza para el Wall Jump
    public float wallJumpCooldown = 0.5f;  // Tiempo de espera entre Wall Jumps

    float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

    bool isTouchingEnemy = false;

    bool isTouchingWall = false;
    bool canWallJump = true;  // Variable para gestionar el cooldown del Wall Jump


    // Update is called once per frame
    void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
            // Si el jugador está tocando una pared, permitir el wall jump
            if (isTouchingWall)
            {
                WallJump();
            }
            else
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
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


    private void WallJump()
    {
        // Realiza el Wall Jump
        controller.Move(wallJumpForce * -transform.localScale.x, false, true);

        // Restablece la variable de salto y la animación
        jump = false;
        animator.SetBool("IsJumping", false);

        // Inicia el cooldown del Wall Jump
        StartCoroutine(WallJumpCooldown());
    }


    IEnumerator WallJumpCooldown()
    {
        canWallJump = false;
        yield return new WaitForSeconds(wallJumpCooldown);
        canWallJump = true;
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

        if (other.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemys"))
        {
            isTouchingEnemy = false;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
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
