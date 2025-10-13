using UnityEngine;

// Define an enumeration for the item categories
public enum Category { Need, Want, Saving }

public class ItemType : MonoBehaviour
{
    // Make this public so you can set the item's type in the Inspector
    public Category itemCategory;

    // You can also add point values here if they vary by item type
    // public int pointsValue = 1; 
}