using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;

    // Camera position relative to player
    public float distance = 4f;
    public float height = 2.2f;

    // Camera rotation
    public float mouseSensitivity = 3f;

    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 50f;

    private float yaw = 0f;
    private float pitch = 10f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (player != null)
        {
            // Start camera behind player
            yaw = player.eulerAngles.y;
        }
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        // Mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * mouseSensitivity;
        pitch -= mouseY * mouseSensitivity;

        pitch = Mathf.Clamp(
            pitch,
            minVerticalAngle,
            maxVerticalAngle
        );

        // Camera rotation
        Quaternion rotation =
            Quaternion.Euler(pitch, yaw, 0f);

        // Position around player's head
        Vector3 target =
            player.position + Vector3.up * height;

        Vector3 offset =
            rotation * new Vector3(
                0f,
                0f,
                -distance
            );

        // Set camera position
        transform.position = target + offset;

        // Look toward player's head
        transform.LookAt(target);
    }
}