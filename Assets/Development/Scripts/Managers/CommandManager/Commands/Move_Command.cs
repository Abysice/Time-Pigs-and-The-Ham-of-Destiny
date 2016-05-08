// Script Comments go here
//
//

using UnityEngine;
using System.Collections;

public class Move_Command : CommandBase {

	private Vector3 m_startPos;
	private Vector3 m_endPos;
	public Move_Command()
	{
	}

	public Move_Command(GameObject p_actor, Vector3 p_startpos, Vector3 p_endpos)
	{
		m_actor = p_actor;
		m_startPos = p_startpos;
		m_endPos = p_endpos;
	}

	public override void Execute()
	{
		//m_actor.transform.position = m_endPos;
		m_actor.transform.position = Vector3.MoveTowards(m_actor.transform.position, m_endPos, 0.1f);
	}

	public override void Undo()
	{
		//m_actor.transform.position = m_startPos;
		m_actor.transform.position = Vector3.MoveTowards(m_actor.transform.position, m_startPos, 0.1f);
	}
}
