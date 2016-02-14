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
	public override void OnStateExit(){
		GameManager.Instance.score = 0;
		GameManager.Instance.currentLevelNumber = 1;
		GameManager.Instance.chainPoints = 0;
	}
	public override void StateUpdate() 
	{

	}
	public override void StateGUI() 
	{

	}
}