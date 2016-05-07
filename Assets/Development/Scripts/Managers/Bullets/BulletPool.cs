using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPool : ScriptableObject
{
    private List<GameObject> m_bulletList;
    private GameObject m_bulletPrefab;

    public GameObject GetBullet()
    {
        if (m_bulletList.Count > 0)
        {
            GameObject obj = m_bulletList[0];
            m_bulletList.RemoveAt(0);
            return obj;
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(m_bulletPrefab);
            obj.GetComponent<Bullet>().SetBulletManager(this);
            return obj;
        }
    }

    public void ReturnBulletToPool(GameObject obj)
    {
        m_bulletList.Add(obj);
        obj.SetActive(false);
    }

    public void ReactivateBullet(GameObject obj)
    {
        m_bulletList.Remove(obj);
        obj.SetActive(true);
    }

    public void ClearPool()
    {
        for (int i = m_bulletList.Count - 1; i > 0; i--)
        {
            GameObject obj = m_bulletList[i];
            m_bulletList.RemoveAt(i);
            Destroy(obj);
        }

        m_bulletList = null;
    }

    public GameObject GetPrefabType()
    {
        return m_bulletPrefab;
    }

    public void InitializeBulletPool(int size, GameObject prefab)
    {
        m_bulletList = new List<GameObject>();
        m_bulletPrefab = prefab;

        for (int i = 0; i < size; i++)
        {
            GameObject obj = (GameObject)Instantiate(prefab);
            obj.GetComponent<Bullet>().SetBulletManager(this);
            obj.SetActive(false);
            m_bulletList.Add(obj);
        }
    }
}
