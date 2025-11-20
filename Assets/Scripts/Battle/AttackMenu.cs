using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackMenu : MonoBehaviour
{
    public Fighter PlayerCharacter;
    public Fighter EnemyCharacter;
    public GameObject ButtonTemplate;
    public GameObject ButtonParent;

    public Attacks MoveLibrary;

    public void Start()
    {
    }
    // call on start then disable menu
    public void SetPlayerAttackMenu(Fighter Player)
    {
        for (int i = 0; i < PlayerCharacter.MoveList.Length; i++)
        {
            GameObject Button = Instantiate(ButtonTemplate, ButtonParent.transform);

            Debug.Log(PlayerCharacter.MoveList[0]);
            string AttackName = PlayerCharacter.MoveList[i];
            Button.GetComponentInChildren<TMP_Text>().text = AttackName;
            Button.GetComponent<ButtonTmeplate>().moveName = AttackName;
        }
    }

    public void DestroyAllButtons()
    {
        foreach (Transform child in ButtonParent.transform)
            Destroy(child.gameObject);
    }

}
