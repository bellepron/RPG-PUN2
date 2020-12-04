using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public InputField usernameIF;
    public InputField passwordIF;
    public Text loginInfo;
    public string username;
    public string password;

    void Start()
    {

    }

    void Update()
    {

    }

    public void LoginButton()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username", usernameIF.text));
        formData.Add(new MultipartFormDataSection("password", passwordIF.text));

        UnityWebRequest www = UnityWebRequest.Post("https://cky-rpg-game.000webhostapp.com/login.php", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Sunucudan yanıt alınamadı.
            Debug.Log(www.error);
        }
        else
        {
            //Sunucudan yanıt alındı.
            if(www.downloadHandler.text=="Login Successful!")
            {
                loginInfo.text="Login Successful!";
                PlayerPrefs.SetString("username",usernameIF.text); // kullanıcı adi saklama
                PlayerPrefs.SetString("password",passwordIF.text);  // şifre saklama
                PhotonNetwork.ConnectUsingSettings();
                SceneManager.LoadScene("ServerList");
            }
            else
            {
                loginInfo.text="Login Failed!";
            }
        }
    }
}
