using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyManager : MonoBehaviour {

	#region Public Variables
	public GameObject enemyObject;
	#endregion
	
	#region Protected Variables
	protected float m_TTWTimer = 100;
	#endregion
	
	#region Private Variable
	private int current_wave = 0; //begins at wave 0
	private CommandManager m_cmanager;
	#endregion
	
	#region Accessors
	#endregion
	
	#region Unity Defaults
	void Start ()
	{
		m_cmanager = Managers.GetInstance().GetCommandManager();
	}
	
	public void Update ()
	{
		if (Managers.GetInstance().GetGameStateManager().CurrentState == Enums.GameStateNames.GS_03_INPLAY)
		{
			if (m_cmanager.GetTimer() > 0.0f) // do nothing this frame
			{
				return;
			}	
			waveCount();
		}
	}
	#endregion
	
	#region Public Methods
	public void waveCount()
	{
		if (!m_cmanager.GetTimeState())
		{
			if (m_TTWTimer > 0)
			{
				m_TTWTimer--;
				Debug.Log (m_TTWTimer);
				
				if (m_TTWTimer <= 0)
				{
					m_TTWTimer = 500;
					executeCurrentWave();
				}
			}
		}
		else
		{
			m_TTWTimer++;
		}
		Debug.Log (m_TTWTimer);
	}
	
	public void SpawnEnemy(int type, Vector3 pos, string wave_num)
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
		enemyObject.tag = wave_num;
	}
	#endregion
	
	#region Protected Methods
	#endregion
	
	#region Private Methods
	/// <summary>
	/// Instantiates the enemies for the current wave and sets the timer for the next wave to begin.
	/// </summary>
	private void executeCurrentWave()
	{
		string cur;
		GameObject[] wave;
		
		if (current_wave > 0) //if at least one wave has happened prior, perform cleanup
		{
			cur = (current_wave - 1).ToString();
			wave = GameObject.FindGameObjectsWithTag(cur);
			foreach (GameObject thing in wave)
			{
				thing.gameObject.SetActive(false);
			}
		}
		
		cur = current_wave.ToString();
		wave = GameObject.FindGameObjectsWithTag(cur);
		foreach (GameObject spawner in wave)
		{
			SpawnController cont = spawner.GetComponent<SpawnController>();
			cont.allowSpawn();
		}
		if (current_wave < 9)
			current_wave++;
	}
	#endregion
}
