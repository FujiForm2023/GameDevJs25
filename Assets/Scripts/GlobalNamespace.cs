using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace GlobalNamespace
{
    // Use in game state
    public enum GameState
    {
        MainMenu,
        PlayerTurn,
        KingdomTurn,
        GameOver
    }

    // Resources for each kingdom and player
    public enum Resources
    {
        Gold,
        Wood,
        Stone,
        Iron,
        Leather
    }

    // Inventory for each kingdom and player
    public class Inventory
    {
        Dictionary<Resources, int> ResourceCount { get; set; }

        public Inventory()
        {
            ResourceCount = new Dictionary<Resources, int>();
            foreach (Resources resource in Enum.GetValues(typeof(Resources)))
            {
                ResourceCount[resource] = 0;
            }
        }

        // Add resources to the inventory
        public void AddResource(Resources resource, int amount)
        {
            if (ResourceCount.ContainsKey(resource))
            {
                ResourceCount[resource] += amount;
            }
            else
            {
                ResourceCount[resource] = amount;
            }
        }

        // Remove resources from the inventory
        public void RemoveResource(Resources resource, int amount)
        {
            if (ResourceCount.ContainsKey(resource) && ResourceCount[resource] >= amount)
            {
                ResourceCount[resource] -= amount;
            }
            else
            {
                Debug.Log("Not enough resources to remove.");
            }
        }

        // Get total of resources in the inventory
        public int GetTotalResources()
        {
            return ResourceCount.Values.Sum();
        }
    }

    // Army class representing each army in the game
    public class Army
    {
        public Kingdom Owner { get; set; }
        public int Size { get; set; }
        public int Strength { get; set; }
        public int Order { get; set; }
        public Army(int size, int strength, int order)
        {
            Size = size;
            Strength = strength;
            Order = order;
        }
    }

    // Kingdom class representing each kingdom in the game
    public class Kingdom
    {
        public string KingdomName { get; set; }
        public Resources KingdomMainResource { get; set; }
        public int KingdomPopulation { get; set; }
        public int KingdomMoney { get; set; }
        public Inventory KingdomInventory { get; set; }
        public List<Army> KingdomArmies { get; set; }
        
        // Constructor for Kingdom class
        public Kingdom(string name, Resources mainResource, int population, int money, Inventory inventory, List<Army> armies)
        {
            KingdomName = name;
            KingdomMainResource = mainResource;
            KingdomPopulation = population;
            KingdomMoney = money;
            KingdomInventory = inventory;
            KingdomArmies = armies;

            foreach (Army ThisArmy in KingdomArmies)
            {
                ThisArmy.Owner = this;
            }
        }

        // Turn ended
        public void EndTurn()
        {
            // Calculate resources based on population and main resource
            int SumOfResources = KingdomInventory.GetTotalResources();

            // Add resources based on population and main resource
            foreach (Resources resource in Enum.GetValues(typeof(Resources)))
            {
                if (resource == KingdomMainResource)
                {
                    KingdomInventory.AddResource(resource, KingdomPopulation / 10);
                }
                else
                {
                    KingdomInventory.AddResource(resource, KingdomPopulation / 20);
                }
            }

            // Add money based on population and main resource
            if (KingdomMainResource == Resources.Gold)
            {
                KingdomMoney += KingdomPopulation / 5;
            }
            else
            {
                KingdomMoney += KingdomPopulation / 20;
            }

            // Add population based on resources
            KingdomPopulation += SumOfResources / 100;

            // Player turn begins
        }

    }
}