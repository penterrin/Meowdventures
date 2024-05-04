using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour {
    //public GameObject Barrera;

    public Platform platformScript;

    public CharacterController2D controller;
	public Animator animator;
    //public Transform SpawnPoint;
    public Transform SpawnPoint;


    public float runSpeed = 40f;
    public float wallJumpForce = 20f;  // Fuerza para el Wall Jump
    public float wallJumpCooldown = 1f;  // Tiempo de espera entre Wall Jumps

    public float timerduration = 60f; // duraci�n del temporizador en segundos
    private float timer = 0f; // tiempo actual del temporizador
    public bool timerEnabled = false;
    public TextMeshProUGUI timerText;


    float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

    bool isTouchingEnemy = false;

    bool isTouchingInteractable = false;    

    bool isTouchingWall = false;
    bool canWallJump = true;  // Variable para gestionar el cooldown del Wall Jump

    public Interactable focus;


    //void Start()
    //{
    //    timer = 0f; // Inicializamos el temporizador
    //}

    // Update is called once per frame
    void Update () {


        if (timerEnabled)
        {
            timer += Time.deltaTime;

            // Verificar el tiempo transcurrido y reiniciar al jugador si es necesario
            if (timer >= timerduration)
            {
                RespawnPlayer();
                // Reiniciar el temporizador desde el script de la plataforma
                
            }
        }


        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
            // Si el jugador est� tocando una pared, permitir el wall jump
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

        UpdateTimerUI();



    //    if(Input.GetKeyDown(KeyCode.F))
    //    {
            
    //      if (isTouchingInteractable )
    //      {
    //            Interactable interactable = hit.collider.GetComponent<Interactable>();
    //      }

    //      if (Interactable != null) 
    //      {
    //            SetFocus(Interactable);
    //      }
            
            
    //    }
    //}
    //void SetFocus(Interactable newFocus)
    //{

        if (DialogueManager.Instance.isDialogueActive)
        {
            horizontalMove = 0f; 
            return;
        }
        

    }
    private void Start()
    {
        // Comprobar si la escena actual es el nivel 2
        if (SceneManager.GetActiveScene().name == "Level2")
        {
           
            // Iniciar la m�sica del nivel 2
            FindObjectOfType<AudioManager>().Play("Level2");
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
            FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
            Debug.Log("Sonando");


        }
        // Comprobar si la escena actual es el nivel 2
        if (SceneManager.GetActiveScene().name == "Main")
        {
            // Iniciar la m�sica del nivel 2
            FindObjectOfType<AudioManager>().Play("Theme");
            FindObjectOfType<AudioManager>().StopPlaying("Level2");
            FindObjectOfType<AudioManager>().StopPlaying("CombatMusic");
            Debug.Log("Sonando");

        }

    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            float remainingTime = Mathf.Max(0f, timerduration - timer);
            string formattedTime = string.Format("{0:00}:{1:00}", Mathf.Floor(remainingTime / 60), remainingTime % 60);
            timerText.text = formattedTime;
        }
    }

    private void WallJump()
    {
        // Realiza el Wall Jump
        controller.Move(wallJumpForce * -transform.localScale.x, false, true);

        // Restablece la variable de salto y la animaci�n
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
        if (other.gameObject.CompareTag("Interactable"))
        {
            isTouchingInteractable = true;
        }
        if (other.gameObject.CompareTag("Portal"))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            FindObjectOfType<AudioManager>().StopPlaying("Theme");
           
           


            //FindObjectOfType<AudioManager>().Play("Level2");




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
        if (other.gameObject.CompareTag("Interactable"))
        {
            isTouchingInteractable = false;
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

    public void RespawnPlayer()
    {
        // Colocar al jugador en el punto de respawn
        transform.position = SpawnPoint.position;

        // Reiniciar el contador de tiempo
        timer = 0f;
    }

    public void RestartTimer()
    {
        timer = 0f;
    }


}
