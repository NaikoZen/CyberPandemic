using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class scoreManager : NetworkBehaviour
{

    public NetworkVariable<int> scoreCountPlayer01 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> hiScoreCountPlayer01 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<int> scoreCountPlayer02 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> hiScoreCountPlayer02 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<int> scoreCountPlayer03 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> hiScoreCountPlayer03 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<int> scoreCountPlayer04 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> hiScoreCountPlayer04 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public TMP_Text scoreText_Player01;
    public TMP_Text scoreText_Player02;
    public TMP_Text scoreText_Player03;
    public TMP_Text scoreText_Player04;
    public TMP_Text hiScoreTextPlayer01;
    public TMP_Text hiScoreTextPlayer02;
    public TMP_Text hiScoreTextPlayer03;
    public TMP_Text hiScoreTextPlayer04;

    public TMP_Text scoreText;
    public TMP_Text hiScoreText;

    public static int scoreCount;
    public static int hiScoreCount;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
     void Update()
    {
        // Obtém o ID do jogador local
        ulong localPlayerId = NetworkManager.Singleton.LocalClientId;

        // Atualiza as variáveis de contagem com base no ID do jogador local
        if (localPlayerId == 1)
        {
            scoreCount = scoreCountPlayer01.Value;
            hiScoreCount = hiScoreCountPlayer01.Value;
        }
        else if (localPlayerId == 2)
        {
            scoreCount = scoreCountPlayer02.Value;
            hiScoreCount = hiScoreCountPlayer02.Value;
        }
        else if (localPlayerId == 3)
        {
            scoreCount = scoreCountPlayer03.Value;
            hiScoreCount = hiScoreCountPlayer03.Value;
        }
        else if (localPlayerId == 4)
        {
            scoreCount = scoreCountPlayer04.Value;
            hiScoreCount = hiScoreCountPlayer04.Value;
        }

        // Atualiza os textos UI conforme necessário
        scoreText.text = "" + scoreCount;
        hiScoreText.text = "" + hiScoreCount;
    }

}
