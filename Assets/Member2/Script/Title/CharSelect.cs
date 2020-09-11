using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharSelect : MonoBehaviour
{
	public List<CharSelectButton> charSelectBT = null;
	public Image FadePanel = null;

	public int[] PlayerCharID;
	public int curCount = 0;                    //현재 배열에 접근하기 위한 현재 카운트 수

	private void Start()
	{
		PlayerCharID = new int[2];
	}
	void NextLoadScene()
	{
		//SceneManager.LoadScene("Main");
	}
	public void CharacterSelect(int ID)
	{
		Outline outline = charSelectBT[ID].GetComponent<Outline>();

		PlayerCharID[curCount] = ID;
		outline.enabled = true;
		outline.effectColor = curCount == 0 ? Color.blue : Color.red;
		curCount++;
	}

	public void GetCharID(int ID)
	{
		if (curCount < 1)
		{
			CharacterSelect(ID);

			while (true)	//컴퓨터와 플레이어 캐릭터 인덱스 중복검사
			{
				int temp = Random.RandomRange(0, 4);
				if (temp != PlayerCharID[0])
				{
					CharacterSelect(temp);
					break;
				}
			}
			if(curCount > 1)
			{
				Debug.Log(PlayerCharID[0] + PlayerCharID[1]);
					
				
				//PlayerManager.Instance.SelectPlayerCharacter(ID);
				GameEvent.Trigger(GameEventType.CharacterSelect, ID);
				//NextLoadScene();
				//Invoke("NextLoadScene", 3.3f);
			}
		}
	}
}
