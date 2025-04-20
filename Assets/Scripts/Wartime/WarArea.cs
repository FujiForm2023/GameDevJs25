using UnityEngine;
using System;
using System.Collections.Generic;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
