using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardSetting
{
    public static int   MoveCardStartCount  => TBL_CARD_SETTINNG.GetEntity(0).MoveCardStartCount;
    public static int   MoveCardMaxCount    => TBL_CARD_SETTINNG.GetEntity(0).MoveCardMaxCount;
    public static float MoveCardDropPercent => TBL_CARD_SETTINNG.GetEntity(0).MoveCardDropPercent;
    
    public static int   AttackCardStartCount  => TBL_CARD_SETTINNG.GetEntity(0).AttackCardStartCount;
    public static int   AttackCardMaxCount    => TBL_CARD_SETTINNG.GetEntity(0).AttackCardMaxCount;
    public static float AttackCardDropPercent => TBL_CARD_SETTINNG.GetEntity(0).AttackCardDropPercent;
    
    public static int   GuardCardStartCount  => TBL_CARD_SETTINNG.GetEntity(0).GuardCardStartCount;
    public static int   GuardCardMaxCount    => TBL_CARD_SETTINNG.GetEntity(0).GuardCardMaxCount;
    public static float GuardCardDropPercent => TBL_CARD_SETTINNG.GetEntity(0).GuardCardDropPercent;
    
    public static int   EnergyCardStartCount  => TBL_CARD_SETTINNG.GetEntity(0).EnergyCardStartCount;
    public static int   EnergyCardMaxCount    => TBL_CARD_SETTINNG.GetEntity(0).EnergyCardMaxCount;
    public static float EnergyCardDropPercent => TBL_CARD_SETTINNG.GetEntity(0).EnergyCardDropPercent;
    
    public static int   HealCardStartCount  => TBL_CARD_SETTINNG.GetEntity(0).HealCardStartCount;
    public static int   HealCardMaxCount    => TBL_CARD_SETTINNG.GetEntity(0).HealCardMaxCount;
    public static float HealCardDropPercent => TBL_CARD_SETTINNG.GetEntity(0).HealCardDropPercent;
}
