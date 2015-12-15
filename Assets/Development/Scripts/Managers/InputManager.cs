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
    private Vector2 m_MouseClickInWorldCoords = Vector2.zero;
    private CommandManager m_cmanager;
    private bool m_moving = false;
    public bool Rewinding = false; //PlaceHolder
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
    }
    //runs every frame
    public void Update()
    {
        if(Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
        {
            //PlaceHolder Code

            if(Input.GetMouseButtonDown(0) && !Rewinding)
            {
                m_MouseClickInWorldCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                m_cmanager.AddMoveCommand(Managers.GetInstance().GetLoadManager().playerObject);
                m_moving = true;
            }
            else if (Vector2.Distance(m_MouseClickInWorldCoords, Managers.GetInstance().GetLoadManager().playerObject.transform.position) > 0.1f && !Rewinding && m_moving)
            {
                m_cmanager.AddMoveCommand(Managers.GetInstance().GetLoadManager().playerObject);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Rewinding Now");
                m_moving = false;
                Rewinding = true;
            }
            if (Rewinding)
            {
                m_cmanager.UndoLastAction();
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
