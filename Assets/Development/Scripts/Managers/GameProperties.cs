// Game Properties class, for keeping public prefabs and variables that 
// need to be loaded into the game be more easily accessed

using UnityEngine;
using System.Collections;

public class GameProperties : MonoBehaviour {

    public GameObject player;

    private string m_levelScene = "LEVEL_SCENE";
    private string m_menuScene = "MAIN_MENU";


    public string LevelScene
    {
        get { return m_levelScene; }
    }
    public string MenuScene
    {
        get { return m_menuScene; }
    }


}
