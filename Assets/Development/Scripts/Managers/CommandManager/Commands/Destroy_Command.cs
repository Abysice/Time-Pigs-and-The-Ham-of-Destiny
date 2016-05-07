using UnityEngine;
using System.Collections;

public class Destroy_Command : CommandBase {
    
    private Vector2 m_destroyedPosition;

	public Destroy_Command()
	{
	}

    public Destroy_Command(GameObject p_actor, Vector2 p_destroyedPosition)
	{
		m_actor = p_actor;
        m_destroyedPosition = p_destroyedPosition;
	}

	public override void Execute()
	{
		//m_actor.transform.position = m_endPos;
	}

	public override void Undo()
	{
		//m_actor.transform.position = m_startPos;
	}
}
