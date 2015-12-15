//GameStateManager, handles the state transitions between the various gamestates.
//
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager : MonoBehaviour
{
	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private Dictionary<Enums.GameStateNames, GameStateBase> m_gameStateDictionary = new Dictionary<Enums.GameStateNames, GameStateBase>();
	private GameStateBase m_currentGameState = null;
	private Enums.GameStateNames m_currentGameStateIndex = Enums.GameStateNames.GS_00_NULL;
	private Enums.GameStateNames m_nextGameStateIndex = Enums.GameStateNames.GS_00_NULL;
	private bool m_initialised = false;
	#endregion

	#region Accessors
	public Enums.GameStateNames CurrentState
	{
		get { return m_currentGameStateIndex; }
	}
	#endregion

	#region Unity Defaults
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// State machine shenanigans 
		if (!m_initialised)
			return;

		if (m_currentGameState != null)
			m_currentGameState.UpdateState();

		if (m_nextGameStateIndex != Enums.GameStateNames.GS_00_NULL)
		{
			if (m_currentGameState != null)
				m_currentGameState.ExitState(m_nextGameStateIndex);
			m_currentGameState = m_gameStateDictionary[m_nextGameStateIndex];
			m_currentGameState.EnterState(m_currentGameStateIndex);
			m_currentGameStateIndex = m_nextGameStateIndex;
			m_nextGameStateIndex = Enums.GameStateNames.GS_00_NULL;
		}
	}
	#endregion

	#region Public Methods
	public void Init()
	{
		// Initialise the bookstateDictionary
		m_gameStateDictionary.Add(Enums.GameStateNames.GS_01_MENU, new GameStateMenu(this));
		m_gameStateDictionary.Add(Enums.GameStateNames.GS_02_LOADING, new GameStateLoading(this));
		m_gameStateDictionary.Add(Enums.GameStateNames.GS_03_INPLAY, new GameStateInplay(this));
		m_gameStateDictionary.Add(Enums.GameStateNames.GS_04_LEAVING, new GameStateLeaving(this));

		//start the state machine
		ChangeGameState(Enums.GameStateNames.GS_01_MENU); //starts in the menu state

		m_initialised = true;

	}


	//Change the game state (occurs on next frame)
	public void ChangeGameState(Enums.GameStateNames nextState)
	{
		if (!m_gameStateDictionary.ContainsKey(nextState))
			return;

		m_nextGameStateIndex = nextState;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

}
