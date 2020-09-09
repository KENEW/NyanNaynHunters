using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRect : MonoSingleton<MoveRect>
{
	public int[,] playerPos = new int[2, 2] { {0, 1}, {2, 1} };

	public int RectPos_x = 3;
	public int RectPos_y = 2;
}