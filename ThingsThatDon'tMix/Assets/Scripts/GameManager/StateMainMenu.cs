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
		
		if( GUILayout.Button("Single Player Game") )
		{
			gameManager.NewGameState(gameManager.stateGamePlaying);
		}

		if( GUILayout.Button("Multiplayer Game") )
		{
			//TODO: Go to the Multiplayer Lobby options screen.
		}

		if( GUILayout.Button("Options") )
		{
			//TODO: Option menu for various settings and resetting your profile
		}

		if( GUILayout.Button("Credits") )
		{
			//TODO: Launch the credit reel!
		}
	
		if( GUILayout.Button("Quit Game") )
		{
			Application.Quit();	
		}
	}
}
