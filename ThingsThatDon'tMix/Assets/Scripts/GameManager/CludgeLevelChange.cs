using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CludgeLevelChange : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject HighScoreMenu;
    public GameObject CreditsMenu;
    public List<Text> Players = new List<Text>();
    public List<Text> Scores = new List<Text>();


    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play()
	{
		GameManager.Instance.NewGameState(GameManager.Instance.stateGamePlaying);
        SoundManager.Instance.PlaySound( SoundManager.SoundType.SFX, "chalk");
	}

    public void HighScores()
    {
        HighScoreMenu.SetActive(true);
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        WriteScores();
        SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX, "chalk");
    }

    public void Credits()
    {
        HighScoreMenu.SetActive(false);
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX, "chalk");
    }

    public void Main()
    {
        HighScoreMenu.SetActive(false);
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
        SoundManager.Instance.PlaySound(SoundManager.SoundType.SFX, "chalk");
    }

    public void WriteScores()
    {
        Players[0].text = HighScoreManager.Instance.CurrentScores.Score5.Name;
        Scores[0].text = HighScoreManager.Instance.CurrentScores.Score5.Value.ToString();
        Players[1].text = HighScoreManager.Instance.CurrentScores.Score4.Name;
        Scores[1].text = HighScoreManager.Instance.CurrentScores.Score4.Value.ToString();
        Players[2].text = HighScoreManager.Instance.CurrentScores.Score3.Name;
        Scores[2].text = HighScoreManager.Instance.CurrentScores.Score3.Value.ToString();
        Players[3].text = HighScoreManager.Instance.CurrentScores.Score2.Name;
        Scores[3].text = HighScoreManager.Instance.CurrentScores.Score2.Value.ToString();
        Players[4].text = HighScoreManager.Instance.CurrentScores.Score1.Name;
        Scores[4].text = HighScoreManager.Instance.CurrentScores.Score1.Value.ToString();
    }
}
