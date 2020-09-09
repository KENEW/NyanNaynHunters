using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int round;
    public Text roundText;
    public Slider
        playerHelathBar,
        playerEnergyBar,
        enemyHealthBar,
        enemyEnergyBar;
    public Player player,enemy;
    public CardField cardField;
    public CoolDown coolDown;
    private bool endAnimation; // 애니메이션이 끝나면 다음카드 사용하기, true : 애니메이션 끝남, false : 아직 안끝남

    private void Start()
    {
        round = 0;
        playerHelathBar.maxValue = player.GetHP();
        playerHelathBar.value = playerHelathBar.maxValue;
        playerEnergyBar.maxValue = player.GetSP();
        playerEnergyBar.value = playerEnergyBar.maxValue;
        enemyHealthBar.maxValue = enemy.GetHP();
        enemyHealthBar.value = enemyHealthBar.maxValue;
        enemyEnergyBar.maxValue = enemy.GetSP();
        enemyEnergyBar.value = enemyEnergyBar.maxValue;

        StartGame();
        coolDown.Restart();
    }

    private void Update()
    {
        if (endAnimation)
        {
            //끝나면 다음카드사용
            coolDown.Restart();
            endAnimation = false;
        }
    }

    //라운드 1,2...마다 호출되는 게임시작 함수
    private void StartGame()
    {
        round++;
        roundText.text = "Round : " + round.ToString();

        if (coolDown.GetSliderValue() <= 0f) //쿨타임이 끝났다면
        {
            //셋팅된 카드 사용
            coolDown.Restart(); //쿨타임 재시작
        }
        //if(player.hp <=0 && enemy.hp<=0) draw
        //else if(player.hp<=0 && enemy.hp>0) win
        //else lose
    }
}
