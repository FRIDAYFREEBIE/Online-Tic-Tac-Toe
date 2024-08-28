using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text;

    [Header("GameBoardContainer")]
    [SerializeField] private GameBoardContainer gameBoardContainer;

    char turn;

    void Update()
    {
        if(gameBoardContainer.Turn == -1)
            turn = 'X';
        else
            turn = 'O';

        text.text = turn + "'s Turn";
    }
}
