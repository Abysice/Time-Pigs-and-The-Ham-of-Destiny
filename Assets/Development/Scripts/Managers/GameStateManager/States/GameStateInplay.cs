using UnityEngine;
using System.Collections;

public class GameStateInplay : GameStateBase
{
	public GameStateInplay(GameStateManager p_gameStateManager)
	{
		m_gameStateManager = p_gameStateManager;
	}


	public override void EnterState(Enums.GameStateNames p_prevState)
	{
		Debug.Log("Entered Gameplay state");
	}

	public override void UpdateState()
	{

	}

	public override void ExitState(Enums.GameStateNames p_nextState)
	{

	}
}