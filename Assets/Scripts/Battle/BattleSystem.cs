using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.TextCore.Text;
using static UnityEditor.Experimental.GraphView.GraphView;

public enum BattleState
{
    APphase,
    EnemyTurn,
    PlayerTurn,
    FightStart,
    Wait,
    Animation,
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
    public AttackMenu AttackMenu1;

    // variables for animation elements
    public PointGain PointsAdded;
    public GameObject TempTransform;

    // text box objects
    public Dialogue PlayerDialogueBox;
    public Dialogue EnemyDialogueBox;

    // for NPC attacks
    public Attacks AllAttacks;

    //used to deturmine NPC behviour
    int randTurn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // sets fight up

        State = BattleState.APphase;
        // spawns combatants
        GameObject PlayerObj = Instantiate(PlayerCharacter, PlayerLocation);
        GameObject EnemyObj = Instantiate(Enemy, EnemyLocation);

        // retrieves stats of each combatant
        PlayerUnit = PlayerObj.GetComponent<Fighter>();
        EnemyUnit = EnemyObj.GetComponent<Fighter>();

        // sets up huds
        playerHUD.SetHud(PlayerUnit);
        enemyHUD.SetHud(EnemyUnit);

        // given a base value of 0
        randTurn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(State);
        // Updates accurate HP values to UI
        playerHUD.SetHP(PlayerUnit.currentHP);
        enemyHUD.SetHP(EnemyUnit.currentHP);

        // fight to move forward
        combatLoop();
    }

    //function is hear to avoid having needlessly public variables
    public void EndPlayerDialogue()
    {
        PlayerDialogueBox.gameObject.SetActive(false);
        playerHUD.SetAP(PlayerUnit.currentAP);
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
        if (PlayerUnit.currentAP >= 100 && State == BattleState.APphase)
        {
            AttackMenu1.SetPlayerAttackMenu(PlayerUnit);
            State = BattleState.PlayerTurn;
            action.OpenMainMenu();
            PlayerUnit.currentAP = 0;
            
            PlayerDialogueBox.gameObject.SetActive(true);
            PlayerDialogueBox.StartDialogue();
            return;
        }
        
        // Enemy takes there turn if they have enough AP
        if (EnemyUnit.currentAP >= 100 && State == BattleState.APphase)
        {
            randTurn = UnityEngine.Random.Range(0, EnemyUnit.MoveList.Length);
            State = BattleState.EnemyTurn;
            EnemyUnit.currentAP = 0;

            EnemyDialogueBox.gameObject.SetActive(true);
            EnemyDialogueBox.StartDialogue();
            return;
        }
        // enemies turn that takes place after the dialogue box
        if (State == BattleState.EnemyTurn && EnemyDialogueBox.TextFinished == true)
        {
            EnemyDialogueBox.TextFinished = false;

            AllAttacks.CallByName(EnemyUnit.MoveList[randTurn], PlayerUnit, EnemyUnit);
            return;
        }

        // If neither players have enough AP then both players gain AP
        if (State == BattleState.APphase)
            StartCoroutine(APanimation(0.7f));
    }

    // coroutine that starts in the APphase, handles AP stats + represents them on screen
    IEnumerator APanimation(float seconds)
    {
        State = BattleState.Wait;
        PlayerUnit.gainAP();
        EnemyUnit.gainAP();

        playerHUD.SetAP(PlayerUnit.currentAP);
        enemyHUD.SetAP(EnemyUnit.currentAP);

        yield return new WaitForSeconds(seconds);
        State = BattleState.APphase;
    }

}


// psuedocode for short animation

// set phase to animation phase
// start IEnumerator
// IEnumerator triggers animation
// animation ends by the end
// phase is changed back to AP phase