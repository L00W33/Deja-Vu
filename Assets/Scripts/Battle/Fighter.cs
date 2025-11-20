using System.Collections;
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
    public string[] MoveList;
    public bool flickering;

    private SpriteRenderer mySprite;


    private void Start()
    {
        mySprite = GetComponentInChildren<SpriteRenderer>();
        flickering = false;
    }
    public bool takeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;

        if (!flickering)
        {
            StopCoroutine(flicker());
            StartCoroutine(flicker());
        }
        
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


    // causes sprite to flicker when taking damage
    IEnumerator flicker()
    {
        flickering = true;
        for (int i = 0; i < 8; i++)
        {
            if (i % 2 == 0)
            {
                mySprite.enabled = false;
            }
            else
            {
                mySprite.enabled = true;
            }

            yield return new WaitForSeconds(0.125f);
        }
        mySprite.enabled = true;
        flickering = false;
    }
}


