using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameCanvasUI : MonoBehaviour {

    public Text Score;
    public Text Level;
    public Text Combo;

    void Update ()
    {
        Score.text = GameManager.Instance.score.ToString();
        Level.text = GameManager.Instance.currentLevelNumber.ToString();
        Combo.text = "X" + GameManager.Instance.chainPoints.ToString();
    }
}
