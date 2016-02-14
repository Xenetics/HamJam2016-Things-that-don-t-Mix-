using UnityEngine;
using System.Collections;

public class CludgeLevelChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play()
	{
		GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
	}
}
