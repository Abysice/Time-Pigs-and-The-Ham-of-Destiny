using UnityEngine;
using System.Collections;

public class CommandBase {

	protected GameObject m_actor;
	protected float m_creationTime;

	public CommandBase()
	{
		m_creationTime = Time.fixedTime;
	}

	public CommandBase(GameObject p_actor)
	{
		m_actor = p_actor;
		m_creationTime = Time.fixedTime;
	}

	public virtual void Execute()
	{

	}

	public virtual void Undo()
	{

	}

}
