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
        Iron
    }

    // Item that can be used in the game
    public enum Item
    {
        Sword,
        Shield,
        Bow,
        Arrow,
        Armor
    }

    public struct Square
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Square(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public enum KingdomEnum
    {
        RED = 0,
        BLUE = 1
    }   

    // Inventory for each kingdom and player
    public class Inventory
    {
        Dictionary<Resources, int> ResourceCount { get; set; }
        Dictionary<Item, int> ItemCount { get; set; }

        public Inventory()
        {
            ResourceCount = new Dictionary<Resources, int>();
            foreach (Resources resource in Enum.GetValues(typeof(Resources)))
            {
                ResourceCount[resource] = 0;
            }

            ItemCount = new Dictionary<Item, int>();
            foreach (Item item in Enum.GetValues(typeof(Item)))
            {
                ItemCount[item] = 0;
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

        // Add items to the inventory
        public void AddItem(Item item, int amount)
        {
            if (ItemCount.ContainsKey(item))
            {
                ItemCount[item] += amount;
            }
            else
            {
                ItemCount[item] = amount;
            }
        }

        // Remove items from the inventory
        public void RemoveItem(Item item, int amount)
        {
            if (ItemCount.ContainsKey(item) && ItemCount[item] >= amount)
            {
                ItemCount[item] -= amount;
            }
            else
            {
                Debug.Log("Not enough items to remove.");
            }
        }

        // Get total of items in the inventory
        public int GetTotalItems()
        {
            return ItemCount.Values.Sum();
        }
    }

    // Army class representing each army in the game
    public class Army
    {
        public Kingdom ArmyOwner { get; set; }
        public int ArmySize { get; set; }
        public int ArmyStrength { get; set; }
        public int ArmyOrder { get; set; }
        public Square ArmyPosition { get; set; }
        public Army(int size, int strength, int order)
        {
            ArmySize = size;
            ArmyStrength = strength;
            ArmyOrder = order;
        }
    }

    public enum KingdomEnum
    {
        RED = 0,
        BLUE = 1
    }

    // Kingdom class representing each kingdom in the game
    public class Kingdom
    {
        public string KingdomName { get; set; }
        public Resources KingdomMainResource { get; set; }
        public int KingdomPopulation { get; set; }
        public int KingdomMoney { get; set; }
        public int KingdomTechLevel { get; set; }
        public Inventory KingdomInventory { get; set; }
        public List<Army> KingdomArmies { get; set; }
        public Square KingdomPosition { get; set; }
        public bool KingdomPlayerIsOwn { get; set; }
        public bool KingdomIsAngry { get; set; }
        public byte KingdomSuspiciousLevel { get; set; }
<<<<<<< HEAD
        public int KingdomAggressionLevel { get; set; }
        public int KingdomTechLevel { get; set; }
        public KingdomEnum KingdomRule { get; set; } 
=======
        public KingdomEnum KingdomEnum { get; set; }
>>>>>>> 6a4254120ae3fdf7732a5410e85b722b74a94e8b


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
                ThisArmy.ArmyOwner = this;
            }
        }

        public Kingdom(KingdomEnum ke)
        {
            KingdomRule = ke;
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

            // Check if kingdom is suspicious at Player
            if (KingdomSuspiciousLevel > 0)
            {
                KingdomSuspiciousLevel--;
            }

            // Player turn begins
        }

    }

    // Task class representing each tasks in the game
    public class Task
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public int TaskMoneyReward { get; set; }
        public Kingdom TaskFromKingdom { get; set; }
        public Item TaskKingdomRequiredItem { get; set; }
        public int TaskKingdomRequiredItemAmount { get; set; }
        public int TaskTimeToComplete { get; set; }
        public int TaskTimeLeft { get; set; }
    }

    // Player class representing player in the game
    public class Player
    {
        public string PlayerName { get; set; }
        public int PlayerMoney { get; set; }
        public Inventory PlayerInventory { get; set; }
        public List<Task> PlayerTasks { get; set; }
        public Square PlayerPosition { get; set; }
        public Kingdom PlayerKingdom { get; set; }

        // Constructor for Player class
        public Player(string name, int money, Inventory inventory)
        {
            PlayerName = name;
            PlayerMoney = money;
            PlayerInventory = inventory;
            PlayerTasks = new List<Task>();
            PlayerPosition = new Square(0, 0);
            PlayerKingdom = null;
        }

        // Add task to player
        public void AddTask(Task task)
        {
            PlayerTasks.Add(task);
        }

        // Remove task from player
        public void RemoveTask(Task task)
        {
            PlayerTasks.Remove(task);
        }

        // Task completed
        public void CompleteTask(Task task)
        {
            if (PlayerTasks.Contains(task))
            {
                PlayerTasks.Remove(task);
                PlayerMoney += task.TaskMoneyReward;
                PlayerInventory.AddItem(task.TaskKingdomRequiredItem, 1);
                if (task.TaskFromKingdom.KingdomSuspiciousLevel < 3)
                {
                    task.TaskFromKingdom.KingdomSuspiciousLevel = 0;
                }
                else
                {
                    task.TaskFromKingdom.KingdomSuspiciousLevel -= 3;
                }
            }
            else
            {
                Debug.Log("Task not found in player's tasks.");
            }
        }

        // Task failed
        public void FailTask(Task task)
        {
            if (PlayerTasks.Contains(task))
            {
                PlayerTasks.Remove(task);
                task.TaskFromKingdom.KingdomIsAngry = true;
            }
            else
            {
                Debug.Log("Task not found in player's tasks.");
            }
        }
    }
}