using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    private int playerMoneyChange = 0;

    public Text MoneyTotalCount;
    public Text MoneyBalanceCount;
    public Text MoneyTransactionCount;

    void Update()
    {
        playerMoneyChange = 100;
        MoneyTotalCount.GetComponent<Text>().text = "$" + playerMoneyChange.ToString();
    }

    void updateScore(/*BoardTile[] playerOwnedBoardTiles, BoardTile playerCurrentBoardTile, Player player*/)
    {
        /*playerMoneyChange += playerCurrentBoardTile.properties.taxes;
        MoneyTransactionText.text += "\\n - $" + playerCurrentBoardTile.properties.taxes;

        foreach (BoardTile tile in playerOwnedBoardTiles)
        {
            if (tile.properties.taxes != null && tile.properties.taxes != 0)
            {
                playerMoneyChange += tile.properties.taxes;
                MoneyTransactionText.text += "\\n + $" + tile.properties.taxes;
            }
        }

        MoneyBalanceCount.text = "$" + playerMoneyChange.ToString();
        MoneyTotalCount.text = player.Money.ToString();*/
    }
}
