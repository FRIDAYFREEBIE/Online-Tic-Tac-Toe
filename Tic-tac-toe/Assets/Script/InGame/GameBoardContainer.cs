using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName = "GameBoardContainer", menuName = "GameBoardContainer")]
public class GameBoardContainer : ScriptableObject, IPunObservable
{
    private int[,] gameBoard = new int[3,3]; // 초기화: 0, X: -1, O: 1
    private int turn = -1; // 초기화: -1, X: -1, O: 1
    public int clicks;

    public int[,] GameBoard => gameBoard;

    public int Turn => turn;

    public int Clicks
    {
        get{return clicks;}
        set{clicks = value;}
    }

    public void SetGameBoard(int x, int y, int data)
    {
        gameBoard[x, y] = data;
    }

    public void SetTurn()
    {
        turn = (turn == -1) ? 1 : -1;
    }

    public void ShowGameBoard()
    {
        string newText = "\n";

        for (int y = 0; y < gameBoard.GetLength(1); y++)
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                newText += gameBoard[x, y] + " ";
            }
            newText += "\n";     
        }

        Debug.Log(newText);
    }

    public void ShowTurn()
    {
        Debug.Log(turn);
    }

    public void ResetContainer()
    {
        for (int y = 0; y < gameBoard.GetLength(1); y++)
        {
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
                gameBoard[x, y] = 0;
            }
        }

        turn = -1;

        clicks = 0;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 네트워크에 보내기
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    stream.SendNext(gameBoard[i, j]); // 게임 보드
                }
            }
            stream.SendNext(turn); // 턴
        }
        else
        {
            // 네트워크에서 받기
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameBoard[i, j] = (int)stream.ReceiveNext(); // 게임 보드
                }
            }
            turn = (int)stream.ReceiveNext(); // 턴
        }
    }
}
