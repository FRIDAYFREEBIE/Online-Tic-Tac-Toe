using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("GameBoardContainer")]
    [SerializeField] private GameBoardContainer gameBoardContainer;

    [Header("Winner UI")]
    [SerializeField] private GameObject XisWinner; 
    [SerializeField] private GameObject OisWinner;
    [SerializeField] private GameObject tie; 

    private GameObject[] allBoxs;

    int[,] gameBoard;
    public int myTurn;


    void Start()
    {
        gameBoardContainer.ResetContainer();
        
        gameBoard = gameBoardContainer.GameBoard;

        XisWinner.SetActive(false);
        OisWinner.SetActive(false);
        tie.SetActive(false);

        allBoxs = GameObject.FindGameObjectsWithTag("Box");

        if (PhotonNetwork.IsMasterClient)
            myTurn = -1; // X
        else
            myTurn = 1; // O
    }

    void Update()
    {
        int result = Win();

        switch(result)
        {
            case -1:
                XisWinner.SetActive(true);
                Invoke("GoToLobby", 3f);
                break;
            case 1:
                OisWinner.SetActive(true);
                Invoke("GoToLobby", 3f);
                break;
            default:
                break;
        }

        if(gameBoardContainer.Clicks == 9)
        {
            tie.SetActive(true);
            Invoke("GoToLobby", 3f);
        }

    }

    // 로비 씬으로 이동
    public void GoToLobby()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }

    // X: -1, O = 1, 승자 없음 0
    public int Win()
    {
        int x = gameBoard.GetLength(0); // 행
        int y = gameBoard.GetLength(1); // 열

        // 행 검사
        for (int i = 0; i < x; i++)
        {
            if (gameBoard[i, 0] != 0) // 행의 첫 번째 값이 0이 아닐 때
            {
                bool same = true;

                for (int j = 1; j < y; j++)
                {
                    if (gameBoard[i, j] != gameBoard[i, 0]) // 첫 번째 값과 다르다면
                    {
                        same = false;
                        break;
                    }
                }

                if (same) // 첫 번째 값과 값다면
                    return gameBoard[i, 0]; // 첫 번째 값 반환
            }
        }

        // 열 체크
        for (int j = 0; j < y; j++)
        {
            if (gameBoard[0, j] != 0) // 열의 첫 번째 값이 0이 아닐 때
            {
                bool same = true;

                for (int i = 1; i < x; i++)
                {
                    if (gameBoard[i, j] != gameBoard[0, j]) // 첫 번째 값과 다르다면
                    {
                        same = false;
                        break;
                    }
                }

                if (same) // 첫 번째 값과 값다면
                    return gameBoard[0, j]; // 첫 번째 값 반환
            }
        }

        // 대각선 (왼쪽 위 -> 오른쪽 아래) 체크
        if (gameBoard[0, 0] != 0) // 왼쪽 위 값이 0이 아닐 때
        {
            bool same = true;

            for (int i = 1; i < x; i++)
            {
                if (gameBoard[i, i] != gameBoard[0, 0]) // 왼쪽 위 값과 다르다면
                {
                    same = false;
                    break;
                }
            }

            if (same) // 왼쪽 위 값과 같다면
                return gameBoard[0, 0]; // 왼쪽 위 값 반환
        }

        // 대각선 (오른쪽 위 -> 왼쪽 아래) 체크
        if (gameBoard[0, y - 1] != 0) // 오른쪽 위 값이 0이 아닐 때
        {
            bool same = true;

            for (int i = 1; i < x; i++)
            {
                if (gameBoard[i, y - i - 1] != gameBoard[0, y - 1]) // 오른쪽 위 값과 다르다면
                {
                    same = false;
                    break;
                }
            }

            if (same) // 오른쪽 위 값과 같다면
                return gameBoard[0, y - 1]; // 오른쪽 위 값 반환
        }

        // 승자 없음
        return 0;
    }

    public bool isTie()
    {
        bool temp = true;
        foreach (GameObject obj in allBoxs)
        {
            TextButtonManager textButtonManager = obj.GetComponent<TextButtonManager>();
            if (textButtonManager != null)
            {
                if (!textButtonManager.isClicked)
                    temp = false;
                    return temp;
            }
        }
            return temp;
    }

    public void Tie()
    {
        tie.SetActive(true);
        Invoke("GoToLobby", 3f);
    }
}
