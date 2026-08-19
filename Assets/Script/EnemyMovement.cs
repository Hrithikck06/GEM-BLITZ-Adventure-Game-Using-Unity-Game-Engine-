using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private float speed = 5f; // Enemy movement speed

    [SerializeField]
    private float rotationSpeed = 5f; // Enemy turning speed

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found!");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // Move enemy towards player
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        // Rotate enemy towards player
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}