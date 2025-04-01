using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public GameObject ButtonHoverSound;
    public GameObject ButtonClickSound;

    private AudioSource hoverAudio;
    private AudioSource clickAudio;

    private void Start()
    {
        if (ButtonHoverSound != null)
            hoverAudio = ButtonHoverSound.GetComponent<AudioSource>();

        if (ButtonClickSound != null)
            clickAudio = ButtonClickSound.GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverAudio != null)
            hoverAudio.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickAudio != null)
            clickAudio.Play();
    }
}