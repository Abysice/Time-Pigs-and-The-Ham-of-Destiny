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
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variable
	
	bool player1IndexSet = false;
	PlayerIndex player1Index;
	GamePadState state1;
	GamePadState prevState1;

	bool player2IndexSet = false;
	PlayerIndex player2Index;
	GamePadState state2;
	GamePadState prevState2;


	private Vector2 m_MouseClickInWorldCoords = Vector2.zero;
	private CommandManager m_cmanager;
	private GUIManager m_guimanager;
	
	#endregion

	#region Accessors
	public Vector2 MouseInWorldCoords
	{
		get { return m_MouseClickInWorldCoords; }
	}
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
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			m_MouseClickInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

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

		// Detect if a button was pressed this frame
		if (prevState1.Buttons.A == ButtonState.Released && state1.Buttons.A == ButtonState.Pressed)
		{
			Debug.Log("BUTTON PUSHED");
		}
		// Detect if a button was released this frame
		if (prevState1.Buttons.A == ButtonState.Pressed && state1.Buttons.A == ButtonState.Released)
		{
			Debug.Log("BUTTON RELEASED");
		}

	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
