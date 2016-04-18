using UnityEngine;
using System.Collections;

public class Burrito : MonoBehaviour {

    public float cookTime = 5f;
    public float timeCooked = 0;
    Color startColor;

    void Start()
    {
        startColor = GetComponent<Renderer>().material.color;
    }

    void Update()
    {
        // COOK_TIME * 2 makes it so the color will turn black if you cook it for twice as long as needed.
        GetComponent<Renderer>().material.color = Color.Lerp(startColor, Color.black, timeCooked / (cookTime * 2));
    }

    void OnCollisionEnter(Collision collision)
    {
        IngredientServing serving = collision.collider.GetComponent<IngredientServing>();
        if (serving != null)
        {
            GameObject servingObject = serving.gameObject;
            Destroy(servingObject.GetComponent<Rigidbody>());
            //servingObject.GetComponent<Collider>().isTrigger = true;

            servingObject.transform.parent = transform;
        }
    }
}
