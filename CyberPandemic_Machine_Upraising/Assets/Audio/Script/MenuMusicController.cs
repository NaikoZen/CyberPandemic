using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    public AudioClip[] menuMusicClips;
    private AudioSource audioSource;
    private List<AudioClip> recentlyPlayed;
    // Ve se o esta em gamplay ou nao
    private bool isGameplayStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        recentlyPlayed = new List<AudioClip>();
    }
    // Verifica se a última música criada foi tocada...
    void PlayRandomMenuMusic()
    {
        if (isGameplayStarted && menuMusicClips.Length > 0)
        {
            List<AudioClip> availableClips = new List<AudioClip>(menuMusicClips);
            availableClips.RemoveAll(clip => recentlyPlayed.Contains(clip));

            if (availableClips.Count > 0)
            {
                int randomIndex = Random.Range(0, availableClips.Count);
                AudioClip randomClip = availableClips[randomIndex];
                recentlyPlayed.Add(randomClip);
                audioSource.clip = randomClip;
                audioSource.Play();
            }
            // Reinicia a lista se nao ha musicas tocando...
            else
            {
                Debug.LogWarning("All menu music clips have been played. Resetting the list.");
                recentlyPlayed.Clear();
                PlayRandomMenuMusic();
            }
        }
        // Diz que nao ha musicas assimiladas...
        else
        {
            //Debug.LogError("No menu music clips assigned!");
        }
    }
    //Quando o jogo inicia...
    public void StartGameplayMusic()
    {
        isGameplayStarted = true;
        PlayRandomMenuMusic();
    }
    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomMenuMusic ();
        }
    }
}
