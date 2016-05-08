using UnityEngine;
using System.Collections;

public class BossTakeDamage_Command : CommandBase
{

    public BossTakeDamage_Command()
    {
    }

    public BossTakeDamage_Command(GameObject p_actor)
    {
        m_actor = p_actor;
    }

    public override void Execute()
    {
        m_actor.GetComponent<BossController>().DecreaseHealth();
    }

    public override void Undo()
    {
        m_actor.GetComponent<BossController>().IncreaseHealth();
    }
}
