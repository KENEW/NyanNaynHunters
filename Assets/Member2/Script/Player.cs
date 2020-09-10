using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using DG.Tweening;
using Spine.Unity;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Slider = UnityEngine.UI.Slider;

public enum PlayerType
{
	User,
	Computer
}

public class Player : MonoBehaviour
{
	protected int hp = 100;
	protected int sp = 100;
	
	private Vector2 m_TilePosition;
	public Vector2 tilePosition => m_TilePosition;

	public int PositionIndex => (int)m_TilePosition.x * TileManager.COL + (int)m_TilePosition.y;

	public PlayerType playerType;

	public Slider hpSlider;
	public Slider spSlider;
	
	public const float LeftXScale = -1;
	public const float RightXScale = 1;

	private int guard;

	private SkeletonAnimation m_SkeletonAnimation;
	
	protected virtual void Awake()
	{
		m_SkeletonAnimation = GetComponent<SkeletonAnimation>();
		
		hp = TBL_GAME_SETTING.GetEntity(0).PlayerStartHP;
		sp = TBL_GAME_SETTING.GetEntity(0).PlayerStartEnergy;

		if (playerType == PlayerType.User)
		{
			hpSlider = GameObject.FindGameObjectWithTag("PlayerHPBar").GetComponent<Slider>();
			spSlider = GameObject.FindGameObjectWithTag("PlayerSPBar").GetComponent<Slider>();
		}
		else
		{
			hpSlider = GameObject.FindGameObjectWithTag("EnemyHPBar").GetComponent<Slider>();
			spSlider = GameObject.FindGameObjectWithTag("EnemySPBar").GetComponent<Slider>();
		}

		hpSlider.maxValue = hp;
		hpSlider.value = hp;
		spSlider.maxValue = sp;
		spSlider.value = sp;

		guard = 0;

	}

	public void SetTilePosition(Vector2 newTilePosition, bool instantly = false)
	{
		m_TilePosition = newTilePosition;

		PlayerManager.Instance.OnPlayerPositionChanged(playerType, instantly);
	}


	public void LeftLerpMove(Vector3 dist, bool instantly = false)
	{
		transform.localScale = new Vector3(LeftXScale, 1, 1);

		if (instantly)
		{
			transform.position = dist;
			return;
		}
		
		if (Vector3.Distance(dist, transform.position) < 0.1) return;
		
		
		
		m_SkeletonAnimation.AnimationState.SetAnimation(0, "Move", true);
		TileManager.Instance.SetColor(m_TilePosition, GameSetting.MoveCardTime * 0.8f);
		transform.DOJump(dist, GameSetting.MoveCardTime * 0.8f, 1, GameSetting.MoveCardTime).OnComplete(() =>
		{
			m_SkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
		});
	}
	
	public void RightLerpMove(Vector3 dist, bool instantly = false)
	{
		transform.localScale = new Vector3(RightXScale, 1, 1);

		if (instantly)
		{
			transform.position = dist;
			return;
		}

		if (Vector3.Distance(dist, transform.position) < 0.1) return;
		
		
		TileManager.Instance.SetColor(m_TilePosition, GameSetting.MoveCardTime * 0.8f);
		m_SkeletonAnimation.AnimationState.SetAnimation(0, "Move", true);
		transform.DOJump(dist, GameSetting.MoveCardTime * 0.8f, 1, GameSetting.MoveCardTime).OnComplete(() =>
		{
			m_SkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
		});
	}

	public int GetHP()
	{
		return hp;
	}

	public int GetSP()
	{
		return sp;
	}


	public void UseCard(Card card)
    {
		if (card is AttackCard) UseAttackCard(((AttackCard)card));
		else if (card is EnergyCard) UseEnergyCard(((EnergyCard)card));
		else if (card is GuardCard) UseGuardCard(((GuardCard)card));
		else if (card is HealCard) UseHealCard(((HealCard)card));
		else if (card is MoveCard) UseMoveCard(((MoveCard)card));
		Debug.Log(name+" Use card");
    }
	


	private bool InTarget(List<int> list, Vector2 targetPosition)
    {
		bool inp = false;

		for (int i=0; i<list.Count; i++)
        {
			Vector2 tilePosition = GetPosition(list[i]);
			if(tilePosition == targetPosition) //스킬범위 안
            {
				inp = true;
            }
        }
		return inp;
    }

