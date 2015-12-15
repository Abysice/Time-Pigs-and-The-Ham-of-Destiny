// Game Start class, bootstraps all the other classes
//
//

using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	// Use this for initialization
	void Start()
	{
		Application.targetFrameRate = 60;
		gameObject.AddComponent<Managers>();
		DontDestroyOnLoad(this);
	}
	//runs every frame
	public void Update()
	{

	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
