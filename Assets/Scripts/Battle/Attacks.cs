using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

// serves as a library for the attacks used by NPCs and the player
public class Attacks : MonoBehaviour
{
    public GameObject ScrollBar;

    // this is a necassary function for NPCs to be able to take their turn
    // it calls other functions by name from the NPC's list
    public void CallByName(string MoveName, Fighter Target, Fighter Caster)
    {
        MethodInfo MoveInfo = this.GetType().GetMethod(MoveName, BindingFlags.Public | BindingFlags.Instance);
        if (MoveInfo.GetParameters().Length == 1)
        {
            MoveInfo.Invoke(this, new object[] { Target });
        }
        else if (MoveInfo.GetParameters().Length == 2)
        {
            MoveInfo.Invoke(this, new object[] { Target, Caster });
        }
        else
        {
            MoveInfo.Invoke(this, new object[] { });
        }
    }

    // basic Enemy Attack
    public void EnemyAttack()
    {
        Instantiate(ScrollBar);
    }
    // simply deals damage to target
    public void BasicAttack(Fighter Target, Fighter Caster)
    {
        Target.takeDamage(Caster.attack);
    }

    // stronger attack will occurr after a number of terms
    public void DelayedAttack(Fighter Target, Fighter Caster, int delay)
    {
        
    }

    //shoots a firball at the target, causing flat damage and burn timer to be added to
    public void FireBall(Fighter Target)
    {

    }

    // BOSS attack, sets target to 1 HP
    public void Reap(Fighter Target)
    {
        
    }

    // steals enemy's life and adds to your own
    public void Harvest(Fighter Target, Fighter Caster)
    {

    }

    // heals a percentage of the target's health, if there are no allies, heals self
    public void Heal(Fighter Target, Fighter Caster)
    {

    }

    // increases outgoing damage for target temporarily
    public void AttackBuff(Fighter Target)
    {
    }

    // increases AP gain for target temporarily
    public void SpeedBuff(Fighter Target)
    {
    }

    // Decreases outgoing damage for target temporarily
    public void AttackDebuff(Fighter Target)
    {
    }

    // Decreases target speed for target temorarily
    public void SpeedDebuff(Fighter Target)
    {
    }

}
