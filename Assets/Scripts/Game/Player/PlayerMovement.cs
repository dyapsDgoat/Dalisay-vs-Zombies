using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private AudioClip moveSound; // Add a field for the move sound
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip collisionSound; // Add a field for the collision sound

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 currentVelocity;
    private Animator _animator;
    private AudioSource audioSource; // Add an AudioSource field for move sound
    private AudioSource backgroundMusicSource;
    private AudioSource collisionAudioSource; // Add an AudioSource field for collision sound

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        _animator = GetComponent<Animator>();

        // Initialize the AudioSource component for move sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = moveSound;

        // Initialize the AudioSource component for background music
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        backgroundMusicSource.playOnAwake = false;
        backgroundMusicSource.loop = true; // Set to loop
        backgroundMusicSource.clip = backgroundMusic;

        // Initialize the AudioSource component for collision sound
        collisionAudioSource = gameObject.AddComponent<AudioSource>();
        collisionAudioSource.playOnAwake = false;
        collisionAudioSource.clip = collisionSound;
    }

    private void FixedUpdate()
    {
        movementInput = Vector2.SmoothDamp(
            movementInput,
            GetRawInput(),
            ref currentVelocity,
            smoothTime);

        rb.velocity = movementInput * speed;

        RotatePlayer();
        RotateTowardsMouse();

        ClampPosition();

        SetAnimation();

        // Play the move sound if the player is moving
        if (movementInput != Vector2.zero && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (movementInput == Vector2.zero && audioSource.isPlaying)
        {
            // Stop the sound if the player is not moving
            audioSource.Stop();
        }
    }

    private void SetAnimation()
    {
        bool IsMoving = movementInput != Vector2.zero;

        _animator.SetBool("IsMoving", IsMoving);
    }

    private void ClampPosition()
    {
        // Get the screen boundaries in world coordinates
        float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        float newX = Mathf.MoveTowards(transform.position.x, clampedX, Time.deltaTime * 10f);
        float newY = Mathf.MoveTowards(transform.position.y, clampedY, Time.deltaTime * 10f);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    private Vector2 GetRawInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void RotatePlayer()
    {
        if (movementInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void RotateTowardsMouse()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z));
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Start()
    {
        // Play the background music
        backgroundMusicSource.Play();
    }

private void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log("Collision detected");

    // Check if the collision is with an object that has the "CollisionTag" tag
    if (collision.gameObject.CompareTag("CollisionTag"))
    {
        Debug.Log("Playing collision sound");

        // Check if the collisionAudioSource and its clip are not null
        if (collisionAudioSource != null && collisionAudioSource.clip != null)
        {
            // Play the collision sound
            collisionAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("CollisionAudioSource or its clip is not set. Make sure to assign them in the Unity Editor.");
        }
    }
}


}
