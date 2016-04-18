using UnityEngine;
using System.Collections;
using System;

public class HotPlate : MonoBehaviour {

    public GameObject hotPadLight;

    FoodItem _currentFoodItem;
    public FoodItem currentFoodItem
    {
        get
        {
            return _currentFoodItem;
        }
        set
        {
            if (value != null)
            {
                if (!value.isBurrito)
                {
                    throw new ArgumentException("currentFoodItem must be a burrito.");
                }
                value.transform.position = transform.position + new Vector3(0, .05f, 0);
            }
            _currentFoodItem = value;
        }
    }

    void Start () {
	    
	}

    void UpdateHotPadLight()
    {
        Color hotPadColor = new Color(1, 0, 0);
        if (currentFoodItem != null && currentFoodItem.isBurrito)
        {
            Tortilla tortilla = currentFoodItem.tortilla;
            if (tortilla.timeCooked > tortilla.cookTime)
            {
                hotPadColor = new Color(0, 1, 0);
            }
        }
        hotPadLight.GetComponent<Renderer>().material.color = hotPadColor;
    }

	void Update () {
        UpdateHotPadLight();
        if (currentFoodItem != null && currentFoodItem.isBurrito)
        {
            currentFoodItem.tortilla.timeCooked += Time.deltaTime;
        }
    }
}
