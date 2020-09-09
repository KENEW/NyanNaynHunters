using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{
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
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetPlayerPos(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetPlayerPos(0, 1);
        }
    }
}
