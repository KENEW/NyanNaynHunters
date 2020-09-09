using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComPlayer : Player
{
    protected void Awake()
    {
        hp = TBL_STAGE.GetEntity(0).EnemyHP;
        sp = TBL_STAGE.GetEntity(0).EnemyEnergy;
    }

    protected override void Start()
    {
        base.Start();

        playerNum = 1;
    }
}
