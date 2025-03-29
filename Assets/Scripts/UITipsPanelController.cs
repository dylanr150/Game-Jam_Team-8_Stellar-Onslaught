using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITipsPanelController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI hintsText;    // The text component for tutorial messages
    [SerializeField] private Button backButton;            // The 'Back' button
    [SerializeField] private Button nextButton;            // The 'Next' or 'OK' button

    [SerializeField] private TextMeshProUGUI pageIndicatorText; // Page Index


    [Header("Tutorial Pages")]
    [TextArea]
    [SerializeField] private string[] tutorialPages = {
        "Welcome to Stellar Onslaught!\nPress A or D to move left or right.",
        "Press Space to shoot bullets.\nDestroy enemies to earn points!",
        "Stay alive and defeat the final boss to win.\nGood luck, pilot!"

        // "Welcome to Stellar Onslaught!",
        // "Press A or D to move left or right.",
        // "Press Space to shoot bullets.",
        // "Destroy enemies to earn points!",
        // "Stay alive and defeat the final boss to win.\nGood luck, pilot!"

        // "Press A/D to move",
        // "Press Space to shoot",
        // "Good luck, pilot!"
    };

    private int currentPageIndex = 0;

    void Start()
    {
        // Initialize the first page
        ShowCurrentPage();
    }

    void OnEnable()
    {
        // Register button click listeners
        if (backButton != null)
            backButton.onClick.AddListener(OnBackClicked);

        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextClicked);
    }

    void OnDisable()
    {
        // Unregister listeners to avoid memory leaks
        if (backButton != null)
            backButton.onClick.RemoveListener(OnBackClicked);

        if (nextButton != null)
            nextButton.onClick.RemoveListener(OnNextClicked);
    }

    private void ShowCurrentPage()
    {
        // Update the main text
        if (hintsText != null && currentPageIndex < tutorialPages.Length)
        {
            hintsText.text = tutorialPages[currentPageIndex];
        }

        // Page number label (Example: "Page 1/3")
        if (pageIndicatorText != null)
        {
            pageIndicatorText.text = $"Page {currentPageIndex + 1}/{tutorialPages.Length}";
        }


        // Handle Back button visibility
        if (backButton != null)
        {
            // If we're on the first page, hide the Back button; otherwise show it
            backButton.gameObject.SetActive(currentPageIndex > 0);
        }

        // Handle Next/OK button label
        if (nextButton != null)
        {
            TextMeshProUGUI btnLabel = nextButton.GetComponentInChildren<TextMeshProUGUI>();
            if (btnLabel != null)
            {
                if (currentPageIndex == tutorialPages.Length - 1)
                {
                    // If it's the last page, label the button as "OK"
                    btnLabel.text = "OK";
                }
                else
                {
                    // Otherwise, label it "Next"
                    btnLabel.text = "Next";
                }
            }
        }
    }

    private void OnNextClicked()
    {
        // If we're on the last page, close the panel
        if (currentPageIndex == tutorialPages.Length - 1)
        {
            gameObject.SetActive(false);
            return;
        }

        // Otherwise, go to the next page
        currentPageIndex++;
        ShowCurrentPage();
    }

    private void OnBackClicked()
    {
        // Decrement page index, but not below 0
        currentPageIndex--;
        if (currentPageIndex < 0)
        {
            currentPageIndex = 0;
        }

        ShowCurrentPage();
    }
}
