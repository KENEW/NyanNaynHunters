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
    public Player player,enemy;
    public CardField cardField;
    public CoolDown coolDown;

    public int PlayerCharID = 0; //Init Value
    public int EnemyCharID = 1;

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
        coolDown.Restart();
    }


    //라운드 1,2...마다 호출되는 게임시작 함수
    private void StartGame()
    {
        if (coolDown.GetSliderValue() <= 0f) //쿨타임이 끝났다면
        {
            coolDown.Restart(); //쿨타임 재시작
        }
    }

    public void GetCharIndex(int player = 0, int enemy = 1)
    {
        PlayerCharID = player;
        EnemyCharID = enemy;
    }
}
