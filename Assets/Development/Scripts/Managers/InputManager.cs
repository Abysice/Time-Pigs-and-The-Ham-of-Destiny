// Inputmanager will be responsible for handling all the 
// touchscreen input junk
//

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using XInputDotNetPure; // Required in C#


public class InputManager : MonoBehaviour {


	#region Public Variables
	public bool m_timeFlowing = false;

	public float p1_LTrigger;
	public float p1_RTrigger;
	public int p1_ButtonA;
	public float p1_LStickX;
	public float p1_LStickY;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variable
	
	bool player1IndexSet = false;
	PlayerIndex player1Index;
	GamePadState state1;
	GamePadState prevState1;

	private CommandManager m_cmanager;
	private GUIManager m_guimanager;
	
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
		m_guimanager = Managers.GetInstance().GetGUIManager();
	}
	//runs every frame
	public void Update()
	{
		Player1Control();

	}


	#endregion

	#region Public Methods
	public void Player1Control()
	{
		// Find a PlayerIndex, for a single player game
		// Will find the first controller that is connected ans use it
		if (!player1IndexSet || !prevState1.IsConnected)
		{
			PlayerIndex testPlayerIndex = (PlayerIndex)0;
			GamePadState testState = GamePad.GetState(testPlayerIndex);
			if (testState.IsConnected)
			{
				Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
				player1Index = testPlayerIndex;
				player1IndexSet = true;
			}
		}

		prevState1 = state1;
		state1 = GamePad.GetState(player1Index);


		p1_LTrigger = state1.Triggers.Left;
		p1_RTrigger = state1.Triggers.Right;
		p1_ButtonA = (int)state1.Buttons.A;
		p1_LStickX = state1.ThumbSticks.Left.X;
		p1_LStickY = state1.ThumbSticks.Left.Y;
     

	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
