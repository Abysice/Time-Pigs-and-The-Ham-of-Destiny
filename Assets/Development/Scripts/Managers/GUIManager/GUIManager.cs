// Script Comments go here
//
//

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIManager : MonoBehaviour {


	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	public GameObject m_canvas;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	public void Awake()
	{
		m_canvas = Managers.GetInstance().GetGameProperties().warningcanvas;
	}

	//initialization
	public void Start()
	{

	}
	//runs every frame
	public void Update()
	{
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			if (Managers.GetInstance().GetLoadManager().playerObject.GetComponent<PlayerController>().m_stillDead)
				m_canvas.SetActive(true);
			else
				m_canvas.SetActive(false);
		}
	}
	#endregion

	#region Public Methods
	//Spawn the ingame GUI
	public void LoadGameGUI()
	{
		m_canvas.SetActive(true);
	}
	public void UnLoadGameGUI()
	{
		m_canvas.SetActive(false);
	}


	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
