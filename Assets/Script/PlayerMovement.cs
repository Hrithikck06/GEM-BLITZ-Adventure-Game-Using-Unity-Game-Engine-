using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 10f;

    public AudioSource crash;

    private int Score = 0;
    private int Health = 100;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HealthText;

    public GameObject LostPanel;
    public GameObject WinPanel;

    private Animator animator;
    private float fixedY;

    void Start()
    {
        animator = GetComponent<Animator>();

        fixedY = transform.position.y;

        if (WinPanel != null)
            WinPanel.SetActive(false);

        if (ScoreText != null)
            ScoreText.text = Score.ToString();

        if (HealthText != null)
            HealthText.text = Health.ToString();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Get the camera
        Transform cameraTransform = Camera.main.transform;

        // Camera forward and right
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // Keep movement on the ground
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Camera-relative movement
        Vector3 move =
            cameraForward * vertical +
            cameraRight * horizontal;

        // Prevent diagonal movement from being faster
        if (move.magnitude > 1f)
            move.Normalize();

        bool isMoving = move.magnitude > 0.01f;

        // Shift + WASD = Run
        bool isRunning =
            Input.GetKey(KeyCode.LeftShift) && isMoving;

        float currentSpeed =
            isRunning ? runSpeed : speed;

        if (isMoving)
        {
            // Move player
            Vector3 newPosition =
                transform.position +
                move * currentSpeed * Time.deltaTime;

            // Keep player on ground
            newPosition.y = fixedY;

            transform.position = newPosition;

            // Rotate player toward movement direction
            Quaternion targetRotation =
                Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Animations
        if (animator != null)
        {
            animator.SetBool("isWalking", isMoving);
            animator.SetBool("IsRunning", isRunning);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Wall collision
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (crash != null)
                crash.Play();
        }

        // Gem collection
        if (collision.gameObject.CompareTag("Gems"))
        {
            if (crash != null)
                crash.Play();

            collision.gameObject.SetActive(false);

            Score += 10;

            if (ScoreText != null)
                ScoreText.text = Score.ToString();

            // Win condition
            if (Score >= 40)
            {
                Time.timeScale = 0;

                if (WinPanel != null)
                    WinPanel.SetActive(true);

                GameObject enemy =
                    GameObject.FindGameObjectWithTag("Enemy");

                if (enemy != null)
                    Destroy(enemy);

                GameObject homeLand =
                    GameObject.FindGameObjectWithTag("Home");

                if (homeLand != null)
                    homeLand.GetComponent<Renderer>().material.color =
                        Color.green;
            }
        }

        // Enemy collision
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (crash != null)
                crash.Play();

            Health -= 10;

            if (HealthText != null)
                HealthText.text = Health.ToString();

            if (Health <= 10)
            {
                Time.timeScale = 0;

                if (LostPanel != null)
                    LostPanel.SetActive(true);
            }
        }
    }
}