using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager SharedInstance;
    public Text TitleLabel;
    public Text ScoreLabel;
    private int TotalScore = 0;
    // Update is called once per frame
    private void Awake() {
        if(SharedInstance == null)
        {
            SharedInstance = this;
        }
    }
    void Update()
    {
        if (GameManager.SharedInstance.GamePaused || !GameManager.SharedInstance.GameStarted)
        {
            TitleLabel.enabled = true;
        }
        else
        {
            TitleLabel.enabled = false;
        }
    }
    //metodo para ir acumulando puntos
    public void ScorePoints(int points)
    {
        TotalScore += points;
        ScoreLabel.text = "Score: " + TotalScore.ToString();
    }
}
