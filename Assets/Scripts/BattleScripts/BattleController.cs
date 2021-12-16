using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {STARTBATTLE, WONBATTLE, LOSTBATTLE, PLAYERTURN, ENEMYTURN}

public class BattleController : MonoBehaviour
{
    public BattleState battleState;

    //!!!implement a list of entities and game objects + the logic behind them, so we can have more entities appear on screen
    //!!!we'll also need to reference the BattleController from GameManager to determine when battles need to happen

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerFightPos;
    public Transform enemyFightPos;

    private BattleEntity playerEntity;
    private BattleEntity enemyEntity;

    public Text battleDialogueText;
    public Transform battleDialoguePanel;

    public BattleHUD playerBattleHUD;

    bool firstTurn = true;

    void Start()
    {
        battleState = BattleState.STARTBATTLE;
        StartCoroutine(SetupBattle());

    }

    IEnumerator SetupBattle()
    {
        GameObject playerObject = Instantiate(playerPrefab, playerFightPos);
        GameObject enemyObject = Instantiate(enemyPrefab, enemyFightPos);

        playerEntity = playerObject.GetComponent<BattleEntity>();
        enemyEntity = enemyObject.GetComponent<BattleEntity>();

        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        playerEntity.setPlayerStats(playerController.getLevel, playerController.getHealth, playerController.getMaxHealth, playerController.getDamage, playerController.getMP, playerController.getName);

        battleDialogueText.text = "A wild " + enemyEntity.entityName + " approaches.";

        playerBattleHUD.SetHUDInfo(playerEntity);

        yield return new WaitForSeconds(2f);

        battleState = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }
    IEnumerator PlayerTurn()
    {
        if(firstTurn)
        {
            battleDialogueText.enabled = true;
            battleDialoguePanel.GetComponent<Image>().enabled = true;
            battleDialogueText.text = "Choose an action";
            yield return new WaitForSeconds(1f);
            battleDialogueText.enabled = false;
            battleDialoguePanel.GetComponent<Image>().enabled = false;

            firstTurn = false;
        }
       
    }

    IEnumerator EnemyTurn()
    {
        //!!!add some more logic

        battleDialogueText.enabled = true;
        battleDialoguePanel.GetComponent<Image>().enabled = true;
        battleDialogueText.text = enemyEntity.entityName + " attacked for " + enemyEntity.damage + " damage!";

        yield return new WaitForSeconds(1f);

        battleDialogueText.enabled = false;
        battleDialoguePanel.GetComponent<Image>().enabled = false;

        bool isDead = playerEntity.TakeDamage(enemyEntity.damage);

        //yield return new WaitForSeconds(.5f);

        playerBattleHUD.SetHealth(playerEntity.health);

        if (isDead)
        {
            battleState = BattleState.LOSTBATTLE;
            EndBattle();
        }
        else
        {
            battleState = BattleState.PLAYERTURN;
            StartCoroutine(PlayerTurn());
        }

    }


    public void OnTatakauPressed()
    {
        if(battleState != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        battleState = BattleState.ENEMYTURN;


        battleDialogueText.enabled = true;
        battleDialoguePanel.GetComponent<Image>().enabled = true;
        battleDialogueText.text = "You attacked for " + playerEntity.damage + " damage!";

        yield return new WaitForSeconds(2f);

        bool isDead = enemyEntity.TakeDamage(playerEntity.damage);

        //update enemyHUD for when we can have health over their heads later


        if(isDead)
        {
            battleState = BattleState.WONBATTLE;
            EndBattle();
        }
        else
        {
            battleState = BattleState.ENEMYTURN;

            StartCoroutine(EnemyTurn());
        }
    }

    void EndBattle()
    {
        if(battleState == BattleState.WONBATTLE)
        {
            battleDialogueText.enabled = true;
            battleDialoguePanel.GetComponent<Image>().enabled = true;
            battleDialogueText.text = "You won!";
        }
        else if (battleState == BattleState.LOSTBATTLE)
        {
            battleDialogueText.enabled = true;
            battleDialoguePanel.GetComponent<Image>().enabled = true;
            battleDialogueText.text = "You lost!";
        }
    }
}
