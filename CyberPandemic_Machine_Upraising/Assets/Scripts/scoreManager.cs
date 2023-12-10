using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class scoreManager : NetworkBehaviour
{
    public TMP_Text scoreText_Player01;
    public TMP_Text scoreText_Player02;
    public TMP_Text scoreText_Player03;
    public TMP_Text scoreText_Player04;
    public TMP_Text hiScoreTextPlayer01;
    public TMP_Text hiScoreTextPlayer02;
    public TMP_Text hiScoreTextPlayer03;
    public TMP_Text hiScoreTextPlayer04;

    public static int scoreCount;
    public static int hiScoreCount;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {


        if (scoreCount > hiScoreCount)
        {
            hiScoreCount = scoreCount;
           
        }

        scoreText_Player01.text = "" + scoreCount;
        hiScoreTextPlayer01.text = "" + hiScoreCount;

        scoreText_Player02.text = "" + scoreCount;
        hiScoreTextPlayer02.text = "" + hiScoreCount;

        scoreText_Player03.text = "" + scoreCount;
        hiScoreTextPlayer03.text = "" + hiScoreCount;

        scoreText_Player04.text = "" + scoreCount;
        hiScoreTextPlayer04.text = "" + hiScoreCount;

    }

}
