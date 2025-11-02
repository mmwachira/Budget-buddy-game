using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Swipe : MonoBehaviour
{
    // --- Public Settings ---
    public float swipeForceMagnitude = 50f; // Base force applied for the swipe
    public float swipePixelToForceScale = 50f; // Divisor to convert screen pixels to force magnitude
    public float minClampedForce = 1f; // Minimum force applied
    public float maxClampedForce = 15f; // Maximum force applied (prevents overpowered swipes)

    // --- Private References ---
    private Rigidbody2D rb;
    private Vector3 touchStartPos; // Using Vector3 to hold screen coordinates (x, y, 0)
    private bool isTouching = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Swipe requires a Rigidbody2D component.");
    }

    void Update()
    {
        // 1. --- MOUSE INPUT (for PC testing) ---
        if (Input.GetMouseButtonDown(0))
        {
            HandleInputBegan(Input.mousePosition);
        }
        else if (isTouching && Input.GetMouseButtonUp(0))
        {
            HandleInputEnded(Input.mousePosition);
        }

        // 2. --- TOUCH INPUT (for mobile) ---
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                HandleInputBegan(touch.position);
            }
            else if (isTouching && touch.phase == TouchPhase.Ended)
            {
                HandleInputEnded(touch.position);
            }
        }
    }

    // --- Helper Functions ---

    private void HandleInputBegan(Vector3 screenPosition)
    {
        // Convert screen position to a world point for the raycast
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPosition);

        // Perform the Raycast to check if THIS GameObject was hit
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            // Touch/Click started on this item
            isTouching = true;
            touchStartPos = screenPosition;
        }
    }

    private void HandleInputEnded(Vector3 screenPosition)
    {
        if (isTouching)
        {
            Vector2 touchEndPos = screenPosition;
            Vector2 swipeVector = touchEndPos - (Vector2)touchStartPos; // Get the full delta

            // 1. Calculate the direction of the swipe (includes X and Y)
            Vector2 swipeDirection = swipeVector.normalized;

            // 2. Scale the swipe distance (pixels) to a force value
            // This allows the force to scale based on how long the swipe was.
            float swipeMagnitude = swipeVector.magnitude / swipePixelToForceScale;

            // 3. Clamp the final force to ensure it's playable (not too weak, not too strong)
            float finalForce = Mathf.Clamp(swipeMagnitude * swipeForceMagnitude, minClampedForce, maxClampedForce);

            // 4. Apply the Impulse Force
            rb.AddForce(swipeDirection * finalForce, ForceMode2D.Impulse);

            isTouching = false;
        }
    }
}