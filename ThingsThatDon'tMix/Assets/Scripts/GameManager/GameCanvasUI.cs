using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameCanvasUI : MonoBehaviour {

    public Text Score;

	void Update ()
    {
        Score.text = GameManager.Instance.score.ToString();
	}
}
