using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks, IConnectionCallbacks, ILobbyCallbacks, IMatchmakingCallbacks
{
    // void IConnectionCallbacks.OnConnectedToMaster()
    // {
    //     PhotonNetwork.JoinLobby();
    // }

    // void ILobbyCallbacks.OnJoinedLobby()
    // {
    //     Debug.Log("Lobby created.");

    //     Transform panelMember = GameObject.Find("Server Panel").transform;
    //     for (int i = 0; i < panelMember.childCount; i++)
    //     {
    //         Button b = panelMember.GetChild(i).GetComponent<Button>();
    //         if (b)
    //             b.interactable = true;
    //     }
    // }

    // void IMatchmakingCallbacks.OnCreatedRoom()
    // {
    //     Debug.Log("Room created." + PlayerPrefs.GetString("server_name"));
    // }



    //GECICI
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 20, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        GameObject new_player = PhotonNetwork.Instantiate("Paladin", new Vector3(122, 151, 141), Quaternion.identity, 0);
        new_player.GetComponent<PhotonView>().Owner.NickName = Random.Range(1, 100) + "(player_";
    }
}