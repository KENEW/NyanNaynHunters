using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSetting
{
     public static int PlayerStartHP     => TBL_GAME_SETTING.GetEntity(0).PlayerStartHP;

     public static int PlayerStartEnergy => TBL_GAME_SETTING.GetEntity(0).PlayerStartEnergy;

     public static float CardSelectTime  => TBL_GAME_SETTING.GetEntity(0).CardSelectTime;
     
     public static float MoveCardTime     => TBL_GAME_SETTING.GetEntity(0).MoveCardTime;
     public static float AttackCardTime   => TBL_GAME_SETTING.GetEntity(0).AttackCardTime;
     public static float GuardCardTime    => TBL_GAME_SETTING.GetEntity(0).GuardCardTime;
     public static float HealCardTime     => TBL_GAME_SETTING.GetEntity(0).HealCardTime;
     public static float EnergyCardTime   => TBL_GAME_SETTING.GetEntity(0).EnergyCardTime;
     public static float CardWaitTime     => TBL_GAME_SETTING.GetEntity(0).CardWaitTime;

}
