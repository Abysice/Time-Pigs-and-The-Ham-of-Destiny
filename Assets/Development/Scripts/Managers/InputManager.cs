// Inputmanager will be responsible for handling all the 
// touchscreen input junk
//

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InputManager : MonoBehaviour {


	#region Public Variables
	public bool m_timeFlowing = false;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variable
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
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			m_MouseClickInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
