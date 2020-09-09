using System;
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

	private Queue<Card> cardQ;

	[SerializeField]
	float PlayerSize = 0.4f;

	Vector2 startPos = Vector2.zero;

	Vector3 flipPosOn	= Vector3.zero;
	Vector3 flipPosOff	= Vector3.zero;

	protected virtual void Start()
	{
		cardQ = new Queue<Card>();
		mSprite = GetComponent<SpriteRenderer>();
		mMoveRect = FindObjectOfType<MoveRect>();

		flipPosOn	= new Vector3(-PlayerSize, PlayerSize, 1);
		flipPosOff  = new Vector3(PlayerSize, PlayerSize, 1);

		startPos = transform.position;//new Vector2(-1.65f, 0.26f);
	}

	protected virtual void Update()
	{
		FlipSet();
	}
	private void FlipSet()
	{
		if (mMoveRect.playerPos[playerNum, 0] < mMoveRect.playerPos[playerNum == 0 ? 1 : 0, 0])
		{
			transform.localScale = flipPosOn;
			curFlip = true;
		}
		else if (mMoveRect.playerPos[playerNum, 0] > mMoveRect.playerPos[playerNum == 0 ? 1 : 0, 0])
		{
			transform.localScale = flipPosOff;
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

	public int GetSP()
	{
		return sp;
	}

	public void UseCard()
    {
		Card card = cardQ.Dequeue();
		if (card is AttackCard) UseAttackCard(card.cardName);
		else if (card is EnergyCard) UseEnergyCard(card.cardName);
		else if (card is GuardCard) UseGuardCard(card.cardName);
		else if (card is HealCard) UseHealCard(card.cardName);
		else if (card is MoveCard) UseMoveCard(card.cardName);
    }

	public void UseAttackCard(string name)
    {

    }

	public void UseEnergyCard(string name)
    {

    }

	public void UseGuardCard(string name)
    {

    }

	public void UseHealCard(string name)
    {

    }

	public void UseMoveCard(string name)
    {

    }
}
