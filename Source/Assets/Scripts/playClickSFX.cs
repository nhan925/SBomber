using UnityEngine;

public class playClickSFX : MonoBehaviour
{
    audio_manager aud;

    private void Start()
    {
        aud = FindObjectOfType<audio_manager>();
    }

    public void ClickSFX()
    {
        aud.playSFX(aud.buttonClick);
    }
}
