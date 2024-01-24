using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private AudioClip enemyMovingSound;
    private AudioSource enemyMovingSoundSource;
    private Rigidbody2D rb;
    private PlayerAwarenessController playerAwarenessController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwarenessController = GetComponent<PlayerAwarenessController>();
        rb = GetComponent<Rigidbody2D>();

         // Initialize the AudioSource component for enemy moving sound
    enemyMovingSoundSource = gameObject.AddComponent<AudioSource>();
    enemyMovingSoundSource.playOnAwake = false;
    enemyMovingSoundSource.loop = true; // Set to loop
    enemyMovingSoundSource.clip = enemyMovingSound;


    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    rb.interpolation = RigidbodyInterpolation2D.Interpolate;

    }

    private void FixedUpdate()
    {
        ChasePlayer();
    }

private void ChasePlayer()
{
    Vector2 directionToPlayer = playerAwarenessController.GetDirectionToPlayer();

    if (directionToPlayer != Vector2.zero)
    {
        // Use LookAt to align the forward direction with the player
        transform.LookAt(transform.position + Vector3.forward, directionToPlayer);

        // Move towards the player
        rb.velocity = directionToPlayer.normalized * speed;

        // Play the enemy moving sound if it's not already playing
        if (!enemyMovingSoundSource.isPlaying)
        {
            enemyMovingSoundSource.Play();
        }
    }
    else
    {
        rb.velocity = Vector2.zero;

        // Stop the enemy moving sound if it's playing
        if (enemyMovingSoundSource.isPlaying)
        {
            enemyMovingSoundSource.Stop();
        }
    }
}

}
