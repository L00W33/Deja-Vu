using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    // components for player options
    public GameObject MainMenu;
    public GameObject ItemMenu;
    public GameObject FightMenu;

    public GameObject MainMenuFirst;
    public GameObject ItemMenuFirst;
    public GameObject FightMenuFirst;

    public BattleSystem System1;

    private AttackMenu AttackMenu1;

    public void Start()
    {
        FightMenu.SetActive(false);
        AttackMenu1 = FightMenu.GetComponent<AttackMenu>();
    }
    // Update is called once per frame
    void Update()
    {
        ExtraInput();
    }

    //allows user to presss space in the menu
    void ExtraInput()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
        }
    }
    // enables main menu and no others, selects fight to start with
    //called on turn start and when returning from inventory
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        ItemMenu.SetActive(false);
        FightMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(MainMenuFirst);
    }

    // closes all player menues until the next turn
    public void CloseAllMenus()
    {
        MainMenu.SetActive(false);
        ItemMenu.SetActive(false);
        FightMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    // ends player turn and prings up fight menu, which then goes into enemy turn
    public void OnPressFight()
    {
        CloseAllMenus();
        FightMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(FightMenuFirst);

        // attack Funciton
        // System1.PlayerAttack();
    }


    // closes main menu and opens item menu (which is a list of items in inventory
    public void OnPressItem()
    {
        MainMenu.SetActive(false);
        ItemMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(ItemMenuFirst);
    }

    // switch to attack list on opening fight menu


}
