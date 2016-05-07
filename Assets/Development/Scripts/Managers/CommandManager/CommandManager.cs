// Command class manager, will contain the command buffer for all game actions and their actions
//
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour
{
	public const float MOVE_SPEED = 0.2f;
    public const int COMMAND_BUFFER_SIZE = 180;
	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private InputManager m_inp;
	//private LinkedList<LinkedList<CommandBase>> m_commandBuffer = new LinkedList<LinkedList<CommandBase>>(); //command buffer storing all commands
	//private LinkedListNode<LinkedList<CommandBase>> m_currentFrame; //pointer to the current node
	//private int m_currentFrameIndex;

    private CommandStack<LinkedList<CommandBase>> m_commandBuffer = new CommandStack<LinkedList<CommandBase>>();
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Awake()
	{
		m_inp = Managers.GetInstance().GetInputManager();
		AddNewFrame();
		//m_currentFrameIndex = 0;
	}
	//runs every frame
	public void Update()
	{
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
	public void AddMoveCommand(GameObject p_actor)
	{
        AddNewFrame();
		Vector3 newpos = ((new Vector3(m_inp.MouseInWorldCoords.x, m_inp.MouseInWorldCoords.y, 0.0f) - p_actor.transform.position).normalized * MOVE_SPEED) + p_actor.transform.position;
        m_commandBuffer.Peek().AddFirst(new Move_Command(p_actor, p_actor.transform.position, newpos));
        m_commandBuffer.Peek().First.Value.Execute();
		//m_currentFrame.Value.AddFirst(new Move_Command(p_actor, p_actor.transform.position, newpos));
		//m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
		
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
        if (m_commandBuffer.Count == 0)
            return;

        LinkedList<CommandBase> m_currentFrame = m_commandBuffer.Pop();
        if (m_currentFrame.Count > 0)
		{
            foreach (CommandBase com in m_currentFrame)
			{
				com.Undo();
			}
		}
		//m_currentFrame = m_currentFrame.Previous; // move back a frame
		//m_currentFrameIndex--;


		Debug.Log(m_commandBuffer.Count);

	}

	//Add a new frame to the buffer
	public void AddNewFrame()
	{
        /*
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
         * */
        if (m_commandBuffer.Count == COMMAND_BUFFER_SIZE)
        {
            m_commandBuffer.RemoveFirstElement();
        }

        m_commandBuffer.Push(new LinkedList<CommandBase>());
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion


}
