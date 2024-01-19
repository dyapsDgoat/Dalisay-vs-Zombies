using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }

    [SerializeField] private float playerAwarenessDistance = 5f;
    [SerializeField] private float maxChaseDistance = 10f; // Maximum chasing distance
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update()
    {
        UpdatePlayerAwareness();
    }

    private void UpdatePlayerAwareness()
    {
        Vector2 enemyToPlayerVector = playerTransform.position - transform.position;
        float distanceToPlayer = enemyToPlayerVector.magnitude;

        if (distanceToPlayer <= playerAwarenessDistance && distanceToPlayer <= maxChaseDistance)
        {
            DirectionToPlayer = enemyToPlayerVector.normalized;
            AwareOfPlayer = true;
        }
        else
        {
            DirectionToPlayer = Vector2.zero;
            AwareOfPlayer = false;
        }
    }

    public Vector2 GetDirectionToPlayer()
    {
        return DirectionToPlayer;
    }
}
