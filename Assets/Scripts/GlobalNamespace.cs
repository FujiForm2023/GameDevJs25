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

    public enum KingdomEnum
    {
        RED = 0,
        BLUE = 1
    }   

    // Kingdom class representing each kingdom in the game
    public class Kingdom
    {
        KingdomEnum kingdomEnum;
        public int hp;
        public int def;
        public int atk;
        public int eco;

        public Kingdom(KingdomEnum KEnum)
        {
            kingdomEnum = KEnum;
            hp = 100;
            def = 4;
            atk = 10;
            eco = 10;
        }

        public void AddHp(int amount)
        {
            hp += amount;
            if (hp > 100)
            {
                hp = 100;
            }
        }
        public void AddDef(int amount)
        {
            def += amount;
            if (def > 100)
            {
                def = 100;
            }
        }
        public void AddAtk(int amount)
        {
            atk += amount;
            if (atk > 100)
            {
                atk = 100;
            }
        }
        public void AddEco(int amount)
        {
            eco += amount;
            if (eco > 100)
            {
                eco = 100;
            }
        }

        public void SubHp(int amount)
        {
            hp -= amount;
            if (hp < 0)
            {
                hp = 0;
            }
        }
        public void SubDef(int amount)
        {
            def -= amount;
            if (def < 0)
            {
                def = 0;
            }
        }
        public void SubAtk(int amount)
        {
            atk -= amount;
            if (atk < 0)
            {
                atk = 0;
            }
        }
        public void SubEco(int amount)
        {
            eco -= amount;
            if (eco < 0)
            {
                eco = 0;
            }
        }
    }
}