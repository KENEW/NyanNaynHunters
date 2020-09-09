using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComPlayer : Player
{
    protected override void Start()
    {
        base.Start();

        playerNum = 1;
        UpdatePos();
    }
    private void Update()
    {
        base.Update();
        //Debug
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    SetPlayerPos(-1, 0);
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    SetPlayerPos(1, 0);
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    SetPlayerPos(0, -1);
        //}
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    SetPlayerPos(0, 1);
        //}
    }
}
