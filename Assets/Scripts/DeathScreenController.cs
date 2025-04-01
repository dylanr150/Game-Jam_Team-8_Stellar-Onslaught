using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DeathScreenController : MonoBehaviour
{
    [Header("SFX")]
    public AudioSource buttonHoverSound;
    public AudioSource buttonClickSound;
    public AudioSource menuMusic;
    
    [Header("Scenes")]
    #if UNITY_EDITOR
    public SceneAsset mainMenuScene;
    public SceneAsset startScene;
    #endif

    [HideInInspector] public string mainMenuSceneName;
    [HideInInspector] public string startSceneName;
    // [HideInInspector] public string tutorialSceneName;

    [Header("Buttons")]
    public Button playAgainButton;
    public Button mainMenuButton;

    [Header("UI")]
    public TMP_Text scoreText;

    void OnValidate()
    {
        #if UNITY_EDITOR
        if (startScene != null) startSceneName = startScene.name;
        if (mainMenuScene != null) mainMenuSceneName = mainMenuScene.name;
        #endif
    }

    void PlayClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }

    void AddHoverSound(Button button)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => {
            if (buttonHoverSound != null)
                buttonHoverSound.Play();
        });

        trigger.triggers.Add(entry);
    }

    void AddSceneChangeButton(Button button, UnityEngine.Events.UnityAction action) 
    {
        if (button != null)
        {
            button.onClick.AddListener(() => 
            {
                PlayClickSound();
                action.Invoke();
            });
            AddHoverSound(button);
        }
    }
    
    void Start()
    {
        if (scoreText is not null) 
            scoreText.text = $"Score: {ScoreManager.Instance.GetCurrentScore()}";

        if (menuMusic != null && !menuMusic.isPlaying)
            menuMusic.Play();

        AddSceneChangeButton(playAgainButton, () => 
        {
            GameManager.Instance.ResetGame();
            SceneManager.LoadScene(startSceneName);
        });
        
        AddSceneChangeButton(mainMenuButton, () => 
        {
            SceneManager.LoadScene(mainMenuSceneName);
        });
    }

    
}
