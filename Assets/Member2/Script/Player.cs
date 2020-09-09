using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
	MoveRect mMoveRect = null;

	SpriteRenderer mSprite = null;

	protected int playerNum = 0;
	protected int hp = 100;
	protected int sp = 100;

	protected bool curFlip = false;

	public float gapTile = 0.79f;

	Vector2 startPos = Vector2.zero;

	protected virtual void Start()
	{
		mSprite = GetComponent<SpriteRenderer>();
		mMoveRect = FindObjectOfType<MoveRect>();

		startPos = new Vector2(transform.position.x, transform.position.y);
		UpdatePos();
	}

	protected void FlipSet()
	{
		if (mMoveRect.playerPos[playerNum, 0] <= mMoveRect.playerPos[playerNum == 0 ? 1 : 0, 0])
		{
			mSprite.flipX = true;
			curFlip = true;
		}
		else
		{
			mSprite.flipX = false;
			curFlip = false;
		}
	}

	protected void SetPlayerPos(int w = 0, int h = 0)
	{
		if ((mMoveRect.playerPos[playerNum, 0] + w <= mMoveRect.RectPos_x) && (mMoveRect.playerPos[playerNum, 0] + w != -1))
		{
			mMoveRect.playerPos[playerNum, 0] += w;
			UpdatePos();
		}
		if ((mMoveRect.playerPos[playerNum, 1] + h <= mMoveRect.RectPos_y) && (mMoveRect.playerPos[playerNum, 1] + h != -1))
		{
			mMoveRect.playerPos[playerNum, 1] += h;
			UpdatePos();
		}
	}

	protected void UpdatePos()
	{
		transform.position = new Vector2(startPos.x + (mMoveRect.playerPos[playerNum, 0] * gapTile),
		startPos.y + (mMoveRect.playerPos[playerNum, 1] * gapTile));
		FlipSet();
	}

	public int GetHP()
	{
		return hp;
	}

	public int SetHP()
	{
		return sp;
	}
}
