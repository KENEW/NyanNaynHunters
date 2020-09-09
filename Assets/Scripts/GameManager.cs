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
    //public Character player,enemy;
    //public CardField cardField;
    public CoolDown coolDown;

    private void Start()
    {
        round = 1;
        roundText.text = "Round : " + round.ToString();
        //playerHelathBar.maxValue = player.maxHP;
        //playerEnergyBar.maxValue = player.maxEnergy;
        //enemyHealthBar.maxValue = enemy.maxHP;
        //enemyEnergyBar.maxValue = enemy.maxEnergy;
    }

    //라운드 1,2...마다 호출되는 게임시작 함수
    private void StartGame()
    {
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
