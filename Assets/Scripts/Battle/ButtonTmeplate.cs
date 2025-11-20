using UnityEngine;
using UnityEngine.UI;

public class ButtonTmeplate : MonoBehaviour
{
    private Fighter Player;
    private Fighter Enemy;
    private Attacks Library;
    private BattleSystem bSystem;
    private PlayerAction Action;
    private AttackMenu aMenu;

    public string moveName;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        Enemy = GameObject.FindWithTag("Enemy").GetComponent<Fighter>();
        Library = FindAnyObjectByType<Attacks>();
        bSystem = FindAnyObjectByType<BattleSystem>();
        Action = FindAnyObjectByType<PlayerAction>();
        aMenu = FindAnyObjectByType<AttackMenu>();
    }
    public void ClickEvent()
    {
        Library.CallByName(moveName, Enemy, Player);
        bSystem.State = BattleState.APphase;
        bSystem.EndPlayerDialogue();
        Action.CloseAllMenus();
        aMenu.DestroyAllButtons();
    }
}
