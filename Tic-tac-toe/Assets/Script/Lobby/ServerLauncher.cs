using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ServerLauncher : MonoBehaviourPunCallbacks 
{
    [Header("Button")]
    [SerializeField] private GameObject startButton; // 시작 버튼

    [Header("Connect")]
    [SerializeField] private Image isConnect; // 연결 상태 표시 이미지

    [Header("isConnected")]
    public bool isConnected;

    void Start()
    {
        isConnected = false;

        startButton.SetActive(isConnected);
        isConnect.color = Color.red;

        // 서버에 연결
        PhotonNetwork.ConnectUsingSettings();
    }

    // 서버 연결 시 호출
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 연결");

        isConnected = true;

        isConnect.color = Color.green;

        // 로비 입장
        PhotonNetwork.JoinLobby();
    }

    // 로비 입장 시 호출
    public override void OnJoinedLobby()
    {
        Debug.Log("로비 입장");

        startButton.SetActive(isConnected);
    }

    // 방 생성 버튼 클릭 시 호출
    public void OnStartButtonClicked()
    {
        // 방 입장
        PhotonNetwork.JoinRandomRoom();
    }

    // 방 입장 실패 시 호출
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogWarning("방 입장 실패: " + message);

        // 방 옵션 설정
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2; // 최대 2명

        // 방 이름
        string roomName = "Room_" + Random.Range(1000, 9999);

        // 방 생성
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    // 방 생성 성공 시 호출
    public override void OnCreatedRoom()
    {
        Debug.Log("방 생성 성공: " + PhotonNetwork.CurrentRoom.Name);
    }

    // 방 생성 실패 시 호출
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("방 생성 실패: " + message);
    }

    // 방 입장 시 호출
    public override void OnJoinedRoom()
    {
        Debug.Log("입장 " + PhotonNetwork.CurrentRoom.Name);

        // 씬 로드
        PhotonNetwork.LoadLevel("InGame");
    }
}
