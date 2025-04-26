using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections.Generic;
using GlobalNamespace;
public class WarManager : MonoBehaviour
{
    private PlayableDirector StartAnimation;
    private Kingdom RedKingdom = new Kingdom(KingdomEnum.RED);
    private Kingdom BlueKingdom = new Kingdom(KingdomEnum.BLUE);
    private int money = 1000;
    private int gold = 12;
    private int iron = 12;
    private int wood = 12;
    private int stone = 12;
    private int turn = 1;
    private int turnEnd = 15;
    private int stamina = 15;

    // Set Kingdom Trade
    private KingdomEnum tradeKingdom;
    public void KingdomTrade(bool kingdom)
    {
        if (kingdom)
        {
            tradeKingdom = KingdomEnum.RED;
        }
        else
        {
            tradeKingdom = KingdomEnum.BLUE;
        }
    }

    // Turn on and off the trade button
    [SerializeField]
    private GameObject OVERLAY_UI;
    [SerializeField]
    private TMP_Text TRADE_TEXT;
    private bool trade = false;
    public void TradeButton()
    {
        trade = !trade;
        if (trade)
        {
            OVERLAY_UI.SetActive(true);
            TRADE_TEXT.text = "CLOSE";
        }
        else
        {
            OVERLAY_UI.SetActive(false);
            TRADE_TEXT.text = "TRADE";
        }
        TurnEnd();
    }

    // Get All Numbers (Because bad UI design practices)
    [SerializeField]
    private TMP_Text TurnText;
    [SerializeField]
    private TMP_Text Gold_1;
    [SerializeField]
    private TMP_Text Wood_1;
    [SerializeField]
    private TMP_Text Stone_1;
    [SerializeField]
    private TMP_Text Iron_1;
    [SerializeField]
    private TMP_Text Gold_2;
    [SerializeField]
    private TMP_Text Wood_2;
    [SerializeField]
    private TMP_Text Stone_2;
    [SerializeField]
    private TMP_Text Iron_2;
    [SerializeField]
    private TMP_Text Money_1;
    [SerializeField]
    private TMP_Text Money_2;
    [SerializeField]
    private TMP_Text Stamina_1;
    [SerializeField]
    private TMP_Text Stamina_2;

    // Kingdom stats
    [SerializeField]
    private Image Red_HP;
    [SerializeField]
    private Image Red_DEF;
    [SerializeField]
    private Image Red_ATK;
    [SerializeField]
    private Image Red_ECO;
    [SerializeField]
    private Image Blue_HP;
    [SerializeField]
    private Image Blue_DEF;
    [SerializeField]
    private Image Blue_ATK;
    [SerializeField]
    private Image Blue_ECO;
    
    // Update UI
    public void UpdateUI()
    {
        if (turn % 10 == 1)
        {
            TurnText.text = ""+turn + "st Turn";
        } else if (turn % 10 == 2)
        {
            TurnText.text = ""+turn + "nd Turn";
        } else if (turn % 10 == 3)
        {
            TurnText.text = ""+turn + "rd Turn";
        } else
        {
            TurnText.text = ""+turn + "th Turn";
        }
        Gold_1.text = "" + gold;
        Wood_1.text = "" + wood;
        Stone_1.text = "" + stone;
        Iron_1.text = "" + iron;
        Gold_2.text = "" + gold;
        Wood_2.text = "" + wood;
        Stone_2.text = "" + stone;
        Iron_2.text = "" + iron;
        Money_1.text = "" + money;
        Money_2.text = "" + money;
        Stamina_1.text = "" + stamina +"/15";
        Stamina_2.text = "" + stamina +"/15";

        Red_HP.fillAmount = (float)RedKingdom.hp / 100;
        Red_DEF.fillAmount = (float)RedKingdom.def / 100;
        Red_ATK.fillAmount = (float)RedKingdom.atk / 100;
        Red_ECO.fillAmount = (float)RedKingdom.eco / 100;
        Blue_HP.fillAmount = (float)BlueKingdom.hp / 100;
        Blue_DEF.fillAmount = (float)BlueKingdom.def / 100;
        Blue_ATK.fillAmount = (float)BlueKingdom.atk / 100;
        Blue_ECO.fillAmount = (float)BlueKingdom.eco / 100;
    }

