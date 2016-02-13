using UnityEngine;
using System.Collections;

public class StateIntro : GameState 
{
	private float countDown = 10f;
	public StateIntro(GameManager manager):base(manager){ }
	
	public override void OnStateEntered()
	{
		Application.LoadLevel("Intro");
	}
	public override void OnStateExit(){}
	public override void StateUpdate() 
	{
		if(countDown<=0)
		{
			gameManager.NewGameState(gameManager.stateMainMenu);
		}
		else
		{
			countDown -= Time.deltaTime;	
		}
		
	}
	public override void StateGUI() 
	{
		GUILayout.Label("state: State Intro  -  "+countDown);
	}
}
