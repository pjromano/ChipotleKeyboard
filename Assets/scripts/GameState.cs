using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    public Text scoreText;

    float _score;
    public float score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            scoreText.text = string.Format("Points: {0:f2}", _score);
        }
    }
}
