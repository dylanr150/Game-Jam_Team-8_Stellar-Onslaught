using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("SFX")]
    public AudioSource gameStartSound;

    void Start()
    {
        PlayGameStartSound();
    }

    void Update()
    {
        // You can put runtime audio logic here if needed
    }

    public void NewLevelStarted()
    {
        PlayGameStartSound();
        // Add any additional logic you want when a new level starts
    }

    private void PlayGameStartSound()
    {
        if (gameStartSound != null && !gameStartSound.isPlaying)
        {
            gameStartSound.Play();
        }
    }
}