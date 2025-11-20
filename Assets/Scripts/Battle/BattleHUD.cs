using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleHUD : MonoBehaviour
{
    // components for combatent UI
    public TextMeshProUGUI textComponent;

    public Slider AP;
    public Slider HP;
    

    public void SetHud(Fighter fighter1)
    {
        textComponent.text = fighter1.name;

        HP.maxValue = fighter1.maxHP;
        AP.maxValue = fighter1.maxAP;

        HP.value = fighter1.currentHP;
        AP.value = fighter1.currentAP;
    }

    // used by other scripts to set new HP values
    public void SetHP(int newHP)
    {
        HP.value = newHP;
    }

    // used by other scripts to set new AP values
    public void SetAP(int newAP)
    {
        AP.value = newAP;
    }

    // buff and debuff checks
}
