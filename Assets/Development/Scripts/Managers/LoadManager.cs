// Loadmanager will be used for loading player/cameras/levels
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
    private GameObject playerObject;
    private GameObject cameraObject;
    private Camera cameraComponent;
    #endregion

    #region Accessors
    public GameObject PlayerObject
    {
        get { return playerObject; }
    }

    public GameObject CameraObject
    {
        get { return cameraObject; }
    }
    
    public Camera CameraComponent
    {
        get { return cameraComponent; }
    }
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
    
    public void SpawnCamera()
    {
        cameraObject = GameObject.Instantiate(Managers.GetInstance().GetGameProperties().cameraPrefab);
        cameraComponent = cameraObject.GetComponent<Camera>();
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
