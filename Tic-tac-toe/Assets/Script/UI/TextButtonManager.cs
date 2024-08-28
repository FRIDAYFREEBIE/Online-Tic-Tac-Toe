using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(TextManager))]
public class TextButtonManager : MonoBehaviourPunCallbacks
{
    [Header("GameBoardContainer")]
    [SerializeField] GameBoardContainer gameBoardContainer;

    public bool isClicked = false;
    private TextManager textManager;
    private GameManager gameManager;

    private int myTurn;

    void Start()
    {
        textManager = GetComponent<TextManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnClick()
    {
        int turn = gameManager.myTurn;

        if (!isClicked  && gameBoardContainer.Turn == turn)
        {
            int x = textManager.x;
            int y = textManager.y;

            if (gameBoardContainer.GameBoard[x, y] == 0)
            {
                photonView.RPC("UpdateGameBoard", RpcTarget.All, x, y, turn);

                isClicked = true;
            }
        }
    }

    // RPC
    [PunRPC]
    void UpdateGameBoard(int x, int y, int turn)
    {
        gameBoardContainer.SetGameBoard(x, y, turn);
        gameBoardContainer.SetTurn();

        gameBoardContainer.Clicks++;

        gameBoardContainer.ShowGameBoard();
    }
}
