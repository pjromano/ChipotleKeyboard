using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Worker : MonoBehaviour {

    public Vector3 workerOffset = new Vector3(0, 1.0f, -1.5f);
    public Vector3 foodItemOffset = new Vector3(0, 0.5f, 0.0f);
    public Vector3 servingOffset = new Vector3(0, 1.0f, 1.0f); // relative to worker
    public Ingredient startIngredient;
    public FoodItem foodItemPrefab;
    public Tortilla tortillaPrefab;
    public Customer customerPrefab;
    public IngredientServing ingredientServingPrefab;
    Ingredient[] ingredients;
    Ingredient _currentIngredient;
    Ingredient currentIngredient
    {
        get
        {
            return _currentIngredient;
        }
        set
        {
            _currentIngredient = value;
            transform.position = new Vector3(value.transform.position.x, 0.0f, value.transform.position.z) + workerOffset;
            UpdateFoodItemPosition();
        }
    }

    FoodItem _currentFoodItem;
    FoodItem currentFoodItem
    {
        get
        {
            return _currentFoodItem;
        }
        set
        {
            _currentFoodItem = value;
            UpdateFoodItemPosition();
        }
    }

    GameState gameState;

    void UpdateFoodItemPosition()
    {
        if (_currentFoodItem != null)
        {
            _currentFoodItem.transform.position = transform.position + foodItemOffset;
        }
    }

	void Start () {
        ingredients = FindObjectsOfType(typeof(Ingredient)) as Ingredient[];
        Array.Sort(ingredients);
        Console.WriteLine(ingredients);
        gameState = FindObjectOfType<GameState>();

        currentIngredient = startIngredient;
	}

    void Update()
    {
        int index = currentIngredient.index;
        if (Input.GetKeyDown("left"))
        {
            index++;
        }
        if (Input.GetKeyDown("right"))
        {
            index--;
        }
        if (index >= ingredients.Length)
        {
            index = ingredients.Length - 1;
        }
        if (index < 0)
        {
            index = 0;
        }
        if (currentIngredient.index != index)
        {
            currentIngredient = ingredients[index];
        }

        if (Input.GetKeyDown("space"))
        {
            // Tortilla stack
            if (currentIngredient.index == 0)
            {
                if (currentFoodItem != null)
                {
                    Destroy(currentFoodItem.gameObject);
                }
                currentFoodItem = Instantiate(foodItemPrefab);
                Tortilla tortilla = Instantiate(tortillaPrefab);
                tortilla.transform.parent = currentFoodItem.transform;
                tortilla.transform.localPosition = new Vector3(0, 0, 0);

                Customer customer = Instantiate(customerPrefab);
                customer.foodItem = currentFoodItem;
                currentFoodItem.customer = customer;
            }
            // Hot plate
            else if (currentIngredient.index == 1)
            {
                HotPlate hotPlate = currentIngredient.GetComponent<HotPlate>();
                if (currentFoodItem == null)
                {
                    currentFoodItem = hotPlate.currentFoodItem;
                    hotPlate.currentFoodItem = null;
                }
                else if (currentFoodItem != null && currentFoodItem.isBurrito)
                {
                    FoodItem temp = hotPlate.currentFoodItem;
                    hotPlate.currentFoodItem = currentFoodItem;
                    currentFoodItem = temp;
                }
            }
            // Cash register
            else if (currentIngredient.index == ingredients.Length - 1)
            {
                if (currentFoodItem != null)
                {
                    UpdateScore(currentFoodItem);
                    Destroy(currentFoodItem.customer.gameObject);
                    Destroy(currentFoodItem.gameObject);
                }
            }
            // Ingredients that are scooped into burrito (i.e. actual ingredients)
            else
            {
                IngredientServing serving = Instantiate(ingredientServingPrefab);
                serving.transform.position = transform.position + servingOffset;
                serving.transform.position += new Vector3(UnityEngine.Random.value * 0.04f - 0.02f,
                    0.0f, UnityEngine.Random.value * 0.05f - 0.02f);

                // Inherit properties from ingredient bin
                // This will change when graphics are improved
                serving.type = currentIngredient.type;
                serving.color = currentIngredient.color;
            }
        }

        if (Input.GetKey("q"))
        {
            Application.Quit();
        }
    }

    void UpdateScore(FoodItem item)
    {
        IngredientServing[] servings = item.GetComponentsInChildren<IngredientServing>();

        int numCorrect = 0;

        var requirements = item.customer.requirements;
        foreach (var requirement in requirements)
        {
            int count = 0;
            foreach (var serving in servings)
            {
                if (serving.type == requirement.Key)
                {
                    count += 1;
                }
            }
            if (count == requirement.Value)
            {
                numCorrect += 1;
            }
        }

        float scoreChange = requirements.Count > 0 ? ((float)numCorrect) / requirements.Count : 1;
        gameState.score += scoreChange;
    }
}