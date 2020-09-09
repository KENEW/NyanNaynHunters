using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private CardComponent card;
    private CardField cardField;

    void Awake()
    {
        card = GetComponent<CardComponent>();
        cardField = FindObjectOfType<CardField>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    
}
