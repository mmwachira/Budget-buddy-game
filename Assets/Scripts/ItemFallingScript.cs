using UnityEngine;

public class ItemFallingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float fallSpeed = 1f; // Speed at which item falls
                                 //public float destroyY = -6f; // Y position where the item gets destroyed


    void Update()
    {
        // Move item down
        // transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // // Destroy if offscreen
        // if (transform.position.y < destroyY)
        // {
        //     Destroy(gameObject);
        // }
    }
}
