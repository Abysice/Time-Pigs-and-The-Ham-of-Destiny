// Camera controller, controls the camera
//
// Written By: Adam Bysice

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private float CAM_SPEED = 0.01f;
	private GameObject m_player = Managers.GetInstance().GetLoadManager().playerObject;
	private CommandManager m_cmanager = Managers.GetInstance().GetCommandManager();
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	public void Start()
	{

	}

	public void Update()
	{
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
			{
				return;
			}

			//PlaceHolder Code
			Vector3 m_inputVec = Vector3.zero;
			m_inputVec += (Vector3.up * CAM_SPEED);
			m_cmanager.AddMoveCommand(gameObject, gameObject.transform.position + m_inputVec);
			
		
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
