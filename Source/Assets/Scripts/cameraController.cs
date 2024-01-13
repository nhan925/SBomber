using UnityEngine;

public class BombermanCamera : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, 5f * Time.deltaTime);
    }

    
}
