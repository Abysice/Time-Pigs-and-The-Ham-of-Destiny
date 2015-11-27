//Managers singleton, allows for easy access to all manager 
//classes, also handles initialization
//
using UnityEngine;
using System.Collections;

public class Managers : MonoBehaviour {
    
    #region Public Variables
    //Variables
    private static Managers m_instance = null;
    private GameStateManager m_gamestatemanager = null;
    private CommandManager m_commandmanager = null;
    private GameProperties m_gameproperties = null;
    private LoadManager m_loadmanager = null;
    private InputManager m_inputmanager = null;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
    #endregion

    #region Accessors
    //Accessors
    public static Managers GetInstance()
    {
        return m_instance;
    }

    public GameStateManager GetGameStateManager()
    {
        return m_gamestatemanager;
    }

    public CommandManager GetCommandManager()
    {
        return m_commandmanager;
    }

    public GameProperties GetGameProperties()
    {
        return m_gameproperties;
    }
    
    public LoadManager GetLoadManager()
    {
        return m_loadmanager;
    }

    public InputManager GetInputManager()
    {
        return m_inputmanager;
    }

    #endregion

    #region Unity Defaults

    void Awake()
    {
        m_instance = this;
    }
    // Use this for initialization
    void Start()
    {
        m_gameproperties = m_instance.GetComponent<GameProperties>();
        m_gamestatemanager = gameObject.AddComponent<GameStateManager>();
        m_commandmanager = gameObject.AddComponent<CommandManager>();
        m_loadmanager = gameObject.AddComponent<LoadManager>();
        m_inputmanager = gameObject.AddComponent<InputManager>();
        m_gamestatemanager.Init();

    }

    // Update is called once per frame
    void Update()
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
