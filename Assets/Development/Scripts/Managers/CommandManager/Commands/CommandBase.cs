using UnityEngine;
using System.Collections;

public class CommandBase {

    protected GameObject m_actor;

    public CommandBase()
    {
    }

    public CommandBase(GameObject p_actor)
    {
        m_actor = p_actor;
    }

    public virtual void Execute()
    {

    }

    public virtual void Undo()
    {

    }

}
