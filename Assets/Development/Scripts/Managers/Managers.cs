using UnityEngine;
using System.Collections;

public class Managers : MonoBehaviour {
    
    //Variables
    private static Managers m_instance = null;
    private GameStateManager m_gamestatemanager = null;
    private CommandManager m_commandmanager = null;

    //Accessors
    public static Managers GetInstance()
    {
        return m_instance;
    }

    void Awake()
    {
        m_instance = this;
    }

    // Use this for initialization
	void Start () {
	     m_gamestatemanager = gameObject.AddComponent<GameStateManager>();
         m_commandmanager = gameObject.AddComponent<CommandManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
