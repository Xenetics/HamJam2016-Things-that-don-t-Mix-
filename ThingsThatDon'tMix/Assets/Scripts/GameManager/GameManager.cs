using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	//state control variables
	private GameState currentState;
	public StateGamePlaying stateGamePlaying{get;set;}
	public StateGameOver stateGameOver{get;set;}
	public StateIntro stateIntro{get;set;}
	public StateMainMenu stateMainMenu{get;set;}

	//logical level number.
	public int currentLevelNumber{get;set;}

	//singleton instance
	public static GameManager Instance { get { return instance; } }
	private static GameManager instance = null;
	
	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;        
		} 
		else 
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		
		//initializing states
		stateGamePlaying = new StateGamePlaying(this);
		stateGameOver = new StateGameOver(this);
		stateIntro = new StateIntro(this);
		stateMainMenu = new StateMainMenu(this);

		//initial level
		currentLevelNumber = 1;
	}
	
	
	private void Start () 
	{
		NewGameState( stateIntro );
	}
	
	private void Update () 
	{
		if (currentState != null)
		{
			currentState.StateUpdate();
		}
	}
	
	private void OnGUI () 
	{
		if (currentState != null)
		{
			currentState.StateGUI();
		}
	}
	
	public void NewGameState(GameState newState)
	{
		if( null != currentState)
		{
			currentState.OnStateExit();
		}
		currentState = newState;
		currentState.OnStateEntered();
	}

	//converts the requested level number (1 to X) into the proper scene number and returns that to be used with Application.LoadLevel().
	public int GetSceneNumberForLevel(int num)
	{
		//TODO: set-up the proper conversion here once the level count is determined and placeholder scenes created...
		return currentLevelNumber + 2;
    }

    public void Play()
	{
		NewGameState(stateGamePlaying);
	}

	public void Options()
	{
		
	}

    public void Quit()
    {
        Application.Quit();	
	}
}