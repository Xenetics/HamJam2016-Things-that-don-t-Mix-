using UnityEngine;
using System.Collections;
public class StateGameOver : GameState 
{
	private float countDown = 5f;
	public StateGameOver(GameManager manager):base(manager) { }

	public override void OnStateEntered()
	{
		Application.LoadLevel("GameOver");
		countDown = 5f;
	}
	public override void OnStateExit(){}
	public override void StateUpdate() 
	{
		//if(countDown<=0)
		//{
		//	gameManager.NewGameState(gameManager.stateMainMenu);
		//}
		//else
		//{
		//	countDown -= Time.deltaTime;	
		//}
	}
	public override void StateGUI() 
	{
		GUILayout.Label("state: GAME OVER  -  "+countDown);
	}
}