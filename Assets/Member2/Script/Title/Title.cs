using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Title : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public GameObject SelectPanel = null;
	public void OnPointerDown(PointerEventData data)
	{
		
	}

	public void OnPointerUp(PointerEventData data)
	{
		this.gameObject.SetActive(false);
		SelectPanel.SetActive(true);
	}
}
