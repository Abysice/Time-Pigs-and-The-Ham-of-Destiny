//Managers singleton, allows for easy access to all manager 
//classes, also handles initialization

using UnityEngine;
using System.Collections;

public class Managers : MonoBehaviour {
    
    //Variables
    private static Managers m_instance = null;
    private GameStateManager m_gamestatemanager = null;
    private CommandManager m_commandmanager = null;
    private GameProperties m_gameproperties = null;

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

    void Awake()
    {
        m_instance = this;
    }

    // Use this for initialization
	void Start () {
         m_gameproperties = m_instance.GetComponent<GameProperties>();
	     m_gamestatemanager = gameObject.AddComponent<GameStateManager>();
         m_commandmanager = gameObject.AddComponent<CommandManager>();

         m_gamestatemanager.Init();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
