using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text MoneyTotalCount;
    public Text MoneyBalanceCount;
    public Text MoneyGainCount;
    public Text MoneyLossCount;
    public Text TurnCount;
    public Text ScoreCount;
    public Text RollCount;
    public Text LeaderboardText;

    string moneyGainText;
    string moneyLossText;

    void Start()
    {
        EventManager.StartListening(EventName.UI.UpdWealth(), UpdateMoneyTotal);
        //EventManager.StartListening(EventName.UI.UpdWealth(), UpdateMoneyBalance);
        EventManager.StartListening(EventName.UI.UpdTransaction(), UpdateTransaction);
        EventManager.StartListening(EventName.System.Turn(), UpdateTurnRollCount);
        EventManager.StartListening(EventName.Player.NewPosition(), UpdateScoreCount);
        EventManager.StartListening(EventName.UI.UpdLeaderboard(), UpdateLeaderboard);
    }

    void UpdateMoneyTotal(GameMessage msg)
    {
        MoneyTotalCount.GetComponent<Text>().text = "Money $" + msg.count.ToString();
    }

    void UpdateMoneyBalance(GameMessage msg)
    {
        MoneyBalanceCount.GetComponent<Text>().text = "Balance $" + msg.count.ToString();
    }

    void UpdateTransaction(GameMessage msg)
    {
        /*
            RollDouble = 0,
            PassGo = 1,
            PayRent = 2,
            ReceiveRent = 3,
            PayTax = 4,
            PayBuild = 5,
         */

        foreach (Messages.Transaction transaction in msg.transaction)
        {
            string typeID = transaction.typeId.ToString();
            string gain = transaction.properties.gain.ToString();
            string loss = transaction.properties.loss.ToString();

            if (typeID == "0" && gain != "")
            {
                moneyGainText += "//n+$" + gain + ". You rolled double!";
            }
            else if (typeID == "1" && gain != "")
            {
                moneyGainText += "//n+$" + gain + ". You passed GO!";
            }
            else if (typeID == "3" && gain != "")
            {
                moneyGainText += "//n+$" + gain + ". You received rent!";
            }
            else if (typeID == "2" && loss != "")
            {
                moneyLossText += "//n-$" + loss + ". You paid rent.";
            }
            else if (typeID == "4" && loss != "")
            {
                moneyLossText += "//n-$" + loss + ". You paid taxes.";
            }
            else if (typeID == "5" && loss != "")
            {
                moneyLossText += "//n-$" + loss + ". You built property.";
            }
        }

        FadeIn(MoneyGainCount.GetComponent<Graphic>()); 

        MoneyGainCount.GetComponent<Text>().text = moneyGainText;
        MoneyLossCount.GetComponent<Text>().text = moneyLossText;

        //StartCoroutine(Waiter());
        FadeOut(MoneyGainCount.GetComponent<Graphic>());
    }

    void UpdateScoreCount(GameMessage msg)
    {
        ScoreCount.GetComponent<Text>().text = "Score " + msg.position.ToString();
    }

    void UpdateTurnRollCount(GameMessage msg)
    {
        int rollSum = 0;
        foreach (int rollNumber in msg.roll)
            rollSum += rollNumber;
        RollCount.GetComponent<Text>().text = "Roll " + rollSum.ToString();

        TurnCount.GetComponent<Text>().text = "Turn " + msg.count.ToString();
    }

    void UpdateLeaderboard(GameMessage msg)
    {
        LeaderboardText.GetComponent<Text>().text = msg.position.ToString();
    }

    /*IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
    }*/

    void FadeIn(Graphic g)
    {
        g.GetComponent<CanvasRenderer>().SetAlpha(0f);
        g.CrossFadeAlpha(1f, .3f, false);
    }

    void FadeOut(Graphic g)
    {
        g.GetComponent<CanvasRenderer>().SetAlpha(1f);
        g.CrossFadeAlpha(0f, 3f, false);
    }
}
