// Keeps the character moving when it should
//
//

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private InputManager m_inp;
	private CommandManager m_cmanager;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_inp = Managers.GetInstance().GetInputManager();
		m_cmanager = Managers.GetInstance().GetCommandManager();
	}
	//runs every frame
	public void Update()
	{
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			//PlaceHolder Code
			if (Input.GetMouseButtonDown(0))
			{
				m_cmanager.AddMoveCommand(gameObject);
			}

			if (Input.GetMouseButtonDown(1))
			{
				m_cmanager.MoveToPrevious();
			}
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
