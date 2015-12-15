// Command class factory, will contain the command buffer for all game actions and their actions
//
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour
{
	public const float MOVE_LERP = 0.1f;
	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private LinkedList<LinkedList<CommandBase>> m_commandBuffer = new LinkedList<LinkedList<CommandBase>>();
	private LinkedList<CommandBase> m_currentFrame; //reference to the current frame list
	private CommandBase m_activeCommand;
	private InputManager m_inp;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_inp = Managers.GetInstance().GetInputManager();
	}
	//runs every frame
	public void Update()
	{
		//Add a new "frame" every frame, Later: once list gets too long, remove oldest frame
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY && !m_inp.Rewinding)
		{
			//if last frame had nothing happen
			if(m_currentFrame != null && m_currentFrame.Count == 0)
			{
				return; // don't add a new frame
			}
			m_commandBuffer.AddLast(new LinkedList<CommandBase>());
			m_currentFrame = m_commandBuffer.Last.Value;
		}
	}
	#endregion

	#region Public Methods
	public void AddMoveCommand(GameObject p_actor)
	{
		//Debug.Log(m_commandBuffer.Last.Value.Count);
		//temporary movement code
		Vector2 newpos = Vector2.Lerp(p_actor.transform.position, m_inp.MouseInWorldCoords, MOVE_LERP);
		m_currentFrame.AddFirst(new Move_Command(p_actor, p_actor.transform.position, newpos));
		m_currentFrame.First.Value.Execute();

	}

	public void UndoLastAction()
	{
		foreach(CommandBase com in m_currentFrame)
		{
			com.Undo();  
		}
		//PlaceHolder - if out of frames
		m_commandBuffer.RemoveLast();
		if(m_commandBuffer.Count == 0)
		{
			Debug.Log("Done Rewinding");
			m_inp.Rewinding = false;
			return;
		}
		m_currentFrame = m_commandBuffer.Last.Value;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion


}
