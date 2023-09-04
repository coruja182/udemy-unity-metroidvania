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

    // Update is called once per frame
    void Update()
    {

    }

    public void OnShoot()
    {
        m_bossBattle.OnShoot();
    }
}