    // Calibrate with load
    void Awake()
    {
        UpdateUI();
        StartAnimation = GetComponent<PlayableDirector>();
    }

    // Sell Items!!!
    public void SellIron()
    {
        if (stamina >= 2 && iron > 0)
        {
            stamina -= 2;
        }
        else
        {
            return;
        }

        if (tradeKingdom == KingdomEnum.RED)
        {
                iron--;
                money += 13 * RedKingdom.eco;
                RedKingdom.AddAtk(3);
                RedKingdom.SubEco(1);
                UpdateUI();
        }
        else
        {
                iron--;
                money += 13 * BlueKingdom.eco;
                BlueKingdom.AddAtk(3);
                BlueKingdom.SubEco(1);
                UpdateUI();

        }
    }

    public void SellWood()
    {
        if (stamina >= 1 && wood > 0)
        {
            stamina--;
        }
        else
        {
            return;
        }
        if (tradeKingdom == KingdomEnum.RED)
        {

                wood--;
                money += RedKingdom.eco;
                UpdateUI();

        }
        else
        {

                wood--;
                money += BlueKingdom.eco;
                UpdateUI();

        }
    }

    public void SellStone()
    {
        if (stamina >= 1 && stone > 0)
        {
            stamina--;
        }
        else
        {
            return;
        }
        if (tradeKingdom == KingdomEnum.RED)
        {

                stone--;
                money += 2 * RedKingdom.eco;
                RedKingdom.AddDef(1);
                RedKingdom.AddHp(1);
                BlueKingdom.SubEco(2);
                UpdateUI();

        }
        else
        {

                stone--;
                money += 2 * BlueKingdom.eco;
                BlueKingdom.AddDef(1);
                BlueKingdom.AddHp(1);
                BlueKingdom.SubEco(2);
                UpdateUI();

        }
    }

    public void SellGold()
    {
        if (stamina >= 3 && gold > 0)
        {
            stamina -= 3;
        }
        else
        {
            return;
        }
        if (tradeKingdom == KingdomEnum.RED)
        {

                gold--;
                money += 8 * RedKingdom.eco;
                RedKingdom.AddEco(5);
                UpdateUI();

        }
        else
        {

                gold--;
                money += 8 * BlueKingdom.eco;
                BlueKingdom.AddEco(5);
                UpdateUI();

        }
    }

    // Turns end
    [SerializeField]
    private PlayableAsset WarTime;
    [SerializeField]
    private PlayableAsset TradeTime;
    [SerializeField]
    private PlayableAsset GameOver;
    [SerializeField]
    private PlayableAsset Victory;
    public void TurnEnd()
    {
        if (stamina != 0) return;
        StartAnimation.Play(WarTime);
        UpdateUI();
    }

    // Battle
    int TEMP_ATK;
    int TEMP_DEF;
    int TEMP_HP;
    int TEMP_ECO;
    public void Attack()
    {
        TEMP_ATK = BlueKingdom.atk;
        TEMP_DEF = BlueKingdom.def;
        TEMP_HP = BlueKingdom.hp;
        TEMP_ECO = BlueKingdom.eco;

        BlueKingdom.SubHp(Mathf.Max(RedKingdom.atk - BlueKingdom.def/2, 1));
        BlueKingdom.SubDef(RedKingdom.atk / 2);
        BlueKingdom.SubAtk(RedKingdom.atk / 3);
        BlueKingdom.SubEco(Mathf.Max(RedKingdom.atk / 2, 1));

        RedKingdom.SubHp(Mathf.Max(TEMP_ATK - RedKingdom.def/2, 1));
        RedKingdom.SubDef(TEMP_ATK / 2);
        RedKingdom.SubAtk(TEMP_ATK / 3);
        RedKingdom.SubEco(Mathf.Max(TEMP_ATK / 2, 1));
        UpdateUI();
    }

    // Turns start
    public void TurnStart()
    {
        if (turn == turnEnd)
        {
            StartAnimation.Play(Victory);
            return;
        }

        if (RedKingdom.hp <= 0 || BlueKingdom.hp <= 0)
        {
            StartAnimation.Play(GameOver);
            return;
        }
        StartAnimation.Play(TradeTime);
        turn++;
        stamina = 15;
        wood += 2; // +2
        stone += 1; // +2
        iron += 4; // +8
        gold += 1; // +3
        UpdateUI();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
