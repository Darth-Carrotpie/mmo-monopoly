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

    void Start()
    {
        EventManager.StartListening(EventName.UI.UpdWealth(), UpdateMoneyTotal);
        EventManager.StartListening(EventName.UI.UpdWealth(), UpdateMoneyBalance);
        EventManager.StartListening(EventName.UI.UpdWealth(), UpdateMoneyGain);
        EventManager.StartListening(EventName.UI.UpdWealth(), UpdateMoneyLoss);
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

    void UpdateMoneyGain(GameMessage msg)
    {
        FadeIn(MoneyGainCount.GetComponent<Graphic>()); 

        MoneyGainCount.GetComponent<Text>().text = "+ $" + msg.count.ToString();

        StartCoroutine(Waiter());
        FadeOut(MoneyGainCount.GetComponent<Graphic>());
    }

    void UpdateMoneyLoss(GameMessage msg)
    {
        MoneyLossCount.GetComponent<Text>().text = "+ $" + msg.count.ToString();
    }

    void UpdateScoreCount(GameMessage msg)
    {
        ScoreCount.GetComponent<Text>().text = "Score " + msg.position.ToString();
    }

    void UpdateTurnRollCount(GameMessage msg)
    {
        TurnCount.GetComponent<Text>().text = "Turn " + msg.count.ToString();
        RollCount.GetComponent<Text>().text = "Roll " + msg.roll.ToString();
    }

    void UpdateLeaderboard(GameMessage msg)
    {
        LeaderboardText.GetComponent<Text>().text = msg.position.ToString();
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
    }

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
