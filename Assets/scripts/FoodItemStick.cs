using UnityEngine;
using System.Collections;

public class FoodItemStick : MonoBehaviour {
    
    void OnCollisionEnter(Collision collision)
    {
        IngredientServing serving = collision.collider.GetComponent<IngredientServing>();
        if (serving != null)
        {
            GameObject servingObject = serving.gameObject;
            Destroy(servingObject.GetComponent<Rigidbody>());
            //servingObject.GetComponent<Collider>().isTrigger = true;

            servingObject.transform.parent = transform.parent;
        }
    }
}
