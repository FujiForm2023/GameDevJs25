using UnityEngine;
using System;
using System.Collections.Generic;
using GlobalNamespace;
public class WarArea : MonoBehaviour
{
    public string areaLabelName = "Area Name";
    public int troopCapacity; //5 ~ 100
    public int troopNum; //0 ~ 100
    public int troopAttackPower = 1;
    public int troopDefencePower = 1;
    [SerializeField] [Range(-10,10)] public int aggressionLevel = 0;
    public int rangeToCapital; //for decision making later
    public List<GameObject> neighbors;
    public KingdomEnum rulingKingdom;

    public List<WarArea> GetAdjacentWarAreas(bool findAllies = true)
    {
        List<WarArea> adjAreas = new List<WarArea>();
        foreach(var n in neighbors)
        {
            WarArea nWarArea = n.GetComponent<WarArea>();
            if((nWarArea.rulingKingdom == rulingKingdom) == findAllies)
            {
                adjAreas.Add(nWarArea);
            }
        }
        return adjAreas;
    }

    public void TransferTroops(WarArea dest, int troopCount)
    {
        troopCount = Math.Min(troopCount, troopNum - 1);
        troopCount = Math.Min(troopCount, dest.troopCapacity - dest.troopNum);
        dest.troopNum += troopCount;
        troopNum -= troopCount;
    }

    public void AttackNeighbor(WarArea dest, int troopCount)
    {
        int attackerCount = Math.Min(troopCount, troopNum - 1);
        troopNum -= attackerCount;

        bool attackerAttacking = true;
        while (attackerCount > 0 && dest.troopNum > 0)
        {
            if (attackerAttacking)
            {
                attackerCount -= dest.troopNum * dest.troopDefencePower *
                    UnityEngine.Random.Range(1, 5) / 10;
            }
            else
            {
                dest.troopNum -= attackerCount * troopAttackPower *
                    UnityEngine.Random.Range(1, 5) / 10;
            }
            Debug.LogFormat("Atk: {0} VS Def: {1}", attackerCount, dest.troopNum);
            attackerAttacking = !attackerAttacking;
        }
        if (dest.troopNum <= 0)
        {
            dest.rulingKingdom = rulingKingdom;
            dest.troopNum = attackerCount;
        }
    }

    public void TurnDecision()
    {
        var friendlyAreas = GetAdjacentWarAreas(true);
        var enemyAreas = GetAdjacentWarAreas(false);
        // todo: update this to use aggression maybe? going to add complexity later
        if (enemyAreas.Count == 0) //transfer troops
        {
            int movedTroops = (int)(troopNum * 0.5f); //0.5 arbitrary for now
            List<WarArea> dests = new List<WarArea>();
            foreach (var f in friendlyAreas) if(f.rangeToCapital > rangeToCapital) dests.Add(f);
            int movedTroopsPerArea = movedTroops / dests.Count;
            foreach (var d in dests) TransferTroops(d, movedTroopsPerArea);
            return;
        }
        WarArea highestPriority = enemyAreas[0];
        foreach(var e in enemyAreas)
        {
            if (e.troopNum < highestPriority.troopNum) highestPriority = e;
        }
        if (highestPriority.troopNum < (int)(troopNum * 0.8f)) //if area confident to win
        {
            AttackNeighbor(highestPriority, (int)(troopNum * 0.8f));
            return;
        } else { //if not confident to win, small skirmish
            AttackNeighbor(highestPriority, (int)(troopNum * 0.3f));
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
