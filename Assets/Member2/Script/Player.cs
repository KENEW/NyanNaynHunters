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

public class Player : MonoBehaviour
{
	public int MaxHP;
	public int HP;
	public int MaxEnergy;
	public int Energy;
	
	private Vector2 m_TilePosition;
	public Vector2 tilePosition => m_TilePosition;

	public int PositionIndex => (int)m_TilePosition.x * TileManager.COL + (int)m_TilePosition.y;

	
	
	public const float LeftXScale = -1;
	public const float RightXScale = 1;

	private int guard;

	public bool isPlayer = false;

	private SkeletonAnimation m_SkeletonAnimation;
	
	protected virtual void Awake()
	{
		m_SkeletonAnimation = GetComponent<SkeletonAnimation>();

		guard = 0;

	}
	

	public void InitStat()
	{
		if (isPlayer)
		{
			
			HP = GameSetting.PlayerStartHP;
			MaxHP = HP;
			Energy = GameSetting.PlayerStartEnergy;
			MaxEnergy = Energy;
		}
		else
		{
			HP = TBL_STAGE.GetEntity(0).EnemyHP;
			MaxHP = HP;
			Energy = TBL_STAGE.GetEntity(0).EnemyEnergy;
			MaxEnergy = Energy;
		}
		
		SetSliderValue();
	}
	
	public void SetSliderValue()
	{
		if (isPlayer)
		{
			SliderManager.Instance.PlayerHPSlider.maxValue = MaxHP;
			SliderManager.Instance.PlayerEnergySlider.maxValue = MaxEnergy;
			SliderManager.Instance.PlayerHPSlider.value = HP;
			SliderManager.Instance.PlayerEnergySlider.value = Energy;
		}
		else
		{
			SliderManager.Instance.EnemyHPSlider.maxValue = MaxHP;
			SliderManager.Instance.EnemyEnergySlider.maxValue = MaxEnergy;
			SliderManager.Instance.EnemyHPSlider.value = HP;
			SliderManager.Instance.EnemyEnergySlider.value = Energy;
		}
		

	}

	public void SetTilePosition(Vector2 newTilePosition, bool instantly = false)
	{
		m_TilePosition = newTilePosition;

		PlayerManager.Instance.OnPlayerPositionChanged(instantly);
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
		return HP;
	}

	public int GetSP()
	{
		return Energy;
	}


	public float UseCard(Card card)
    {
	    if (card is AttackCard) return UseAttackCard(((AttackCard)card));
		else if (card is EnergyCard) return UseEnergyCard(((EnergyCard)card));
		else if (card is GuardCard) return UseGuardCard(((GuardCard)card));
		else if (card is HealCard) return UseHealCard(((HealCard)card));
		else if (card is MoveCard) return UseMoveCard(((MoveCard)card));
		Debug.Log(name+" Use card");

		return 1f;
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
		// 0(-1,-1)  1(-1,0)  2(-1,1)
		// 3(0, -1)  4(0,0)   5(0,1)
		// 6(1, -1)  7(1,0)   8(1,1)

		var playerTilePosition = m_TilePosition;

		Vector2 selectedTile = Vector2.zero;
		switch (pos)
		{
			case 0:
				selectedTile.x = playerTilePosition.x - 1;
				selectedTile.y = playerTilePosition.y - 1;
				break;

			case 1:
				selectedTile.x = playerTilePosition.x - 1;
				selectedTile.y = playerTilePosition.y;
				break;
			case 2:
				selectedTile.x = playerTilePosition.x - 1;
				selectedTile.y = playerTilePosition.y + 1;
				break;
			case 3:
				selectedTile.x = playerTilePosition.x;
				selectedTile.y = playerTilePosition.y -1;
				break;

			case 4:
				selectedTile.x = playerTilePosition.x;
				selectedTile.y = playerTilePosition.y;
				break;
			case 5:
				selectedTile.x = playerTilePosition.x;
				selectedTile.y = playerTilePosition.y + 1;
				break;
			case 6:
				selectedTile.x = playerTilePosition.x + 1;
				selectedTile.y = playerTilePosition.y - 1;
				break;

			case 7:
				selectedTile.x = playerTilePosition.x + 1;
				selectedTile.y = playerTilePosition.y;
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
        
		HP = Math.Min(HP + value, MaxHP);
		SetSliderValue();
    }

	private void AddEnergy(int value)
    {
		Debug.Log("Energy " + value);
		Energy = Math.Min(Energy + value, MaxEnergy);
		SetSliderValue();
    }


	// 공격 카드 사용
	private float UseAttackCard(AttackCard card)
	{
		guard = 0;
		
		string randomAttackAnimationName = Random.Range(0, 2) == 0 ? "Attack1" : "Attack2";
		var animationTime = m_SkeletonAnimation.Skeleton.Data.FindAnimation(randomAttackAnimationName).Duration;
		
		foreach (var posIndex in card.positions)
		{
			var tilePosition = GetPosition(posIndex);
			TileManager.Instance.SetColor(tilePosition, animationTime * 1.2f);
		}
		
		m_SkeletonAnimation.AnimationState.SetAnimation(0, randomAttackAnimationName, false);

		if (Energy - card.energyCost >= 0)
			StartCoroutine(UseAttackCard_Coroutine(card, animationTime));

		return GameSetting.AttackCardTime;
	}

	private IEnumerator UseAttackCard_Coroutine(AttackCard card, float time)
	{
		yield return new WaitForSeconds(time);
		
		m_SkeletonAnimation.AnimationState.SetAnimation(0, "Idle", true);

		
		Player target = isPlayer ? PlayerManager.Instance.Enemy : PlayerManager.Instance.Player;
		

		if (InTarget(card.positions, target.tilePosition)) //명중하면 true, 빗나가면 false
		{
			target.AddHP(-card.damage);
			target.TakeHitAnimation();
		}

		AddEnergy(-card.energyCost);
	}
	
	// 에너지 카드 사용
	private float UseEnergyCard(EnergyCard card)
    {
	    guard = 0;

		AddEnergy(card.energyIncrease);

		return GameSetting.EnergyCardTime;
    }

	// 가드 카드 사용
	private float UseGuardCard(GuardCard card)
    {
		guard = card.damageReduction;

		return GameSetting.GuardCardTime;
    }
	
	// 힐 카드 사용
	private float UseHealCard(HealCard card)
    {
	    guard = 0;

        if (Energy - card.energyCost >= 0)
        {
			AddHP(card.HPIncrease);
			AddEnergy(-card.energyCost);
		}

		return GameSetting.HealCardTime;
    }

	// 무브 카드 사용
	public float UseMoveCard(MoveCard cardData)
	{
		guard = 0;

		Vector2 newPosition;
		AddHP(-HP);
		
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

		return GameSetting.MoveCardTime;
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
