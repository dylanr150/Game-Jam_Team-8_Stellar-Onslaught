using UnityEngine;

public class TutorialHintsCanvasController : MonoBehaviour
{
    void Start()
    {
        if (!GameManager.Instance.ShowTutorial())
        {
            Debug.Log("Disabled tutorial canvas");
            gameObject.SetActive(false);
        }
    }
}
