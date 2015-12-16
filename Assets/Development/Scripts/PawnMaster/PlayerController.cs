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
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_inp = Managers.GetInstance().GetInputManager();
	}
	//runs every frame
	public void Update()
	{
		//stop moving if you're close enuf to goal
		if (Vector2.Distance(m_inp.MouseInWorldCoords, Managers.GetInstance().GetLoadManager().playerObject.transform.position) < 0.1f)
		{
			m_inp.m_timeFlowing = false;
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
