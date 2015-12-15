//
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
		//transform.position = Vector2.Lerp(transform.position, m_inp.MouseInWorldCoords, 0.1f);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
