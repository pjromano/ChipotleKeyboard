using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Customer : MonoBehaviour {

    public FoodItem foodItem;
    public Dictionary<string, int> requirements = new Dictionary<string, int>();

    void Start()
    {
        Ingredient[] allIngredients = FindObjectsOfType<Ingredient>();
        System.Array.Sort(allIngredients);
        for (int i = 2; i < allIngredients.Length - 1; i++)
        {
            int numNeeded = Random.Range(-1, 4);
            if (numNeeded > 0)
            {
                requirements.Add(allIngredients[i].type.Replace("\\n", " "), numNeeded);
            }
        }

        string requirementStr = "Requirements:\n";
        foreach(var foodType in requirements)
        {
            requirementStr += foodType.Key + ": " + foodType.Value + "\n";
        }
        GetComponentInChildren<TextMesh>().text = requirementStr;
    }

	void Update () {
        transform.position = new Vector3(foodItem.transform.position.x, transform.position.y, transform.position.z);
	}
}
