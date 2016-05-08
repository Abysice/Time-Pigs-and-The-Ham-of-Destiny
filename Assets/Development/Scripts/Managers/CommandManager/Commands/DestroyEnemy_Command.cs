using UnityEngine;
using System.Collections;

public class DestroyEnemy_Command : CommandBase
{
    private Vector2 m_destroyedPosition;

    public DestroyEnemy_Command()
    {
    }

    public DestroyEnemy_Command(GameObject p_actor, Vector2 p_destroyedPosition)
    {
        m_actor = p_actor;
        m_destroyedPosition = p_destroyedPosition;
    }

    public override void Execute()
    {
        m_actor.GetComponent<EnemyController>().DestroyEnemy();
    }

    public override void Undo()
    {
        m_actor.GetComponent<EnemyController>().BringBackEnemy(m_destroyedPosition);
    }
}
