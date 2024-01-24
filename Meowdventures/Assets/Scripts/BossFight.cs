using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleStates { START, PLAYERTURN, ENEMYTURN, WON, LOST }




public class BossFight : MonoBehaviour
{
    public Animator playerAnimator;

    //private Animator playerAnimator; // Animator del jugador
    //public Animator animator;






    public GameObject playerPrefab;
    public GameObject KingPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit kingUnit;

    public TMP_Text dialogueText;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    private int enemiesDefeatedCount = 0;



    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());


        // Encuentra el objeto del jugador y su Animator
        GameObject playerObject = GameObject.FindWithTag("Player"); // Asume que el jugador tiene una etiqueta "Player"

        if (playerObject != null)
        {
            playerAnimator = playerObject.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("No se pudo encontrar el objeto del jugador.");
        }
    }




    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();


        GameObject EnemyGO = Instantiate(KingPrefab, enemyBattleStation);
        kingUnit = EnemyGO.GetComponent<Unit>();

        dialogueText.text = "Defeat " + kingUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(kingUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }


    IEnumerator PlayerAttack()
    {




        bool isDead = kingUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(kingUnit.currentHP);
        dialogueText.text = "You attacked with a normal attack ";

        // Establecer animación de ataque
        playerAnimator.SetBool("Attack", true);
        playerAnimator.SetBool("Idle", false);

        yield return new WaitForSeconds(0.5f);

        // Reiniciar animaciones
        playerAnimator.SetBool("Idle", true);
        playerAnimator.SetBool("Attack", false);


        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            enemyHUD.SetHP(kingUnit.currentHP = 0);
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            enemyHUD.SetHP(kingUnit.currentHP);
            dialogueText.text = "You deal " + playerUnit.damage + " damage...";

            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }


        playerAnimator.SetBool("Idle", true);
    }

    IEnumerator EnemyTurn()
    {
        playerAnimator.SetBool("Attack", false);
        if (kingUnit.MagicPoints >= kingUnit.MagicPointCost)
        {


            kingUnit.ReducePoints(kingUnit.MagicPointCost);
            dialogueText.text = kingUnit.unitName + " attacks!";

            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(kingUnit.Magicdamage);

            playerHUD.SetHP(playerUnit.currentHP);
            enemyHUD.SetMP(kingUnit.MagicPoints);


            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }

    }


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won!";
            enemiesDefeatedCount++;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene("Main");
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
        playerAnimator.SetBool("Attack", false);

    }

    void PlayerTurn()
    {

        dialogueText.text = "Choose an action";


        playerAnimator.SetBool("Idle", true);
        playerAnimator.SetBool("Attack", false);
    }


    IEnumerator PlayerMagicAttack()
    {




        //// Cambiar al estado de reposo
        //playerAnimator.SetBool("Idle", true);

        // Check if the player has enough magic points to use the magic attack
        if (playerUnit.MagicPoints >= playerUnit.MagicPointCost)
        {


            // Deduct the magic point cost
            playerUnit.ReducePoints(playerUnit.MagicPointCost);

            // Perform the magic attack
            bool isDead = kingUnit.TakeDamage(playerUnit.Magicdamage);



            enemyHUD.SetHP(kingUnit.currentHP);
            playerHUD.SetMP(playerUnit.MagicPoints);
            dialogueText.text = "You attacked with a magic attack ";


            // Establecer animación de ataque
            playerAnimator.SetBool("Attack", true);
            playerAnimator.SetBool("Idle", false);

            yield return new WaitForSeconds(0.5f);

            // Reiniciar animaciones
            playerAnimator.SetBool("Idle", true);
            playerAnimator.SetBool("Attack", false);



            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                enemyHUD.SetHP(kingUnit.currentHP = 0);
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                enemyHUD.SetHP(kingUnit.currentHP);
                dialogueText.text = "You deal " + playerUnit.damage + " of Magic damage...";

                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "You don't have enough magic points!";
            // Handle the case where the player doesn't have enough magic points to perform the magic attack
            // You can display a message or take appropriate actions here.
        }

        playerAnimator.SetBool("Idle", true);
    }



    IEnumerator PlayerHeal()
    {
        playerAnimator.SetBool("Attack", false);
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

        playerAnimator.SetBool("Attack", false);
    }

    public void OnAttackButton()
    {
        playerAnimator.SetBool("Attack", true);
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }


    public void OnMagicAttackButton()
    {
        playerAnimator.SetBool("Attack", true);
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerMagicAttack());
    }

    public void OnHealButton()
    {
        playerAnimator.SetBool("Attack", false);
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
    public int GetEnemiesDefeatedCount()
    {
        return enemiesDefeatedCount;
    }

}
