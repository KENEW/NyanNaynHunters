using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRect : MonoSingleton<MoveRect>
{
	//public int[,] RectPos	= new int[4, 3];  //Weight, Height
	public int[,] playerPos = new int[2, 2] { {0, 1}, {3, 1} };

	public int RectPos_x = 3;
	public int RectPos_y = 2;
	
	private void Start()
	{
		//for (int i = 0; i < RectPos.GetLength(0); i++)
		//{
		//	for (int j = 0; j < RectPos.GetLength(1); j++)
		//	{
		//		RectPos[i, j] = 0;
		//	}
		//}
	}
	
	
}