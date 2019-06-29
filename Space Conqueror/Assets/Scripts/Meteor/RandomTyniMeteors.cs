using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTyniMeteors : MonoBehaviour
{
    public float m_spawnX;
    public float m_spawnX2;
    public float m_spawnY;
    public float m_spawnY2;
    public GameObject m_tyniMeteors;
    public int m_amount;

    private void Start()
    {
        for (int i = 0; i < m_amount; i++)
        {
            Instantiate(m_tyniMeteors,
                new Vector2(Random.Range(m_spawnX, m_spawnX2), Random.Range(m_spawnY, m_spawnY2)), Quaternion.identity);
        }
    }
}
