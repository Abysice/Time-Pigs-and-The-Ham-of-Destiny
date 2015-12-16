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
			if(!EventSystem.current.IsPointerOverGameObject())
			{ 
				//PlaceHolder Code
				if(Input.GetMouseButtonDown(0))
				{
					m_guimanager.ResetScrollBar();
					m_cmanager.RestartTime();
					m_cmanager.AddNewFrame();
					m_MouseClickInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					m_cmanager.AddMoveCommand(Managers.GetInstance().GetLoadManager().playerObject);
					m_timeFlowing = true;
				}
				else if (Vector2.Distance(m_MouseClickInWorldCoords, Managers.GetInstance().GetLoadManager().playerObject.transform.position) > 0.1f && m_timeFlowing)
				{
					m_cmanager.AddNewFrame();
					m_cmanager.AddMoveCommand(Managers.GetInstance().GetLoadManager().playerObject);
				}
			}
			else //is over the GUI
			{
				if (Input.GetMouseButtonDown(0))
				{
					//start lerping towards the slider value
					m_timeFlowing = false;
					m_guimanager.LerpSliderTowards(Input.mousePosition.x / Screen.width);
				}
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
