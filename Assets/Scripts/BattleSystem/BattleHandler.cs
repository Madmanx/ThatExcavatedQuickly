using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleHandler
{
    public enum EntityTurn { Player, Player2 }
    enum BattleState { Win, Lose, Continue }
    private static EntityTurn currentTurn;

    static BattleState currentState = BattleState.Continue;

    public static EntityTurn CurrentTurn
    {
        get
        {
            return currentTurn;
        }

        set
        {
            switch (value)
            {
                //case EntityTurn.Player:
                //    UIManager.instance.ChangeTurn("Player");
                //    while (GameManager.instance.CurrentPlayer.playerData.playerCards.Count < Player.MAX_CARDS_IN_HAND && GameManager.instance.CurrentPlayer.playerData.playerDeck.Count > 0)
                //    {
                //        GameManager.instance.CurrentPlayer.DrawCard();
                //        UIManager.instance.UpdateHandPlayerPosition();
                //    }
                //    break;
                //case EntityTurn.AI:
                //    UIManager.instance.ChangeTurn("Bob");
                //    while (GameManager.instance.CurrentEnemy.enemyData.playerCards.Count < Player.MAX_CARDS_IN_HAND && GameManager.instance.CurrentEnemy.enemyData.playerDeck.Count > 0)
                //    {
                //        GameManager.instance.CurrentEnemy.DrawCard();
                //        UIManager.instance.UpdateHandEnemyPosition();
                //    }
                //    break;

            }
            currentTurn = value;
        }
    }

    // This function should be call when we want the turn to change
    public static void NextTurn()
    {
        if (currentState != BattleState.Continue)
            return;

        currentState = CheckForBattleEnd();
        if (currentState == BattleState.Continue)
        {
            if (CurrentTurn == EntityTurn.Player2)
            {
                CurrentTurn = EntityTurn.Player;
            }
            else
            {
                CurrentTurn = EntityTurn.Player2;
            }

        }
        else if (currentState == BattleState.Win)
        {
            WinProcess();
        }
        else if (currentState == BattleState.Lose)
        {
            LoseProcess();
        }


    }

    static void Init()
    {

    }



    static BattleState CheckForBattleEnd()
    {
        //if (playerData.playerCards.Count < 3 && playerData.playerDeck.Count == 0
        //    || (OnlyShieldsLeft(playerData.playerCards) && playerData.playerDeck.Count == 0))
        //    return BattleState.Lose;
        //if (enemyData.playerCards.Count < 3 && enemyData.playerDeck.Count == 0
        //    || (OnlyShieldsLeft(enemyData.playerCards) && enemyData.playerDeck.Count == 0))
        //    return BattleState.Win;

        return BattleState.Continue;
    }


    static void WinProcess()
    {
    
    }

    static void LoseProcess()
    {

    }

}