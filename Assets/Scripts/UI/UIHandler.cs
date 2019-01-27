using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text MoneyTotalCount;
    public Text MoneyBalanceCount;
    public Text MoneyTransactionCount;
    public Text TurnCount;
    public Text ScoreCount;
    public Text RollCount;
    public Text LeaderboardText;

    string moneyGainText;
    string moneyLossText;

    void Start()
    {
        EventManager.StartListening(EventName.UI.UpdWealth(), UpdateMoneyTotal);
        EventManager.StartListening(EventName.UI.UpdTransaction(), UpdateTransaction);

        EventManager.StartListening(EventName.System.Turn(), UpdateTurnRollCount);
        EventManager.StartListening(EventName.Player.NewPosition(), UpdateScoreCount);
        EventManager.StartListening(EventName.UI.UpdLeaderboard(), UpdateLeaderboard);

        EventManager.StartListening(EventName.Player.PossibleAction(), UpdateButtons);
    }

    void UpdateMoneyTotal(GameMessage msg)
    {
        MoneyTotalCount.GetComponent<Text>().text = "Money $" + msg.count.ToString();
    }

    void UpdateTransaction(GameMessage msg)
    {
        moneyGainText = "";
        moneyLossText = "";

        int gainSum = 0;
        int lossSum = 0;
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

            if (typeID == "RollDouble" && gain != "")
            {
                moneyGainText += "\n +$" + gain + ". You rolled double!";
            }
            else if (typeID == "PassGo" && gain != "")
            {
                moneyGainText += "\n +$" + gain + ". You passed GO!";
            }
            else if (typeID == "ReceiveRent" && gain != "")
            {
                moneyGainText += "\n +$" + gain + ". You received rent!";
            }
            else if (typeID == "PayRent" && loss != "")
            {
                moneyLossText += "\n -$" + loss + ". You paid rent.";
            }
            else if (typeID == "PayTax" && loss != "")
            {
                moneyLossText += "\n -$" + loss + ". You paid taxes.";
            }
            else if (typeID == "PayBuild" && loss != "")
            {
                moneyLossText += "\n -$" + loss + ". You built property.";
            }

            gainSum += int.Parse(gain);
            lossSum += int.Parse(loss);
        }

        MoneyBalanceCount.GetComponent<Text>().text = "Balance $" + (gainSum + lossSum).ToString();

        FadeIn(MoneyTransactionCount.GetComponent<Graphic>()); 

        MoneyTransactionCount.GetComponent<Text>().text = moneyGainText + "\n \n" + moneyLossText;

        FadeOut(MoneyTransactionCount.GetComponent<Graphic>());
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

    void UpdateButtons(GameMessage msg)
    {
        //msg.possibleAction = 
    }

    void FadeIn(Graphic g)
    {
        g.GetComponent<CanvasRenderer>().SetAlpha(0f);
        g.CrossFadeAlpha(1f, .3f, false);
    }

    void FadeOut(Graphic g)
    {
        g.GetComponent<CanvasRenderer>().SetAlpha(1f);
        g.CrossFadeAlpha(0f, 4f, false);
    }
}
