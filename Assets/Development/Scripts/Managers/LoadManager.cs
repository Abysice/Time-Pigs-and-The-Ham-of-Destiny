﻿// Loadmanager will be used for loading player/cameras/levels
//
// Adam

using UnityEngine;
using System.Collections;

public class LoadManager : MonoBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	public GameObject playerObject;
	public GameObject cameraObject;
	public GameObject enemyObject;
	public Camera cameraComponent;
	#endregion

	#region Accessors
	//public GameObject GetPlayerObject()
	//{
	//    return playerObject;
	//}

	//public GameObject CameraObject
	//{
	//    get { return cameraObject; }
	//}
	
	//public Camera CameraComponent
	//{
	//    get { return cameraComponent; }
	//}
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{

	}
	//runs every frame
	public void Update()
	{

	}
	#endregion

	#region Public Methods
	public void SpawnPlayer()
	{
		playerObject = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().playerPrefab);
	}
	
	/*public void SpawnEnemy()
	{
		enemyObject = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().enemyPrefab);
	}*/
	
	public void SpawnCamera()
	{
		cameraObject = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().cameraPrefab);
		cameraComponent = cameraObject.GetComponent<Camera>();
		playerObject.transform.SetParent(cameraObject.transform);
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
