// Command class manager, will contain the command buffer for all game actions and their actions
//
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour
{
	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private float MAGIC_TIMER = 0.1f;
	private float MAX_TIMER = 0.1f;

	private InputManager m_inp;
	private LinkedList<LinkedList<CommandBase>> m_commandBuffer = new LinkedList<LinkedList<CommandBase>>(); //command buffer storing all commands
	private LinkedListNode<LinkedList<CommandBase>> m_currentFrame; //pointer to the current node
	private int m_currentFrameIndex;
	#endregion

	#region Accessors
	public float GetTimer()
	{
		return MAGIC_TIMER;
	}
	#endregion

	#region Unity Defaults
	//initialization
	public void Awake()
	{
		m_inp = Managers.GetInstance().GetInputManager();
		AddNewFrame();
		m_currentFrameIndex = 0;
	}
	//runs every frame
	public void Update()
	{	
		//speeds up time
		//CHANGE ME TO XBOX TRIGGER VALUE LATER
		if (Input.GetKeyDown(KeyCode.T))
			MAX_TIMER -= 0.01f; //MAX_TIMER = 0.1 - (Trigger/100.0f)

		if (MAGIC_TIMER > 0.0f) // do nothing this frame
		{
			MAGIC_TIMER -= Time.deltaTime;
			return;
		}
		else if (MAGIC_TIMER < 0.0f)
		{
			MAGIC_TIMER = MAX_TIMER;
		}

		//Add a new "frame" every frame, Later: once list gets too long, remove oldest frame
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			//AddNewFrame();
		}
	}

	//stuff after all the commands have been entered
	public void LateUpdate()
	{
		
	}
	#endregion

	#region Public Methods
	//add a command to the current frame
	public void AddMoveCommand(GameObject p_actor, Vector2 m_destination)
	{
		m_currentFrame.Value.AddFirst(new Move_Command(p_actor, p_actor.transform.position, m_destination));
		m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
		AddNewFrame();
		Debug.Log(m_commandBuffer.Count);
	}

	//move to a certain point in the command buffer from 0-1
	//public void MoveForward()
	//{
	//	if (m_currentFrame == null)
	//	{
	//		Debug.Log("This should never happen");
	//		AddNewFrame();
	//	}
	//	foreach (CommandBase com in m_currentFrame.Value)
	//	{
	//		com.Execute();
	//	}
	//	m_currentFrame = m_currentFrame.Next; // move back a frame
	//	m_currentFrameIndex++;
	//}
	public void MoveToPrevious()
	{
		if (m_currentFrame.Value.Count > 0)
		{
			foreach (CommandBase com in m_currentFrame.Value)
			{
				com.Undo();
			}
		}
		if (m_currentFrameIndex > 0) //can't go back a frame when at frame 0
		{
			m_currentFrame = m_currentFrame.Previous; // move back a frame
			m_currentFrameIndex--;
		}

		Debug.Log(m_commandBuffer.Count);

	}

	//Add a new frame to the buffer
	public void AddNewFrame()
	{
		//if last frame had nothing happen
		if (m_currentFrame != null && m_currentFrame.Value.Count == 0)
		{
			return; // don't bother adding a new frame
		}
		//if current frame is not last frame
		while (m_currentFrame != m_commandBuffer.Last)
		{
			m_commandBuffer.RemoveLast();//delete all the extra's before we start moving forward again	
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
