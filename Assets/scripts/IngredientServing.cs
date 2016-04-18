using UnityEngine;
using System.Collections;

public class IngredientServing : MonoBehaviour {

    public string type;
    private Color _color;
    public Color color
    {
        get
        {
            return _color;
        }
        set
        {
            GetComponent<Renderer>().material.color = value;
            _color = value;
        }
    }
}
