// Game Properties class, for keeping public prefabs and variables that 
// need to be loaded into the game be more easily accessed
//
using UnityEngine;
using System.Collections;

public class GameProperties : MonoBehaviour {

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


	public GameObject playerPrefab;
	public GameObject cameraPrefab;
	
	//enemy prefabs
	public GameObject DLD;
	public GameObject DRD;
	public GameObject URD;
	public GameObject ULD;
	public GameObject D;
	public GameObject L2R;
	public GameObject R2L;
    public GameObject BaconBottom;
	
    public GameObject playerBulletPrefab;
    public GameObject enemyBulletPrefab;
    public GameObject sineEnemyBulletPrefab;

	public GameObject warningcanvas;

	////////////////////////////
	//LAZY FUNCS

	public void StartGame()
	{
		Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_02_LOADING);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void RestartGame()
	{
		Managers.GetInstance().GetGameStateManager().ChangeGameState(Enums.GameStateNames.GS_01_MENU);
	}

}
