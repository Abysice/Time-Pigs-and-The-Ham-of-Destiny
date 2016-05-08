using UnityEngine;
using System.Collections;

public class SpawnEnemy_Command : CommandBase
{

    private Vector2 m_spawnPosition;
    private EnemyPool m_enemyPool;

    public SpawnEnemy_Command()
    {
    }

    public SpawnEnemy_Command(EnemyPool p_enemyPool, Vector2 p_spawnPosition)
    {
        m_spawnPosition = p_spawnPosition;
        m_enemyPool = p_enemyPool;
    }

    public override void Execute()
    {
        m_actor = m_enemyPool.GetEnemy();
        m_actor.transform.position = m_spawnPosition;
        m_actor.GetComponent<EnemyController>().ActivateEnemy();
    }

    public override void Undo()
    {
        m_actor.GetComponent<EnemyController>().DestroyEnemy();
    }
}
