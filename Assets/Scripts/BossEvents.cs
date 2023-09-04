using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvents : MonoBehaviour
{
    private BossBattle m_bossBattle;

    // Start is called before the first frame update
    void Start()
    {
        m_bossBattle = GetComponentInParent<BossBattle>();
    }

    public void OnShoot()
    {
        m_bossBattle.OnShoot();
    }

    public void OnShootFinished()
    {
        m_bossBattle.OnShootFinished();
    }

    public void OnVanish()
    {
        m_bossBattle.OnVanish();
    }

    public void OnReappear()
    {
        m_bossBattle.OnReappear();
    }
}
