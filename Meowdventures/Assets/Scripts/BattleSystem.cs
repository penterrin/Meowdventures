using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TMP_Text dialogueText;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;


    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
       GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();


        GameObject EnemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = EnemyGO.GetComponent<Unit>();

        dialogueText.text = "Defeat " + enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }


    IEnumerator PlayerAttack()
    {

        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You attacked with a normal attack ";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }


    }

    IEnumerator EnemyTurn()
    {
        //if (enemy = mage)
        //{
            dialogueText.text = enemyUnit.unitName + " attacks!";

            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.Magicdamage);

            playerHUD.SetHP(playerUnit.currentHP);

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
        //}

    }


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won!";
            SceneManager.LoadScene(2);
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action";
    }


    IEnumerator PlayerMagicAttack()
    {
        // Check if the player has enough magic points to use the magic attack
        if (playerUnit.MagicPoints >= playerUnit.MagicPointCost)
        {
            // Deduct the magic point cost
            playerUnit.ReducePoints(playerUnit.MagicPointCost);

            // Perform the magic attack
            bool isDead = enemyUnit.TakeDamage(playerUnit.Magicdamage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You attacked with a magic attack ";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "You don't have enough magic points!";
            // Handle the case where the player doesn't have enough magic points to perform the magic attack
            // You can display a message or take appropriate actions here.
        }
    }



    //IEnumerator PlayerHeal()
    //{
    //    playerUnit.Heal(5);

    //    playerHUD.SetHP(playerUnit.currentHP);
    //    dialogueText.text = "You feel renewed strength!";

    //    yield return new WaitForSeconds(2f);

    //    state = BattleState.ENEMYTURN;
    //    StartCoroutine(EnemyTurn());
    //}

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }


    public void OnMagicAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerMagicAttack());
    }
}
