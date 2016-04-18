using UnityEngine;
using System.Collections;

public class Tortilla : MonoBehaviour {

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
}
