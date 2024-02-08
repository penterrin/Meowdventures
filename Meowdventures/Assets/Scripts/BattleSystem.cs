using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public Animator playerAnimator;

    //private Animator playerAnimator; // Animator del jugador
    //public Animator animator;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TMP_Text dialogueText;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    private int enemiesDefeatedCount = 0; // NO VALE PARA NADA
    private int attacksCount = 0;

    public BattleState state;

    public UnityEngine.UI.Button attackButton;
    public UnityEngine.UI.Button ultiButton;
    public UnityEngine.UI.Button healButton;

    public GameObject GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());

        // Encuentra el objeto del jugador y su Animator
        GameObject playerObject = GameObject.FindWithTag("Player"); 

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
        DisableButtons();
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject EnemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = EnemyGO.GetComponent<Unit>();

        dialogueText.text = "DefeAt " + enemyUnit.unitName;
        
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        DisableButtons();
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You AttAcked with A normAl AttAck ";

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
            enemyHUD.SetHP(enemyUnit.currentHP = 0);
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = "You deAl " + playerUnit.damage + " dAmAge...";

            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        }
        playerAnimator.SetBool("Idle", true);
    }

    IEnumerator EnemyTurn()
    {
        playerAnimator.SetBool("Attack", false);
        if (enemyUnit.MagicPoints >= enemyUnit.MagicPointCost)
        {
            enemyUnit.ReducePoints(enemyUnit.MagicPointCost);
            dialogueText.text = enemyUnit.unitName + " AttAcks!";

            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.Magicdamage);

            playerHUD.SetHP(playerUnit.currentHP);
            enemyHUD.SetMP(enemyUnit.MagicPoints);

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
            dialogueText.text = "You were defeAted.";
            GameOverScreen.SetActive(true);
        }
        //playerAnimator.SetBool("AttAck", false);
    }

    void PlayerTurn()
    {
        EnableButtons();

        dialogueText.text = "Choose An Action";

        playerAnimator.SetBool("Idle", true);
        playerAnimator.SetBool("Attack", false);
    }

    IEnumerator PlayerMagicAttack()
    {
        DisableButtons();
        //// Cambiar al estado de reposo
        //playerAnimator.SetBool("Idle", true);

        // Check if the player has enough magic points to use the magic attack
        if (playerUnit.MagicPoints >= playerUnit.MagicPointCost)
        {
            // Deduct the magic point cost
            playerUnit.ReducePoints(playerUnit.MagicPointCost);

            // Perform the magic attack
            bool isDead = enemyUnit.TakeDamage(playerUnit.Magicdamage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            playerHUD.SetMP(playerUnit.MagicPoints);
            dialogueText.text = "You AttAcked with A mAgic AttAck ";

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
                enemyHUD.SetHP(enemyUnit.currentHP = 0);
                StopAllCoroutines();
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                enemyHUD.SetHP(enemyUnit.currentHP);
                dialogueText.text = "You deAl " + playerUnit.Magicdamage + " of MAgic dAmAge...";

                yield return new WaitForSeconds(2f);
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "You don't hAve enough mAgic points!";
            // Handle the case where the player doesn't have enough magic points to perform the magic attack
            // You can display a message or take appropriate actions here.
        }

        playerAnimator.SetBool("Idle", true);
    }

    IEnumerator PlayerHeal()
    {
        DisableButtons();
        playerAnimator.SetBool("Attack", false);
        playerUnit.Heal(60);

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
        attacksCount++;
        if (state != BattleState.PLAYERTURN)
            return;
        FindObjectOfType<AudioManager>().Play("Click");

        StartCoroutine(PlayerAttack());
    }

    public void OnMagicAttackButton()
    {
        attacksCount = 0;
        if (state != BattleState.PLAYERTURN)
            return;
        FindObjectOfType<AudioManager>().Play("Click");

        StartCoroutine(PlayerMagicAttack());
    }

    public void OnHealButton()
    {
        playerAnimator.SetBool("Attack", false);
        if (state != BattleState.PLAYERTURN)
            return;

        FindObjectOfType<AudioManager>().Play("Click");

        StartCoroutine(PlayerHeal());
    }

    public int GetEnemiesDefeatedCount()
    {
        return enemiesDefeatedCount;
    }

    private void EnableButtons()
    {
        attackButton.interactable = true;
        ultiButton.interactable = attacksCount > 2;
        healButton.interactable = true;
    }

    private void DisableButtons()
    {
        attackButton.interactable = false;
        ultiButton.interactable = false;
        healButton.interactable = false;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

