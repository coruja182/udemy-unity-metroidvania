using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{

    [SerializeField] private float m_lifetime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_lifetime);
    }

}
