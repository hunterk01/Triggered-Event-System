using UnityEngine;

public class TriggerBehavior : MonoBehaviour
{
    public bool showTriggerInEditor = true;
    [HideInInspector] public bool triggerActivated = false;

    [Header("Triggering Object")]
    public bool triggeredByPlayer = true;
    private GameObject player;
    public GameObject triggeredByObject;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (triggeredByPlayer && other.gameObject == player)
        {
            triggerActivated = true;
        }
        else if (!triggeredByPlayer && other.gameObject == triggeredByObject)
        {
            triggerActivated = true;
        }
    }

    public void OnDrawGizmos()
    {
        if (showTriggerInEditor)
        {
            Gizmos.color = Color.red;

            if (GetComponent<BoxCollider>())
            {
                Gizmos.DrawWireCube(transform.position, transform.localScale);
            }
            else if (GetComponent<SphereCollider>())
            {
                Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius * transform.localScale.x);
            }
        }
    }
}
