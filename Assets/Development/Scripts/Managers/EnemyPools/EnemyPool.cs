using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPool : ScriptableObject {

    private List<GameObject> m_enemyList;
    private GameObject m_enemyPrefab;

    public GameObject GetEnemy()
    {
        if (m_enemyList.Count > 0)
        {
            GameObject obj = m_enemyList[0];
            m_enemyList.RemoveAt(0);
            return obj;
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(m_enemyPrefab);
            obj.GetComponent<EnemyController>().SetEnemyManager(this);
            return obj;
        }
    }

    public void ReturnEnemyToPool(GameObject obj)
    {
        m_enemyList.Add(obj);
        obj.SetActive(false);
    }

    public void ReactivateEnemy(GameObject obj)
    {
        m_enemyList.Remove(obj);
        obj.SetActive(true);
    }

    public void ClearPool()
    {
        for (int i = m_enemyList.Count - 1; i > 0; i--)
        {
            GameObject obj = m_enemyList[i];
            m_enemyList.RemoveAt(i);
            Destroy(obj);
        }

        m_enemyList = null;
    }

    public GameObject GetPrefabType()
    {
        return m_enemyPrefab;
    }

    public void InitializeEnemyPool(int size, GameObject prefab)
    {
        m_enemyList = new List<GameObject>();
        m_enemyPrefab = prefab;

        for (int i = 0; i < size; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefab);
            obj.GetComponent<EnemyController>().SetEnemyManager(this);
            obj.SetActive(false);
            m_enemyList.Add(obj);
        }
    }
}
