﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>, GameEventListener<GameEvent>
{
    private int round;
    public Text roundText;
    //public Slider
    //    playerHelathBar,
    //    playerEnergyBar,
    //    enemyHealthBar,
    //    enemyEnergyBar;
    public CardField cardField;
    public CoolDown coolDown;

    public int PlayerCharID = 0; //Init Value
    public int EnemyCharID = 1;

    public float interval;//한사람 액션 후 다른사람이 액션하기까지 걸리는 시간
    private bool IsPlayerFirst;//누가먼저할건지에 대한 변수, true : 플레이어, false : 적

    private int turn;//한 라운드에 카드사용횟수에 대한 변수, 3회 다 사용하면 1로 리셋

    public int clickedCardCount;

    private int index = 0;//사용할 카드에 대한 인덱스

    private void Awake()
    {
        this.AddGameEventListening<GameEvent>();

    }


    public void OnGameEvent(GameEvent e)
    {
        switch (e.Type)
        {
            case GameEventType.StageStart:
                SoundManager.Instance.PlayBGM("GameStart_BGM");
                round = 0;
                turn = 1;
                clickedCardCount = 0;
                index = 0;

                StartGame();

                break;
        }
    }



    //차례가 끝날때마다 호출되는 함수
    private void StartGame()
    {
        Debug.Log("Start Game");
        GameEvent.Trigger(GameEventType.GameStart);
        
        if (CardClick.canClick == false)
        {
            Card playerCard = cardField.playerHandler[index];
            Card enemyCard = cardField.enemyHandler[index];
            

            int playerPriority = GetPriority(playerCard);
            int enemyPriority = GetPriority(enemyCard);

            if (playerCard is GuardCard && enemyCard is AttackCard)
                IsPlayerFirst = true; //player선
            else if (enemyCard is GuardCard && playerCard is AttackCard)
                IsPlayerFirst = false; //enemy선
            else if (playerPriority < enemyPriority)
                IsPlayerFirst = true;
            else if (playerPriority >= enemyPriority)
                IsPlayerFirst = false;

            if (turn >= 1 && turn <= 3)
            {
                StartCoroutine(PlayerAction());
            }
                
            else
                CardClick.canClick = true;
        }
        else
        {
            if (CheckGame())
            {
                return;
            }
            coolDown.Restart(); //쿨타임 재시작
            turn = 1;
            round += 1;
            index = 0;
            roundText.text = "Round : " + round.ToString();
            if (round > 1) clickedCardCount += 1;//추가로 하나 더

            for (int i = 0; i < cardField.cardPos.Length; i++)
            {
                var cc = cardField.cardPos[i].GetComponent<CardComponent>();
                if (cc.GetIsUsed() == true) cc.OnCard();
            }

            //if (cardField.cardList.Count < cardField.cardPos.Length)
            //{
            //    Card card2 = CardManager.Instance.GetRandomCard();
            //    if (CardManager.Instance.MaxCountCheck(card2, cardField.cardList))
            //    {
            //        cardField.AddCard(card2);
            //    }
            //}

            clickedCardCount = 0;

            StartCoroutine(CardSelectTime());
            
        }
    }

    IEnumerator CardSelectTime()
    {
        while (true)
        {
            if (coolDown.GetSliderValue() <= 0f) break;
            yield return new WaitForSeconds(1f);
        }
        CardClick.canClick = false;
        //적이 카드를 사용한 후 랜덤카드 바로 세팅
        cardField.enemyHandler.Add(CardManager.Instance.GetRandomCard());
        cardField.enemyHandler.Add(CardManager.Instance.GetRandomCard());
        cardField.enemyHandler.Add(CardManager.Instance.GetRandomCard());
        cardField.UpdateHandler();
        StartGame();
    }

    IEnumerator PlayerAction()
    {
        Card card;

        float delayTime = 0f;

        var firstCharacter  = IsPlayerFirst ? PlayerManager.Instance.Player : PlayerManager.Instance.Enemy;
        var secondCharacter = IsPlayerFirst ? PlayerManager.Instance.Enemy : PlayerManager.Instance.Player;

        var firstCardHandler  = IsPlayerFirst ? cardField.playerHandler : cardField.enemyHandler;
        var secondCardHandler = IsPlayerFirst ? cardField.enemyHandler : cardField.playerHandler;


        card = firstCardHandler[index];
        delayTime = firstCharacter.UseCard(card);
        firstCardHandler.Remove(card);
        cardField.UpdateHandler();
        yield return new WaitForSeconds(delayTime);



        card = secondCardHandler[index];
        delayTime = secondCharacter.UseCard(card);
        secondCardHandler.Remove(card);
        cardField.UpdateHandler();
        yield return new WaitForSeconds(delayTime);

        
        while (true)
        {
            if (coolDown.GetSliderValue() <= 0f)
            {
                break;
            }
            
            yield return null;
        }

        yield return new WaitForSeconds(GameSetting.CardWaitTime);

        turn++;
        Debug.Log("Turn : " + turn);
        if (turn > 3) CardClick.canClick = true;
        StartGame();

    }

    private float GetInterval(Card card)
    {
        float interval = 0;
        if (card is AttackCard) interval = TBL_GAME_SETTING.GetEntity(0).AttackCardTime;
        else if (card is EnergyCard) interval = TBL_GAME_SETTING.GetEntity(0).EnergyCardTime;
        else if (card is GuardCard) interval = TBL_GAME_SETTING.GetEntity(0).GuardCardTime;
        else if (card is HealCard) interval = TBL_GAME_SETTING.GetEntity(0).HealCardTime;
        else if (card is MoveCard) interval = TBL_GAME_SETTING.GetEntity(0).MoveCardTime;
        return interval;
    }

    private bool CheckGame()
    {
        bool b = true;
        int playerHP = PlayerManager.Instance.Player.GetHP();
        int enemyHP = PlayerManager.Instance.Enemy.GetHP();
        if (playerHP == 0 && enemyHP == 0) Debug.Log("Drawwwwwwww");
        else if (playerHP == 0) Debug.Log("Losssssssssse");
        else if (enemyHP == 0) Debug.Log("Winnnnnnnnnnn");
        else b = false;

        return b;
    }

    private int GetPriority(Card card)
    {
        int priority;
        if (card is MoveCard) priority = 1;
        else if (card is EnergyCard) priority = 2;
        else if (card is GuardCard) priority = 3;
        else if (card is HealCard) priority = 4;
        else if (card is AttackCard) priority = 5;
        else if (card is EmptyCard) priority = 0;
        else priority = 6;
        return priority;
    }

    private void NextRound()
    {
        
    }

    public void GetCharIndex(int player = 0, int enemy = 1)
    {
        PlayerCharID = player;
        EnemyCharID = enemy;
    }
}
