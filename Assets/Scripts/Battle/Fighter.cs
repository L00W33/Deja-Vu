using UnityEngine;

public class Fighter : MonoBehaviour
{
    // holds a list of the enemy's attributes

    public string unitName;
    public int speed;
    public int maxHP;
    public int currentHP;
    public int maxAP;
    public int currentAP;
    public int attack;

    public bool takeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;
        return false;
    }

    public void heal(int regain)
    {
        currentHP += regain;
    }

    public void gainAP()
    {
        currentAP += speed;
    }
}