	private Vector2 GetPosition(int pos)
    {
		// 스테이지: N(x, y)
		//  0(0,0)   1(0, 1)   2(0, 2)    3(0, 3)
		//  4(1,0)   5(1, 1)   6(1, 2)    7(1, 3)
		//  8(2,0)   9(2, 1)  10(2, 2)   11(2, 3)
		// 12(3,0)  13(3, 1)  14(3, 2)   15(3, 3)


		// 타일 범위
		// 0  1  2
		// 3  4  5
		// 6  7  8

		var playerTilePosition = m_TilePosition;

		Vector2 selectedTile = Vector2.zero;
		switch (pos)
		{
			case 0:
				selectedTile.x = playerTilePosition.x - 1;
				selectedTile.y = playerTilePosition.y - 1;
				break;

			case 1:
				selectedTile.x = playerTilePosition.x;
				selectedTile.y = playerTilePosition.y - 1;
				break;
			case 2:
				selectedTile.x = playerTilePosition.x + 1;
				selectedTile.y = playerTilePosition.y - 1;
				break;
			case 3:
				selectedTile.x = playerTilePosition.x - 1;
				selectedTile.y = playerTilePosition.y;
				break;

			case 4:
				selectedTile.x = playerTilePosition.x;
				selectedTile.y = playerTilePosition.y;
				break;
			case 5:
				selectedTile.x = playerTilePosition.x + 1;
				selectedTile.y = playerTilePosition.y;
				break;
			case 6:
				selectedTile.x = playerTilePosition.x - 1;
				selectedTile.y = playerTilePosition.y + 1;
				break;

			case 7:
				selectedTile.x = playerTilePosition.x;
				selectedTile.y = playerTilePosition.y + 1;
				break;
			case 8:
				selectedTile.x = playerTilePosition.x + 1;
				selectedTile.y = playerTilePosition.y + 1;
				break;

		}

		return selectedTile;
	}

	private void AddHP(int value)
    {
        if (value < 0) //공격받을 시
        {
			value += guard;
			if (value > 0) value = 0;
        }
		hp += value;
		hpSlider.value = hp;
    }

	private void AddSP(int value)
    {
		sp += value;
		spSlider.value = sp;
    }


	// 공격 카드 사용
	private void UseAttackCard(AttackCard card)
	{
		string randomAttackAnimationName = Random.Range(0, 2) == 0 ? "Attack1" : "Attack2";
		var animationTime = m_SkeletonAnimation.Skeleton.Data.FindAnimation(randomAttackAnimationName).Duration;
		
		foreach (var posIndex in card.positions)
		{
			var tilePosition = GetPosition(posIndex);
			TileManager.Instance.SetColor(tilePosition, animationTime * 1.2f);
		}

		
		m_SkeletonAnimation.AnimationState.SetAnimation(0, randomAttackAnimationName, false);
		
		StartCoroutine(  UseAttackCard_Coroutine(card, animationTime));
	}

	private IEnumerator UseAttackCard_Coroutine(AttackCard card, float time)
	{
		yield return new WaitForSeconds(time);
		
		m_SkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);

		Player target;
		if (playerType == PlayerType.User)
			target = PlayerManager.Instance.Enemy;
		else
			target = PlayerManager.Instance.Player;

		if (InTarget(card.positions, target.tilePosition)) //명중하면 true, 빗나가면 false
		{
			target.AddHP(-card.damage);
			target.TakeHitAnimation();
		}

		AddSP(-card.energyCost);
	}
	
	// 에너지 카드 사용
	private void UseEnergyCard(EnergyCard card)
    {
		AddSP(card.energyIncrease);
    }

	// 가드 카드 사용
	private void UseGuardCard(GuardCard card)
    {
		guard = card.damageReduction;
    }
	
	// 힐 카드 사용
	private void UseHealCard(HealCard card)
    {
		AddHP(card.HPIncrease);
		AddSP(card.energyCost);
    }

	// 무브 카드 사용
	public void UseMoveCard(MoveCard cardData)
	{
		Vector2 newPosition;
		
		switch (cardData.moveType)
		{
			case MoveType.Left:
				newPosition = m_TilePosition + new Vector2(0, -cardData.distance);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
			
			case MoveType.Right:
				newPosition = m_TilePosition + new Vector2(0, cardData.distance);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
			
			case MoveType.Up:
				newPosition = m_TilePosition + new Vector2(-cardData.distance, 0);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
			
			case MoveType.Down:
				newPosition = m_TilePosition + new Vector2(cardData.distance, 0);
				if (TileManager.Instance.CanMove(newPosition))
				{
					SetTilePosition(newPosition);
				}
				break;
		}
	}
	
	private void TakeHitAnimation()
	{
		StartCoroutine(TakeHitAnimation_Coroutine());
	}

	private IEnumerator TakeHitAnimation_Coroutine()
	{
		m_SkeletonAnimation.AnimationState.SetAnimation(0, "Damaged", true);

		yield return new WaitForSeconds(0.6f);
		
		m_SkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);
	}
}
