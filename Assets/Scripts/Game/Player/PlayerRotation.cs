using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to a point in the game world
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.z));

        // Calculate the direction from the player to the mouse pointer
        Vector2 direction = (mouseWorldPosition - transform.position).normalized;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set the player's rotation to the calculated angle
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
