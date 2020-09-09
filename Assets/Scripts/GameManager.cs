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
        //if(player.hp <=0 && enemy.hp<=0) draw
        //else if(player.hp<=0 && enemy.hp>0) win
        //else lose
    }
}
