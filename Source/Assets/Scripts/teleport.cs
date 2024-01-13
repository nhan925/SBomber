using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform destination;
    GameObject player;
    audio_manager aud;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aud = FindObjectOfType<audio_manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.48f)
            {
                aud.playSFX(aud.teleport);
                player.transform.position = destination.position;
            }
        }
    }
}
