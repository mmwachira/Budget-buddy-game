using UnityEngine;

public class Swipe : MonoBehaviour
{
    public float swipeForce = 100f; // Magnitude of the swipe force
    private Rigidbody2D rb;
    private Vector2 touchStartPos;
    private bool isTouching = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Ensure Rigidbody2D exists for falling/movement
        if (rb == null) Debug.LogError("Swiper requires a Rigidbody2D component.");
    }

    void Update()
    {
        // --- MOUSE INPUT (for testing on PC) ---
        if (Input.GetMouseButtonDown(0))
        {
            HandleInputBegan(Input.mousePosition);
        }
        else if (isTouching && Input.GetMouseButtonUp(0))
        {
            HandleInputEnded(Input.mousePosition);
        }

        // --- TOUCH INPUT (for mobile) ---
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

    // --- NEW HELPER METHODS ---

    private void HandleInputBegan(Vector3 screenPosition)
    {
        // 1. Convert screen position to a world point
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPosition);

        // 2. Perform the Raycast
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        // 3. Check if THIS GameObject was hit
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isTouching = true;
            touchStartPos = screenPosition;
            // The item is now picked up!
        }
    }

    private void HandleInputEnded(Vector3 screenPosition)
    {
        if (isTouching)
        {
            Vector2 touchEndPos = screenPosition;
            Vector2 swipeVector = touchEndPos - touchStartPos;

            // Apply force based on swipe direction (as before)
            Vector2 horizontalSwipe = new Vector2(swipeVector.x, 0f).normalized;
            rb.AddForce(horizontalSwipe * swipeForce, ForceMode2D.Impulse);

            isTouching = false;
            // The item is released!
        }
    }
}