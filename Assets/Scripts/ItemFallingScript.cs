using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Ensure the Rigidbody is present
public class ItemFallingScript : MonoBehaviour
{
    // The force/speed we apply to make it fall faster than gravity alone
    public float downwardForce = 1f;

    // Y position where the item gets destroyed
    public float destroyY = -6f;

    private Rigidbody2D rb;

    void Awake()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Ensure gravity is enabled on the Rigidbody to handle basic falling
        // You can set the scale in the Rigidbody component itself, or here:
        // rb.gravityScale = 1.0f; 
    }

    void FixedUpdate()
    {
        // FixedUpdate is the best place to apply physics updates

        // Apply a constant downward force to control the fall speed.
        // This is often better than trying to set velocity directly, as it combines with gravity.
        rb.AddForce(Vector2.down * downwardForce);
    }

    void Update()
    {
        // Check for destruction in Update, as it's not a physics calculation
        if (transform.position.y < destroyY)
        {
            // You might want to notify a Game Manager here before destroying
            // e.g., GameManager.Instance.ItemMissed();
            Destroy(gameObject);
        }
    }
}