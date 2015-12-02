// Inputmanager will be responsible for handling all the 
// touchscreen input junk
//

using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {


    #region Public Variables
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variable
    private Vector2 m_MouseClickInWorldCoords;
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

    }
    //runs every frame
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            m_MouseClickInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(m_MouseClickInWorldCoords);
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
