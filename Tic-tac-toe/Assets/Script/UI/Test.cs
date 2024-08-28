using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public GameManager gameManager;
    public GameBoardContainer gameBoardContainer;

    void Update()
    {
        string newString;

        newString = "gameManager turn:" + gameManager.myTurn.ToString();
        newString += "\n gameBoardContainer turn: " +  gameBoardContainer.Turn.ToString();

        textMeshProUGUI.text = newString;
    }
}
