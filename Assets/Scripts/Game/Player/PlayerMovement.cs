using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothTime = 0.1f;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 currentVelocity;
    private Animator _animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        _animator = GetComponent<Animator>();
    }

private void FixedUpdate()
{
    // Smoothly damp the movement input over time
    movementInput = Vector2.SmoothDamp(
        movementInput,
        GetRawInput(),
        ref currentVelocity,
        smoothTime);

    // Apply the smoothed movement input to the player's velocity
    rb.velocity = movementInput * speed;

    // Rotate the player based on the movement direction
    RotatePlayer();
    // Rotate the player towards the mouse pointer
    RotateTowardsMouse();

    // Prevent the player from leaving the screen
    ClampPosition();

    //Animation for walking
    SetAnimation();
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

    // Calculate the clamped position
    float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
    float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

    // Smoothly move towards the clamped position
    float newX = Mathf.MoveTowards(transform.position.x, clampedX, Time.deltaTime * 10f);
    float newY = Mathf.MoveTowards(transform.position.y, clampedY, Time.deltaTime * 10f);

    // Update the player's position
    transform.position = new Vector3(newX, newY, transform.position.z);
}



    private Vector2 GetRawInput()
    {
        // Get raw input from the Input System
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
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // Convert the mouse position to a point in the game world
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z));

        // Calculate the direction from the player to the mouse pointer
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the player's rotation to the calculated angle
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}