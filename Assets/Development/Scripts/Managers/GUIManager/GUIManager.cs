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
	private GameObject m_canvasObject;
	private CommandManager m_cmanager;
	private bool m_init = false;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	public void Awake()
	{

	}

	//initialization
	public void Start()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
	}
	//runs every frame
	public void Update()
	{
		if (!m_init)
			return;

	}
	#endregion

	#region Public Methods
	//Spawn the ingame GUI
	public void LoadGameGUI()
	{

	}


	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
