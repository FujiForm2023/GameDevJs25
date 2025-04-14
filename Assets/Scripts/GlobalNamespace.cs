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
        public Dictionary<Resources, int> ResourceCount { get; set; }

        public Inventory()
        {
            ResourceCount = new Dictionary<Resources, int>();
            foreach (Resources resource in Enum.GetValues(typeof(Resources)))
            {
                ResourceCount[resource] = 0;
            }
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
        public string Name { get; set; }
        public Resources MainResource { get; set; }
        public int Population { get; set; }
        public int Money { get; set; }
        public Inventory Inventory { get; set; }
        public List<Army> Armies { get; set; }
        
        // Constructor for Kingdom class
        public Kingdom(string name, Resources mainResource, int population, int money, Inventory inventory, List<Army> armies)
        {
            Name = name;
            MainResource = mainResource;
            Population = population;
            Money = money;
            Inventory = inventory;
            Armies = armies;

            foreach (Army army in Armies)
            {
                army.Owner = this;
            }
        }

        // Add resources to the kingdom's inventory
        public void AddResource(Resources resource, int amount)
        {
            if (Inventory.ResourceCount.ContainsKey(resource))
            {
                Inventory.ResourceCount[resource] += amount;
            }
            else
            {
                Inventory.ResourceCount[resource] = amount;
            }
        }

        // Remove resources from the kingdom's inventory
        public void RemoveResource(Resources resource, int amount)
        {
            if (Inventory.ResourceCount.ContainsKey(resource) && Inventory.ResourceCount[resource] >= amount)
            {
                Inventory.ResourceCount[resource] -= amount;
            }
            else
            {
                Debug.Log("Not enough resources to remove.");
            }
        }

        // Turn ended
        public void EndTurn()
        {
            // Calculate resources based on population and main resource
            int SumOfResources = Inventory.ResourceCount.Values.Sum();

            // Add resources based on population and main resource
            foreach (Resources resource in Enum.GetValues(typeof(Resources)))
            {
                if (resource == MainResource)
                {
                    AddResource(resource, Population / 10);
                }
                else
                {
                    AddResource(resource, Population / 20);
                }
            }

            // Add money based on population and main resource
            if (MainResource == Resources.Gold)
            {
                Money += Population / 5;
            }
            else
            {
                Money += Population / 20;
            }

            // Add population based on resources
            Population += Inventory.ResourceCount.Values.Sum() / 100;

            // Player turn begins
        }

    }
}