using UnityEngine;
using System.Collections;

public class StateMainMenu : GameState 
{
	public StateMainMenu(GameManager manager):base(manager){ }
	
	public override void OnStateEntered()
	{
		Application.LoadLevel("MainMenu");
	}
	public override void OnStateExit(){}
	public override void StateUpdate() {}
	public override void StateGUI() 
	{
		GUILayout.Label("state: State Main Menu");
	}
}
