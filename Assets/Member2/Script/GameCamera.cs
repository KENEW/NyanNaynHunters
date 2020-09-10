using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class GameCamera : MonoSingleton<GameCamera>
{
    private ProCamera2D m_ProCamera2D;
    private ProCamera2DShake m_ProCamera2DShake;

    private void Awake()
    {
        m_ProCamera2D      = GetComponent<ProCamera2D>();
        m_ProCamera2DShake = GetComponent<ProCamera2DShake>();
    }
    
    public void AttackMode(Vector3 position, float time)
    {
    }

    public void NormalMode()
    {
        
    }

    public void ShakeAttack()
    {
        m_ProCamera2DShake.Shake(0);
    }
}
