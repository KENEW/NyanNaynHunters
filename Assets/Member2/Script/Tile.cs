using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("왼쪽 더스트")] public ParticleSystem LeftDust;
    [Header("오른 더스트")] public ParticleSystem RightDust;
    private SpriteRenderer m_SpriteRenderer;

    private Color m_BaseColor = Color.white;

    [Header("공격")] public List<ParticleSystem> AttackParticles;
    
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
    
    public void PlayLeftDust(float delay)
    {
        StartCoroutine(PlayParticle_Coroutine(delay, LeftDust));
    }

    public void PlayRightDust(float delay)
    {
        StartCoroutine(PlayParticle_Coroutine(delay, RightDust));
    }

    public void PlayAttack(int index, float delay)
    {
        
        StartCoroutine(PlayParticle_Coroutine(delay, AttackParticles[index]));
    }
    
    private IEnumerator PlayParticle_Coroutine(float delay, ParticleSystem particle)
    {
        yield return new WaitForSeconds(delay);
        
        particle.gameObject.SetActive(true);
        particle.Play();
    }
}
