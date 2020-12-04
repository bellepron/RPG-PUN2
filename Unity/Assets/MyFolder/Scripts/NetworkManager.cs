using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks, IConnectionCallbacks, ILobbyCallbacks, IMatchmakingCallbacks
{
    void IConnectionCallbacks.OnConnectedToMaster() // Servera bağlanınca
    {
        PhotonNetwork.JoinLobby();
    }

    void ILobbyCallbacks.OnJoinedLobby() // Lobiye girince
    {
        Debug.Log("Lobby created.");

        Transform panelMember = GameObject.Find("Server Panel").transform;
        for (int i = 0; i < panelMember.childCount; i++)
        {
            Button b = panelMember.GetChild(i).GetComponent<Button>();
            if (b)
                b.interactable = true;
        }
    }

    void IMatchmakingCallbacks.OnCreatedRoom() // Oda oluşunca
    {
        Debug.Log("Room created." + PlayerPrefs.GetString("server_name"));
    }
}