using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSetting
{
     public static int PlayerStartHP     => TBL_GAME_SETTING.GetEntity(0).PlayerStartHP;

     public static int PlayerStartEnergy => TBL_GAME_SETTING.GetEntity(0).PlayerStartEnergy;

     public static float CardSelectTime  => TBL_GAME_SETTING.GetEntity(0).CardSelectTime;
}
