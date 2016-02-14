using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	[System.Serializable]
	public class MultiArray
	{
		public bool[] values;
	}

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

	//Game Vars
	private CHEMICAL lastType = CHEMICAL.Empty;
	public uint chainPoints;
	public MultiArray[] reactions;
	public Vat theVat{get;set;}
	public uint score{get;set;}
	private uint FLASK_SCORE = 50;

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


    public void Play()
	{
		NewGameState(GameManager.Instance.stateGamePlaying);
	}

	public void Options()
	{
		
	}

    public void Quit()
    {
        Application.Quit();
	}

	public void Consume(CHEMICAL type)
	{
		//todo: create combo function for evaluation here.
		if(!reactions[(int)type].values[(int)lastType])
		{
			//store last type to continue the combo
			lastType = type;
			chainPoints++;
            SoundManager.Instance.PlaySoundRandom(SoundManager.SoundType.SFX, "combogood");
			Debug.Log("Combo: x"+chainPoints);
		}
		else
		{
			lastType = CHEMICAL.Empty;
			chainPoints = 0;
            SoundManager.Instance.PlaySoundRandom(SoundManager.SoundType.SFX, "combobad");
            Debug.Log("Combo Lost!");
		}

		score += chainPoints * FLASK_SCORE * ( 1 + (uint) currentLevelNumber / 10);
		Debug.Log ("Score: "+score);
	}

    public GameState GetState()
    {
        return currentState;
    }
}