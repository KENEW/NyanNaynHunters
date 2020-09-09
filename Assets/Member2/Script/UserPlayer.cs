using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{

    protected void Awake()
    {
        hp = TBL_GAME_SETTING.GetEntity(0).PlayerStartHP;
        sp = TBL_GAME_SETTING.GetEntity(0).PlayerStartEnergy;
    }

    protected override void Start()
    {
        base.Start();

        playerNum = 0;
    }

	private void Update()
	{
        //Debug
		if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetPlayerPos(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetPlayerPos(1, 0);
        }
    }
}
