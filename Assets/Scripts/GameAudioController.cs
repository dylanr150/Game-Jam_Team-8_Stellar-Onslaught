using UnityEngine;

public class GameSoundController : MonoBehaviour
{
    public static GameSoundController Instance { get; private set; }

    public AudioSource ShipShootSource;
    public AudioSource ShipDieSource;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Avoid duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persists across scenes
    }

    public void PlayShipShoot()
    {
        if (ShipShootSource != null && ShipShootSource.clip != null)
            ShipShootSource.Play();
    }

    public void PlayShipDie()
    {
        if (ShipDieSource != null && ShipDieSource.clip != null)
            ShipDieSource.Play();
    }
}