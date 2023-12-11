using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    public AudioClip[] menuMusicClips;
    private AudioSource audioSource;
    private List<AudioClip> recentlyPlayed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        recentlyPlayed = new List<AudioClip>();
        PlayRandomMenuMusic();
    }
    // Verifica se a última música criada foi tocada...
    void PlayRandomMenuMusic()
    {
        if (menuMusicClips.Length > 0)
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
            // Reinicia a lista se não há músicas tocando...
            else
            {
                Debug.LogWarning("All menu music clips have been played. Resetting the list.");
                recentlyPlayed.Clear();
                PlayRandomMenuMusic();
            }
        }
        // Diz que não há músicas assimiladas...
        else
        {
            Debug.LogError("No menu music clips assigned!");
        }
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
