using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text;

    [Header("X, Y Position")]
    public int x;
    public int y;

    [Header("GameBoardContainer")]
    [SerializeField] GameBoardContainer gameBoardContainer;

    private int[,] gameBoard;

    private void Start()
    {
        gameBoard = gameBoardContainer.GameBoard;

        if(gameBoard[x,y] == 0)
        {
            text.text = " ";
        }
    }

    void Update()
    {
        switch(gameBoard[x,y])
        {
            case -1:
                text.text = "X";
                break;
            case 1 :
                text.text = "O";
                break;
            default:
                text.text = " ";
                break;
        }
    }
}
