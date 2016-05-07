using UnityEngine;
using System.Collections;

public class DestroyBullet_Command : CommandBase {
    
    private Vector2 m_destroyedPosition;

	public DestroyBullet_Command()
	{
	}

    public DestroyBullet_Command(GameObject p_actor, Vector2 p_destroyedPosition)
	{
		m_actor = p_actor;
        m_destroyedPosition = p_destroyedPosition;
	}

	public override void Execute()
	{
        m_actor.GetComponent<Bullet>().DestroyBullet();
	}

	public override void Undo()
	{
        m_actor.GetComponent<Bullet>().BringBackBullet(m_destroyedPosition);
	}
}
