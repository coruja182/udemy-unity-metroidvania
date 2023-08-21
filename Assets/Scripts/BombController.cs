using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    [SerializeField] private float m_timeToExplode = .5f;
    [SerializeField] private GameObject m_explosionGameObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_timeToExplode -= Time.deltaTime;
        if (m_timeToExplode <= 0f)
        {
            if (m_explosionGameObject != null)
            {
                Instantiate(m_explosionGameObject, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
}
