using UnityEngine;

public enum Category { Need, Want, Miss }

public class ItemType : MonoBehaviour
{
    public Category itemCategory;

    // How much this item affects budget
    public float itemCost = 10f;

    // Optional, if you want scoring later
    public int pointsValue = 1;
}
