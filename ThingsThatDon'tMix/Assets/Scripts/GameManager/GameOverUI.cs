using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Text Score;
    public InputField NameInput;

    void Start()
    {
        Score.text = GameManager.Instance.score.ToString();
    }

    public void Post ()
    {
        HighScoreManager.Instance.InsertScore(NameInput.text, (int)GameManager.Instance.score);
        GameManager.Instance.NewGameState(GameManager.Instance.stateMainMenu);
    }
}
