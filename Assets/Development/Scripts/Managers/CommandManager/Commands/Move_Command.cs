// Script Comments go here
//
//

using UnityEngine;
using System.Collections;

public class Move_Command : CommandBase {

	private Vector2 m_startPos;
	private Vector2 m_endPos;
	public Move_Command()
	{
	}

	public Move_Command(GameObject p_actor, Vector2 p_startpos, Vector2 p_endpos)
	{
		m_actor = p_actor;
		m_startPos = p_startpos;
		m_endPos = p_endpos;
	}

	public override void Execute()
	{
		m_actor.transform.localPosition = m_endPos;
	}


	public override void Undo()
	{
		m_actor.transform.localPosition = m_startPos;
	}
}
