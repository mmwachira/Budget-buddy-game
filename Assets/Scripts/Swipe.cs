using UnityEngine;
using UnityEngine.InputSystem;   // NEW INPUT SYSTEM

[RequireComponent(typeof(Rigidbody2D))]
public class Swipe : MonoBehaviour
{
    [Header("Swipe Settings")]
    public float swipeForceMagnitude = 50f;
    public float swipePixelToForceScale = 50f;
    public float minClampedForce = 1f;
    public float maxClampedForce = 15f;

    private Rigidbody2D rb;

    private Vector2 touchStartPos;
    private bool isTouching = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Swipe requires a Rigidbody2D component.");
    }

    void Update()
    {
        HandleMouseInput();
        HandleTouchInput();
    }

    // ----------------------------------------
    // MOUSE INPUT (new system still supports using mouse API)
    // ----------------------------------------
    void HandleMouseInput()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleInputBegan(Mouse.current.position.ReadValue());
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            HandleInputEnded(Mouse.current.position.ReadValue());
        }
    }

    // ----------------------------------------
    // TOUCH INPUT (New Input System style)
    // ----------------------------------------
    void HandleTouchInput()
    {
        if (Touchscreen.current == null) return;

        var touch = Touchscreen.current.primaryTouch;

        if (touch.press.wasPressedThisFrame)
        {
            HandleInputBegan(touch.position.ReadValue());
        }
        else if (touch.press.wasReleasedThisFrame)
        {
            HandleInputEnded(touch.position.ReadValue());
        }
    }

    // ----------------------------------------
    // Start Input
    // ----------------------------------------
    private void HandleInputBegan(Vector2 screenPosition)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isTouching = true;
            touchStartPos = screenPosition;
        }
    }

    // ----------------------------------------
    // End Input → Apply Swipe Force
    // ----------------------------------------
    private void HandleInputEnded(Vector2 screenPosition)
    {
        if (!isTouching) return;

        Vector2 swipeVector = screenPosition - touchStartPos;

        Vector2 swipeDirection = swipeVector.normalized;
        float swipeMagnitude = swipeVector.magnitude / swipePixelToForceScale;

        float finalForce = Mathf.Clamp(
            swipeMagnitude * swipeForceMagnitude,
            minClampedForce,
            maxClampedForce
        );

        rb.AddForce(swipeDirection * finalForce, ForceMode2D.Impulse);

        isTouching = false;
    }
}
