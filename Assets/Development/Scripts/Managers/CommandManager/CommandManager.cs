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
	private float MAGIC_TIMER = 0.05f;
	private float m_timer = 0.05f;

	private bool m_isRewinding = false;
	private InputManager m_inp;
	private LinkedList<LinkedList<CommandBase>> m_commandBuffer = new LinkedList<LinkedList<CommandBase>>(); //command buffer storing all commands
	private LinkedListNode<LinkedList<CommandBase>> m_currentFrame; //pointer to the current node
	private int m_currentFrameIndex;
	#endregion

	#region Accessors
	public float GetMaxTimer()
	{
		return m_timer;
	}

	public float GetTimer()
	{
		return MAGIC_TIMER;
	}

	public bool GetTimeState()
	{
		return m_isRewinding;
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
		//ClampTimers();
		ClampTimersKeyboard();
		///////////////////////////////////////////////////////////////////

		if (MAGIC_TIMER > 0.0f) // do nothing this frame
		{
			MAGIC_TIMER -= Time.deltaTime;
			return;
		}
		else if (MAGIC_TIMER <= 0.0f)
		{
			MAGIC_TIMER = m_timer;
		}
	}

	//stuff after all the commands have been entered
	public void LateUpdate()
	{
		//runs before every frame
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			if (!m_isRewinding)
			{
				if (MAGIC_TIMER == m_timer && !Managers.GetInstance().GetLoadManager().playerObject.GetComponent<PlayerController>().m_stillDead)
				{
					AddNewFrame();
				}
				else
				{
					HangOnFrame();
				}
			}
			else
			{
				if (MAGIC_TIMER == m_timer)
				{
					MoveBackAFrame();
				}
				else // Execute at same frame
				{
					HangOnFrameUndo();
				}
			}
		}
	}
	#endregion

	#region Public Methods
	//add a command to the current frame
	public void AddMoveCommand(GameObject p_actor, Vector3 m_destination)
	{
		if (!m_isRewinding)
		{
			m_currentFrame.Value.AddFirst(new Move_Command(p_actor, p_actor.transform.localPosition, m_destination));
			m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
		}
	}

    public void AddDestroyBulletCommand(GameObject p_actor, Vector2 m_position)
    {
        if (!m_isRewinding)
        {
            m_currentFrame.Value.AddFirst(new DestroyBullet_Command(p_actor, p_actor.transform.position));
            m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
        }
    }

    public void AddSpawnBulletCommand(BulletPool p_bulletPool, Vector2 m_position, Vector2 m_direction)
    {
        if (!m_isRewinding)
        {
            m_currentFrame.Value.AddFirst(new SpawnBullet_Command(p_bulletPool, m_position, m_direction));
            m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
        }
    }

	public void AddPlayerDeathCommand(SpriteRenderer p_sprite, GameObject p_explosion)
	{
		if (!m_isRewinding)
		{
			m_currentFrame.Value.AddFirst(new DisableComponentCommand(p_sprite, p_explosion));
			m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
		}
	}
    public void AddSpawnEnemyCommand(EnemyPool p_enemyPool, Vector2 m_position)
    {
        if (!m_isRewinding)
        {
            m_currentFrame.Value.AddFirst(new SpawnEnemy_Command(p_enemyPool, m_position));
            m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
        }
    }

    public void AddDestroyEnemyCommand(GameObject p_actor, Vector2 m_position)
    {
        if (!m_isRewinding)
        {
            m_currentFrame.Value.AddFirst(new DestroyEnemy_Command(p_actor, p_actor.transform.position));
            m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
        }
    }

	public void AddFinishedExplosion(Animator p_animation, GameObject p_actor)
	{
		if (!m_isRewinding)
		{
			m_currentFrame.Value.AddFirst(new ToggleAnimation(p_animation, p_actor));
			m_currentFrame.Value.First.Value.Execute(); //execute the command you just added
		}
	}


	public void MoveBackAFrame()
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
			m_commandBuffer.RemoveLast(); //delete all the extra's before we start moving forward again	
		}

	}

	//Add a new frame to the buffer
	public void AddNewFrame()
	{
		//if last frame had nothing happen
		if (m_currentFrame != null && m_currentFrame.Value.Count == 0)
		{
			return; // don't bother adding a new frame
		}
	
		m_commandBuffer.AddLast(new LinkedList<CommandBase>());
		m_currentFrame = m_commandBuffer.Last;
		m_currentFrameIndex++;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	private void HangOnFrame()
	{
		if (m_currentFrame.Value.Count == 0)
		{
			return;
		}
		foreach (CommandBase com in m_currentFrame.Value)
		{
			if (com is Move_Command)
			{
				com.Execute();
			}
			
		}
	}

	private void HangOnFrameUndo()
	{
		foreach (CommandBase com in m_currentFrame.Value)
		{
			if (com is Move_Command)
				com.Undo();
		}
	}

	private void ClampTimersKeyboard()
	{
		//speeds up time
		//CHANGE ME TO XBOX TRIGGER VALUE LATER
		if (Input.GetKeyDown(KeyCode.T))
		{
			m_timer -= 0.01f; //MAX_TIMER = 0.1 - (Trigger/100.0f)
			if (m_timer <= 0f)
				m_timer = 0f;
			if (m_timer > 0.03f)
				m_timer = 0.03f;
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			m_timer += 0.01f; //MAX_TIMER = 0.1 - (Trigger/100.0f)
			if (m_timer <= 0f)
				m_timer = 0f;
			if (m_timer > 0.03f)
				m_timer = 0.03f;
		}
		//Debug.Log(m_timer);

		if (Input.GetKey(KeyCode.Y))
			m_isRewinding = true;
		else
			m_isRewinding = false;
	}

	private void ClampTimers()
	{
		float l_temp = Mathf.Max(m_inp.p1_LTrigger, m_inp.p1_RTrigger);

		if (l_temp > 0.05f)
		{
			m_timer = 0.03f - (l_temp * 0.01f * 3.0f); //MAX_TIMER = 0.1 - (Trigger/100.0f)-
			if (m_timer <= 0f)
				m_timer = 0f;
			if (m_timer > 0.03f)
				m_timer = 0.03f;
		}

		if (m_inp.p1_LTrigger > 0.05f && (m_inp.p1_LTrigger > m_inp.p1_RTrigger))
			m_isRewinding = true;
		else
			m_isRewinding = false;
	}
	#endregion


}
