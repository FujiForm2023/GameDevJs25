using UnityEngine;
using System;
using System.Collections.Generic;

public enum KingdomEnum
{
    RED = 0,
    BLUE = 1
}

class Kingdom
{
    KingdomEnum kingdomEnum;
    int aggessionLevel = 0; //-5 ~ 5
    int techLevel = 0; // how frequent the kingdom upgrades by itself
    public Kingdom(KingdomEnum ke)
    {
        kingdomEnum = ke;
    } 
}
public class KingdomManager : MonoBehaviour
{
    Kingdom redKingdom = new(KingdomEnum.RED);
    Kingdom blueKingdom = new(KingdomEnum.BLUE);

    //temporary
    public GameObject testAttacker;
    public GameObject testDefender;
    public bool doAttackDefenceTest;
    void DoBattle(WarArea defender, WarArea attacker, int attackerCount)
    {
        attackerCount = Math.Min(attackerCount, attacker.troopNum - 1);
        attacker.troopNum -= attackerCount;

        bool attackerAttacking = true;
        while (attackerCount > 0 && defender.troopNum > 0)
        {
            if (attackerAttacking)
            {
                attackerCount -= defender.troopNum * defender.troopDefencePower * 
                    UnityEngine.Random.Range(1, 5) / 10;
            }
            else
            {
                defender.troopNum -= attackerCount * attacker.troopAttackPower * 
                    UnityEngine.Random.Range(1, 5) / 10;
            }
            Debug.LogFormat("Atk: {0} VS Def: {1}", attackerCount, defender.troopNum);
            attackerAttacking =! attackerAttacking;
        }
        if (defender.troopNum <= 0)
        {
            defender.rulingKingdom = attacker.rulingKingdom;
            defender.troopNum = attackerCount;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(doAttackDefenceTest)
        {
            WarArea defender = testDefender.GetComponent<WarArea>();
            WarArea attacker = testAttacker.GetComponent<WarArea>();
            DoBattle(defender, attacker, 100);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
