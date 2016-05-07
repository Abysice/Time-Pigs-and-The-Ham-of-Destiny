using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletPoolManager : MonoBehaviour
{

    public static BulletPoolManager instance;

    private List<BulletPool> m_bulletPoolList = new List<BulletPool>();

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    public BulletPool GetBulletPool(int p_poolSize, GameObject p_bulletPrefab)
    {
        foreach (BulletPool l_pool in m_bulletPoolList)
        {
            if (l_pool.GetPrefabType() == p_bulletPrefab)
            {
                return l_pool;
            }
        }

        //BulletPool l_newPool = new BulletPool(p_poolSize, p_bulletPrefab);
        BulletPool l_newPool = ScriptableObject.CreateInstance<BulletPool>();
        l_newPool.InitializeBulletPool(p_poolSize, p_bulletPrefab);
        m_bulletPoolList.Add(l_newPool);
        return l_newPool;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


}
