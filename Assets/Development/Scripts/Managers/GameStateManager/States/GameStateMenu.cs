using UnityEngine;
using System.Collections;

public class GameStateMenu : GameStateBase
{
	public GameStateMenu(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}


	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		Debug.Log("Entered Menu State");
	}

	public override void UpdateState()
	{
		//Put some sort of GUI event to get to this
		m_gameStateManager.ChangeGameState(Enums.GameStateNames.GS_02_LOADING);
	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}