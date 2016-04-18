using UnityEngine;
using System.Collections;
using System;

public class Ingredient : MonoBehaviour, IComparable<Ingredient> {

    public int index;
    public string type;
    public Color color;

    void Start()
    {
        TextMesh mesh = GetComponentInChildren<TextMesh>();
        // Ugly hack required because Unity Inspector doesn't allow newlines in fields
        mesh.text = type == null ? "" : type.Replace("\\n", "\n");
        mesh.color = color;
    }

    public int CompareTo(Ingredient other)
    {
        if (this.index < other.index)
        {
            return -1;
        }
        else if (this.index == other.index)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
