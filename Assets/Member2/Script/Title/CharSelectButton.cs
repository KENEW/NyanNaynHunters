using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharSelectButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	public CharSelect mCharSelect = null;
	Image image = null;
    public int CharIndex;   //버튼마다 인덱스 값을 주어서 버튼 이벤트 발생시 반환되는 인덱스 값

	bool enterCheck = false;
	bool downCheck = false;

	void Start()
	{
		image = GetComponent<Image>();
	}
	public void OnPointerDown(PointerEventData data)
	{
		downCheck = true;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		enterCheck = true;
	}
	public void OnPointerExit(PointerEventData data)
	{
		enterCheck = false;
	}

	public void OnPointerUp(PointerEventData data)
	{
		if (enterCheck && downCheck)
		{
			mCharSelect.charID[mCharSelect.curCount] = CharIndex;

			if(mCharSelect.curCount >= 1)
			{
				GameManager.Instance.GetCharIndex(mCharSelect.charID[0], mCharSelect.charID[1]);
				SceneManager.LoadScene("Main");
			}
			else
			{
				image.color = Color.blue;
				mCharSelect.curCount++;
			}

			Debug.Log(CharIndex);
		}
		else
		{
			downCheck = false;
		}
	}
}
