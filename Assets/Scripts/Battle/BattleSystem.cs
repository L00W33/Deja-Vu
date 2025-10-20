using System;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.TextCore.Text;

public enum BattleState
{
    APphase,
    EnemyTurn,
    PlayerTurn,
    FightStart,
    Victory,
    Loss
}

public class BattleSystem : MonoBehaviour
{
    public BattleState State;

    // objects for the player and the enemy
    public GameObject Enemy;
    public GameObject PlayerCharacter;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Transform EnemyLocation;
    public Transform PlayerLocation;

    Fighter PlayerUnit;
    Fighter EnemyUnit;

    // for the scrollbar used when blocking
    public PlayerAction action;
    public GameObject ScrollBar;

    // text box objects
    public Dialogue PlayerDialogueBox;
    public Dialogue EnemyDialogueBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // sets fight up

        State = BattleState.FightStart;
        // spawns combatants
        GameObject PlayerObj = Instantiate(PlayerCharacter, PlayerLocation);
        GameObject EnemyObj = Instantiate(Enemy, EnemyLocation);

        // retrieves states of each combatant
        PlayerUnit = PlayerObj.GetComponent<Fighter>();
        EnemyUnit = EnemyObj.GetComponent<Fighter>();

        // sets up huds
        playerHUD.SetHud(PlayerUnit);
        enemyHUD.SetHud(EnemyUnit);
    }

    // Update is called once per frame
    void Update()
    {
        // Updates accurate HP values to UI
        playerHUD.SetHP(PlayerUnit.currentHP);
        enemyHUD.SetHP(EnemyUnit.currentHP);

        // fight to move forward
        combatLoop();
    }

    //function is hear to avoid having needlessly public variables
    public void PlayerAttack()
    {
        if (EnemyUnit.takeDamage(PlayerUnit.attack))
        {
            State = BattleState.Victory;
        }
        else
        {
            State = BattleState.APphase;
        }

        PlayerDialogueBox.gameObject.SetActive(false);
        playerHUD.SetAP(PlayerUnit.currentAP);
    }

    public void EnemyAttack()
    {
        Instantiate(ScrollBar);
    }

    //exists so curser can reference this file instead of others
    public void PlayerTakesDamage(float damageMultiplier)
    {
        if (PlayerUnit.takeDamage(Convert.ToInt32(EnemyUnit.attack * damageMultiplier)))
        {
            State = BattleState.Loss;
        }
    }

    //main combat loop that moves the game forward
    void combatLoop()
    {
        // victory check

        // player takes their turn when they have enough AP
        if (PlayerUnit.currentAP >= 100 && State != BattleState.EnemyTurn)
        {
            State = BattleState.PlayerTurn;
            action.OpenMainMenu();
            PlayerUnit.currentAP = 0;
            
            PlayerDialogueBox.gameObject.SetActive(true);
            PlayerDialogueBox.StartDialogue();
            return;
        }

        Debug.Log(State);
        
        // Enemy takes there turn if they have enough AP
        if (EnemyUnit.currentAP >= 100 && State != BattleState.PlayerTurn)
        {
            State = BattleState.EnemyTurn;
            EnemyUnit.currentAP = 0;

            EnemyDialogueBox.gameObject.SetActive(true);
            EnemyDialogueBox.StartDialogue();
            return;
        }
        // portion of enemies turn that takes place after the dialogue box
        if (State == BattleState.EnemyTurn && EnemyDialogueBox.TextFinished == true)
        {
            EnemyDialogueBox.TextFinished = false;
            EnemyAttack();
            enemyHUD.SetAP(EnemyUnit.currentAP);
            return;
        }

        // If neither players have enough AP then both players gain AP
        if (State == BattleState.APphase)
        {
            PlayerUnit.gainAP();
            EnemyUnit.gainAP();

            playerHUD.SetAP(PlayerUnit.currentAP);
            enemyHUD.SetAP(EnemyUnit.currentAP);
        }
    }
}
