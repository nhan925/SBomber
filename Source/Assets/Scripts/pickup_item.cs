using UnityEngine;

public class pickup_item : MonoBehaviour
{
   public enum ItemType
    {
        MoreBomb,
        SpeedUp,
        ExplodeRadius,
        MoreLife,
        Lucky,
    }
    public ItemType Type;
    UI ui;
    audio_manager aud;

    private void Start()
    {
        ui = FindObjectOfType<UI>();
        aud = FindObjectOfType<audio_manager>();
    }

    private void PickUpItem(GameObject player)
    {
        aud.playSFX(aud.itemPickup);
        bomb_controller bombCtrl = player.GetComponent<bomb_controller>();
        movements_controller moveCtrl = player.GetComponent<movements_controller>();
        if (Type == ItemType.MoreBomb && bombCtrl.bombAmount < bombCtrl.max_bomb)
        {
            bombCtrl.bombAmount++;
            bombCtrl.remainBomb++;
            ui.setBombtext((bombCtrl.bombAmount - 1).ToString());
        }
        else if (Type == ItemType.SpeedUp && moveCtrl.speed < moveCtrl.max_speed)
        {
            moveCtrl.speed += 0.5f;
            ui.setSpeedtext(Mathf.Round(2 * moveCtrl.speed - 8).ToString());
        }
        else if (Type == ItemType.ExplodeRadius && bombCtrl.explodeSize < bombCtrl.max_explodeSize)
        {
            bombCtrl.explodeSize++;
            ui.setRaidustext((bombCtrl.explodeSize - 1).ToString());
        }
        else if (Type == ItemType.MoreLife && bombCtrl.lives < bombCtrl.max_lives)
        {
            bombCtrl.lives++;
            ui.setLivetext((bombCtrl.lives - 1).ToString());
        }
        else if (Type == ItemType.Lucky && bombCtrl.ItemProb < bombCtrl.max_ItemProb)
        {
            bombCtrl.ItemProb += 0.02f;
            ui.setLuckytext(Mathf.Round(50f * bombCtrl.ItemProb - 5f).ToString());
        }
        StaticData.s_itemCount++;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUpItem(collision.gameObject);
        }
    }
}
