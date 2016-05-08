using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPoolManager : MonoBehaviour {

    private List<EnemyPool> m_enemyPoolList = new List<EnemyPool>();

    public EnemyPool GetEnemyPool(int type)
    {
        return m_enemyPoolList[type];
    }

    private EnemyPool SpawnEnemyPool(int p_poolSize, GameObject p_enemyPrefab)
    {
        //BulletPool l_newPool = new BulletPool(p_poolSize, p_bulletPrefab);
        EnemyPool l_newPool = ScriptableObject.CreateInstance<EnemyPool>();
        l_newPool.InitializeEnemyPool(p_poolSize, p_enemyPrefab);
        m_enemyPoolList.Add(l_newPool);
        return l_newPool;
    }

    public void GenerateEnemyPools()
    {
        SpawnEnemyPool(5, Managers.GetInstance().GetGameProperties().R2L);
        SpawnEnemyPool(5, Managers.GetInstance().GetGameProperties().L2R);
        SpawnEnemyPool(5, Managers.GetInstance().GetGameProperties().DRD);
        SpawnEnemyPool(5, Managers.GetInstance().GetGameProperties().DLD);
        SpawnEnemyPool(5, Managers.GetInstance().GetGameProperties().URD);
        SpawnEnemyPool(5, Managers.GetInstance().GetGameProperties().ULD);
        SpawnEnemyPool(5, Managers.GetInstance().GetGameProperties().D);
        SpawnEnemyPool(1, Managers.GetInstance().GetGameProperties().BaconBottom);
    }

    // Use this for initialization
    void Start()
    {
        //GenerateEnemyPools();
    }

    // Update is called once per frame
    void Update()
    {

    }


}
