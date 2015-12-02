// Command class factory, will contain the command buffer for all game actions and their actions
//
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandManager : MonoBehaviour
{
    public const float MOVE_LERP = 0.1f;
    #region Public Variables
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
    private LinkedList<CommandBase> m_CommandBuffer = new LinkedList<CommandBase>();
    private CommandBase m_activeCommand;
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

    }
    #endregion

    #region Public Methods
    public void AddMoveCommand(GameObject p_actor)
    {
        Debug.Log(m_CommandBuffer.Count);
        Vector2 newpos = Vector2.Lerp(p_actor.transform.position, m_inp.MouseInWorldCoords, MOVE_LERP);
        m_CommandBuffer.AddFirst(new Move_Command(p_actor, p_actor.transform.position, newpos));
        m_CommandBuffer.First.Value.Execute();

    }

    public void UndoLastAction()
    {
        //PlaceHolder
        if (m_CommandBuffer.Count == 0)
        {
            //m_inp.Rewinding = false;
            return;
        }

        m_CommandBuffer.First.Value.Undo();
        m_CommandBuffer.RemoveFirst();
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion


}
