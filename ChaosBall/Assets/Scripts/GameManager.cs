using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GoalScript blue, red, green, orange;
    private bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = blue.isSolve && red.isSolve && green.isSolve && orange.isSolve;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            Rect rect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 75);
            GUI.Box(rect, "Game Over");

            Rect rect2 = new Rect(Screen.width / 2 - 30, Screen.height / 2 - 25, 60, 50);
            GUI.Label(rect2, "Good Job!");
        }
    }
}
