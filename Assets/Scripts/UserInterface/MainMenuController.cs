// MainMenuController.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuController : MonoBehaviour
{
    [Header("SFX")]
    public AudioSource buttonHoverSound;
    public AudioSource buttonClickSound;
    public AudioSource menuMusic;
    
    [Header("Scenes")]
    #if UNITY_EDITOR
    public SceneAsset startScene;
    public SceneAsset tutorialScene;
    #endif

    [HideInInspector] public string startSceneName;
    // [HideInInspector] public string tutorialSceneName;

    [Header("Buttons")]
    public Button startButton;
    public Button tutorialButton;
    public Button quitButton;

    // Add HowToPlayPanel reference
    [Header("How to Play Panel (Overlay)")]
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private Button closeHowToPlayButton;

    void OnValidate()
    {
        #if UNITY_EDITOR
        if (startScene != null)
            startSceneName = startScene.name;
        /*
        if (tutorialScene != null)
            tutorialSceneName = tutorialScene.name;
        */
        #endif
    }
    
    void Start()
    {
        if (menuMusic != null && !menuMusic.isPlaying)
            menuMusic.Play();

        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartClicked);
            AddHoverSound(startButton);
        }


        if (tutorialButton != null)
        {
            // Previously: OnTutorialClicked() for loading a separate scene
            // Now using OnHowToPlayOverlayClicked() for overlay display
            tutorialButton.onClick.AddListener(OnHowToPlayOverlayClicked);
            AddHoverSound(tutorialButton);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitClicked);
            AddHoverSound(quitButton);
        }

        // Make sure the HowToPlay panel is disabled at startup
        if (howToPlayPanel != null) 
            howToPlayPanel.SetActive(false);

        // Setup the Close button on the panel
        if (closeHowToPlayButton != null)
        {
            closeHowToPlayButton.onClick.AddListener(OnCloseHowToPlay);
            AddHoverSound(closeHowToPlayButton);
        }
    }

    void OnStartClicked()
    {
        PlayClickSound();
        SceneManager.LoadScene(startSceneName);
    }

    void OnHowToPlayOverlayClicked()
    {
        PlayClickSound();
        // Show the overlay panel
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(true);
    }


/*
    // Original method for loading the tutorial scene (now unused):
    void OnTutorialClicked()
    {
        PlayClickSound();
        SceneManager.LoadScene(tutorialSceneName);
    }
    */

    void OnCloseHowToPlay()
    {
        PlayClickSound();
        if (howToPlayPanel != null)
            howToPlayPanel.SetActive(false);
    }


    void OnQuitClicked()
    {
        PlayClickSound();
        Application.Quit();
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
}
