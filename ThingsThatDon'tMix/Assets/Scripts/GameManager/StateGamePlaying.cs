using UnityEngine;
using System.Collections;
public class StateGamePlaying : GameState 
{
	private bool isPaused = false;
	
	public StateGamePlaying(GameManager manager):base(manager){	}
	
	public override void OnStateEntered()
	{
        Application.LoadLevel("Play");
    }

	public override void OnStateExit(){}
	
	public override void StateUpdate() 
	{
		// Did the player win?
		if(gameManager.theVat.fluidHeight <= 0)
		{
			NextLevel();
		}
		
		//check for pause button 
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			if (isPaused)
			{
				ResumeGameMode();
			}
			else
			{
				PauseGameMode();
			}
		}
	}
	
	public override void StateGUI() 
	{	
		if(isPaused)
		{
			string[] names = QualitySettings.names;
			string message = "Game Paused. Press ESC to resume or select a new quality setting below.";
			GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginVertical();
			GUILayout.Label(message, GUILayout.Width(200));
			
			for (int i = 0; i < names.Length; i++) 
			{
				if (GUILayout.Button(names[i],GUILayout.Width(200)))
				{
					QualitySettings.SetQualityLevel(i,true);
				}
			}
			
			GUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.EndArea();
		}
		else
		{
			GUILayout.Label("state: GAME PLAYING");	
		}
	}
	
	private void ResumeGameMode() 
	{
		Time.timeScale = 1.0f;
		isPaused = false;
	}
	
	private void PauseGameMode() 
	{
		Time.timeScale = 0.0f;
		isPaused = true;
	}

	private void NextLevel()
	{
		gameManager.currentLevelNumber++;
		gameManager.theVat.maxFluid = 10 + ((gameManager.currentLevelNumber / 5) * 10);
		gameManager.theVat.fluidHeight = gameManager.theVat.maxFluid - 5;
		gameManager.theVat.conveyors[0].speed += 5;
		gameManager.theVat.conveyors[1].speed += 5;
		gameManager.theVat.conveyors[0].spawnMin = Mathf.Max (0.5f, (gameManager.theVat.conveyors[0].spawnMin - 0.02f));
		gameManager.theVat.conveyors[0].spawnMax = Mathf.Max (0.5f, (gameManager.theVat.conveyors[0].spawnMin - 0.025f));
		gameManager.theVat.conveyors[1].spawnMin = Mathf.Max (0.5f, (gameManager.theVat.conveyors[1].spawnMin - 0.02f));
		gameManager.theVat.conveyors[1].spawnMax = Mathf.Max (0.5f, (gameManager.theVat.conveyors[1].spawnMin - 0.025f));

		//TODO: Play glug glug
	}
}