using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTurnManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        char newChar;
        int turn = gameManager.myTurn;

        if(turn == -1)
            newChar = 'X';
        else
            newChar = 'O';

            text.text = "You are " + newChar.ToString();
    }
}
