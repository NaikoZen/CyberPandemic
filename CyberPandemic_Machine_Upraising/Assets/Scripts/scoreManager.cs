using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class scoreManager : NetworkBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text hiScoreText;
    public static int scoreCount;
    public static int hiScoreCount;

    // Start is called before the first frame update
    void Start()
    {
       // if (PlayerPrefs.HasKey("HighScore"))
       // {
       //     hiScoreCount = PlayerPrefs.GetInt("HighScore");
       // }
    }

    // Update is called once per frame
    void Update()
    {


        if (scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
            //PlayerPrefs.SetInt("HighScore", hiScoreCount);
        }
        scoreText.text = "" + scoreCount;
        hiScoreText.text = "" + hiScoreCount;


    }
}
