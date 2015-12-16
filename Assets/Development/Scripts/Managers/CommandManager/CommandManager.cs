// Command class manager, will contain the command buffer for all game actions and their actions
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
	private InputManager m_inp;
	private LinkedList<LinkedList<CommandBase>> m_commandBuffer = new LinkedList<LinkedList<CommandBase>>(); //command buffer storing all commands
	private LinkedListNode<LinkedList<CommandBase>> m_currentFrame; //pointer to the current node
	private int m_currentFrameIndex;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_inp = Managers.GetInstance().GetInputManager();
		AddNewFrame();
		m_currentFrameIndex = 0;
	}
	//runs every frame
	public void Update()
	{
		//Add a new "frame" every frame, Later: once list gets too long, remove oldest frame
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY && m_inp.m_timeFlowing)
		{
			AddNewFrame();
		}
	}
	#endregion

	#region Public Methods
	//add a command to the current frame
	public void AddMoveCommand(GameObject p_actor)
	{
		//temporary movement code
		Vector2 newpos = Vector2.Lerp(p_actor.transform.position, m_inp.MouseInWorldCoords, MOVE_LERP);
		m_currentFrame.Value.AddFirst(new Move_Command(p_actor, p_actor.transform.position, newpos));
		m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
	}

	//Restart time and remove all future nodes
	public void RestartTime()
	{
		while (m_currentFrame.Next != null) //remove future frames
		{
			m_commandBuffer.Remove(m_currentFrame.Next);
		}
		m_commandBuffer.Remove(m_currentFrame);
		m_currentFrame = m_commandBuffer.Last;
		m_currentFrameIndex = (m_commandBuffer.Count - 1);
		m_inp.m_timeFlowing = true;
	}

	//move to a certain point in the command buffer from 0-1
	public void MovetoAction(float p_sliderVal)
	{
		int l_goalIndex = (int)(((float)m_commandBuffer.Count) * p_sliderVal); // get an integer approximation
		
		if(m_currentFrame == null) 
		{
			Debug.Log("This should never happen");
			AddNewFrame();
		}
		while (l_goalIndex != m_currentFrameIndex)
		{
			if(l_goalIndex > m_currentFrameIndex)
			{
				foreach (CommandBase com in m_currentFrame.Value)
				{
					com.Execute();
				}
				m_currentFrame = m_currentFrame.Next; // move back a frame
				m_currentFrameIndex++;
			}
			else if (l_goalIndex < m_currentFrameIndex)
			{
				if(m_currentFrame.Value.Count > 0)
				{
					foreach (CommandBase com in m_currentFrame.Value)
					{
						com.Undo();
					}
				}
				m_currentFrame = m_currentFrame.Previous; // move back a frame
				m_currentFrameIndex--;
			}
		}
	}

	//Add a new frame to the buffer
	public void AddNewFrame()
	{
		//if last frame had nothing happen
		if (m_currentFrame != null && m_currentFrame.Value.Count == 0)
		{
			return; // don't add a new frame
		}
		m_commandBuffer.AddLast(new LinkedList<CommandBase>());
		m_currentFrame = m_commandBuffer.Last;
		m_currentFrameIndex++;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion


}
