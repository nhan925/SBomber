using UnityEngine;

public class resetData : MonoBehaviour
{
   void Start()
    {
        StaticData.s_score = 0;
        StaticData.s_time = 0f;
        StaticData.s_bombamount = 1;
        StaticData.s_remainbomb = 1;
        StaticData.s_lucky = 0.1f;
        StaticData.s_blast = 1;
        StaticData.s_speed = 4f;
        StaticData.s_live = 1;
        StaticData.s_itemCount = 0;
    }
}
