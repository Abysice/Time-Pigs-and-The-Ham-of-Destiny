using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyManager : MonoBehaviour {

	#region Public Variables
	public GameObject enemyObject;
	#endregion
	
	#region Protected Variables
	#endregion
	
	#region Private Variable
	private int current_wave = 0; //begins at wave 0
	public GameObject[] wave;
	private CommandManager m_cmanager;
	#endregion
	
	#region Accessors
	#endregion
	
	#region Unity Defaults
	void Start ()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
	}
	
	void Update ()
	{
		if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
		{
			return;
		}	
			
		if (current_wave == 0)
		{
			wave = GameObject.FindGameObjectsWithTag("0");
			foreach (GameObject spawner in wave)
			{
				SpawnController cont = spawner.GetComponent<SpawnController>();
				cont.allowSpawn();
			}
			current_wave++;
		}
	}
	#endregion
	
	#region Public Methods
	public void SpawnEnemy(int type, Vector3 pos)
	{
		switch(type)
		{
		case 1: //v-type, move horizontally right to left across the screen
			enemyObject = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().R2L, pos, Managers.GetInstance().GetGameProperties().R2L.transform.rotation);
			break;
		case 2: //v-type, move horizontally left to right across the screen
			enemyObject = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().L2R, pos,Managers.GetInstance().GetGameProperties().L2R.transform.rotation);
			break;
		case 3: //o-type 1, move down the screen along the right diagonal
			enemyObject = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().DRD, pos, Managers.GetInstance().GetGameProperties().DRD.transform.rotation);
			break;
		case 4: //o-type 1, move down the screen along the left diagonal
			enemyObject = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().DLD, pos, Managers.GetInstance().GetGameProperties().DLD.transform.rotation);
			break;
		case 5: //o-type 1, move up the screen along the right diagonal
			enemyObject = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().URD, pos, Managers.GetInstance().GetGameProperties().URD.transform.rotation);
			break;
		case 6: //o-type 1, move up the screen along the left diagonal
			enemyObject = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().ULD, pos, Managers.GetInstance().GetGameProperties().ULD.transform.rotation);
			break;
		case 7: //m-type, move vertically down the screen
			enemyObject = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().D, pos, Managers.GetInstance().GetGameProperties().D.transform.rotation);
			break;
		}
	}
	#endregion
	
	#region Protected Methods
	#endregion
	
	#region Private Methods
	#endregion
}
