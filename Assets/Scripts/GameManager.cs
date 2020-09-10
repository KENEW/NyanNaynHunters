using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
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
    private bool sequence;//누가먼저할건지에 대한 변수, true : 플레이어, false : 적

    private void Start()
    {
        DontDestroyOnLoad(this);
        round = 0;
        //playerHelathBar.maxValue = player.GetHP();
        //playerHelathBar.value = playerHelathBar.maxValue;
        //playerEnergyBar.maxValue = player.GetSP();
        //playerEnergyBar.value = playerEnergyBar.maxValue;
        //enemyHealthBar.maxValue = enemy.GetHP();
        //enemyHealthBar.value = enemyHealthBar.maxValue;
        //enemyEnergyBar.maxValue = enemy.GetSP();
        //enemyEnergyBar.value = enemyEnergyBar.maxValue;

        StartGame();
    }




    //차례가 끝날때마다 호출되는 함수
    private void StartGame()
    {
        Debug.Log("Start Game");
        coolDown.Restart(); //쿨타임 재시작
        Card playerCard = cardField.playerHandler.Peek();
        Card enemyCard = cardField.enemyHandler.Peek();

        int playerPriority = GetPriority(playerCard);
        int enemyPriority = GetPriority(enemyCard);

        if(playerCard is GuardCard && enemyCard is AttackCard)
            sequence = true; //player선
        else if(enemyCard is GuardCard && playerCard is AttackCard)
            sequence = false; //enemy선
        else if (playerPriority < enemyPriority)
            sequence = true;
        else if(playerPriority >= enemyPriority)
            sequence = false;

        StartCoroutine(PlayerAction());
    }

    IEnumerator PlayerAction()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Action Start");
        Debug.Log("Sequence" + sequence);

        Card playerCard;

        if (sequence == true)
        {
            playerCard = cardField.playerHandler.Dequeue();
            PlayerManager.Instance.Player.UseCard(playerCard);
        }
        else
        {
            playerCard = cardField.enemyHandler.Dequeue();
            PlayerManager.Instance.Enemy.UseCard(playerCard);
        }
            
        cardField.UpdateHandler();

        interval = GetInterval(playerCard);
        if (interval == 0) Debug.LogError(playerCard);
        yield return new WaitForSeconds(interval);

        //몇초 뒤
        Debug.Log("Sequence" + sequence);
        if (sequence == true)
        {
            playerCard = cardField.enemyHandler.Dequeue();
            PlayerManager.Instance.Enemy.UseCard(playerCard);
        }
        else
        {
            playerCard = cardField.playerHandler.Dequeue();
            PlayerManager.Instance.Player.UseCard(playerCard);
        }
        
        cardField.UpdateHandler();
        interval = GetInterval(playerCard);
        yield return new WaitForSeconds(interval);

        Debug.Log("Action end");

        while (true)
        {
            if (coolDown.GetSliderValue() <= 0f) break;
            yield return null;
        }

        yield return new WaitForSeconds(GameSetting.CardWaitTime);

        //적이 카드를 사용한 후 랜덤카드 바로 세팅
        cardField.enemyHandler.Enqueue(CardManager.Instance.GetRandomCard());
        cardField.UpdateHandler();

        if (CheckGame())
        {
            StopCoroutine(PlayerAction());
        }
        else
        {
            StartGame();
        }

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
        else priority = 6;
        return priority;
    }

    private void NextRound()
    {
        round++;
        roundText.text = "Round : " + round.ToString();
    }

    public void GetCharIndex(int player = 0, int enemy = 1)
    {
        PlayerCharID = player;
        EnemyCharID = enemy;
    }
}
