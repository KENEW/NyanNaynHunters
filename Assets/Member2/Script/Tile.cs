using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;

    private Color m_BaseColor = Color.white;
    
    public Transform Left;
    public Transform Right;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.enabled = false;
    }

    public void FlashColor(float time)
    {
        StartCoroutine(FlashColor_Coroutine(time));
    }

    private IEnumerator FlashColor_Coroutine(float time)
    {
        m_SpriteRenderer.enabled = true;

        yield return new WaitForSeconds(time);

        m_SpriteRenderer.enabled = false;
    }
}
