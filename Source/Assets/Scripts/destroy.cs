using UnityEngine;

public class destroy : MonoBehaviour
{
    [SerializeField] float destroyTime = 0.25f;
    [HideInInspector] public float spawnItemProb;
    public GameObject[] spawnItems;
    public GameObject portal;
    [HideInInspector] public bool isBrick = false;
    private float curProb;
    [HideInInspector] public Vector3 spawnPortalPos;
    movements_controller m_ctrl;

    void Start()
    {
        m_ctrl = FindObjectOfType<movements_controller>();
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        curProb = spawnItemProb;
    }

    private void OnDestroy()
    {
        if (transform.position == spawnPortalPos && !m_ctrl.existPortal)
        {
            Instantiate(portal, transform.position, Quaternion.identity);
            m_ctrl.existPortal = true;
            return;
        }
        if (spawnItems.Length > 0 && Random.value < curProb && isBrick)
        {
            int randomIndex = Random.Range(0, spawnItems.Length);
            Instantiate(spawnItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
