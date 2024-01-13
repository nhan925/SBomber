using UnityEngine;
using UnityEngine.UI;

public class resumeButton : MonoBehaviour
{
    PauseTime pt;
    void Start()
    {
        pt = FindObjectOfType<PauseTime>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pt.escKey = true;
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
