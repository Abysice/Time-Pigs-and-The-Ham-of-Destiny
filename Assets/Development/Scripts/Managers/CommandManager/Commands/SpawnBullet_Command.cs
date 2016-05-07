using UnityEngine;
using System.Collections;

public class SpawnBullet_Command : CommandBase {
    
    private Vector2 m_spawnPosition;
    private BulletPool m_bulletPool;

	public SpawnBullet_Command()
	{
	}

    public SpawnBullet_Command(BulletPool p_bulletPool, Vector2 p_spawnPosition)
	{
        m_spawnPosition = p_spawnPosition;
        m_bulletPool = p_bulletPool;
	}

	public override void Execute()
	{
        m_actor = m_bulletPool.GetBullet();
        m_actor.transform.position = m_spawnPosition;
        m_actor.GetComponent<Bullet>().ActivateBullet();
	}

	public override void Undo()
	{
        m_actor.GetComponent<Bullet>().DestroyBullet();
	}
}
