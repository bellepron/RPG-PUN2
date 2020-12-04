using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.Networking;
using System.Linq; // for (an array).ToList();

public class ServerSelectionSystem : MonoBehaviour
{
    public GraphicRaycaster graphicRaycaster;
    public PointerEventData pointerEventData;
    public EventSystem eventSystem;

    public Transform serverPanel;
    public string servers = "";
    public List<string> serverList = new List<string>();

    void Start()
    {
        WriteServerList();
    }

    void Update()
    {
        GetServerName();
    }

    void GetServerName()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<Button>().interactable)
                {
                    PlayerPrefs.SetString("server_name", result.gameObject.transform.GetChild(0).GetComponent<Text>().text); // Server adi saklama.
                    PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("server_name"), new RoomOptions { MaxPlayers = 10 }, TypedLobby.Default);
                    CharacterSelect();
                }
            }
        }
    }

    void CharacterSelect()
    {
        SceneManager.LoadScene("CharacterList", LoadSceneMode.Single);
    }
    void WriteServerList()
    {
        StartCoroutine(ServerList());
    }

    IEnumerator ServerList()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("serverListele", "serverListele"));

        UnityWebRequest www = UnityWebRequest.Post("https://cky-rpg-game.000webhostapp.com/serverlist.php", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Sunucudan yanıt alınamadı.
            Debug.Log(www.error);
        }
        else
        {
            servers = www.downloadHandler.text;
            
            // serverList.Add(servers.Split('*')[0]); // for noobs
            // serverList.Add(servers.Split('*')[1]);
            serverList = servers.Split('*').ToList(); // for SOLID

            for (int i = 0; i < serverList.Count; i++)
            {
                GameObject button = (GameObject)Instantiate(Resources.Load("Server"));
                button.name = "Server " + (i + 1);
                button.transform.SetParent(serverPanel.transform);
                button.transform.GetChild(0).GetComponent<Text>().text = serverList[i];
            }

        }
    }
}
