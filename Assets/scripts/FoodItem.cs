using UnityEngine;
using System.Collections;

public class FoodItem : MonoBehaviour {

    // Temporary reference to customer
    public Customer customer;
    
    public bool isBurrito
    {
        get
        {
            return GetComponentsInChildren<Tortilla>().Length > 0;
        }
    }

    public Tortilla tortilla
    {
        get
        {
            if (isBurrito)
            {
                return GetComponentsInChildren<Tortilla>()[0];
            } else
            {
                return null;
            }
        }
    }
}
