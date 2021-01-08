using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CharacterListOperations : MonoBehaviour
{
    public string characterInfo = "";
    List<string> characterList = new List<string>();

    const int characterSlotLimit = 2;
    public int characterSlot = 0;
    public int numberOfIncomingInfo = 0;

    public Text c_Name;
    public Text c_Level;
    public Text c_Gender;
    public Text c_Class;
    public Button startCreateButton;

    void Start()
    {
        StartCoroutine(CharacterList());
    }

    void CharacterSelect()
    {
        Debug.Log(characterInfo);

        if (numberOfIncomingInfo / 4 >= characterSlot + 1)
        {
            c_Name.text = characterList[(characterSlot) * 4];
            c_Level.text = "Level: " + characterList[(characterSlot) * 4 + 1];
            c_Gender.text = characterList[(characterSlot) * 4 + 2];
            c_Class.text = characterList[(characterSlot) * 4 + 3];
            startCreateButton.transform.GetChild(0).GetComponent<Text>().text = "Start";
        }
        else
        {
            c_Name.text = "Name: ?";
            c_Level.text = "Level: 1";
            c_Gender.text = "Gender: ?";
            c_Class.text = "Class: ?";
            startCreateButton.transform.GetChild(0).GetComponent<Text>().text = "Create";
        }
    }

    IEnumerator CharacterList()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("server_name", PlayerPrefs.GetString("server_name")));
        formData.Add(new MultipartFormDataSection("username", PlayerPrefs.GetString("username")));
        formData.Add(new MultipartFormDataSection("password", PlayerPrefs.GetString("password")));

        UnityWebRequest www = UnityWebRequest.Post("https://cky-rpg-game.000webhostapp.com/characterlist.php", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            //Sunucudan yanıt alınamadı.
            Debug.Log(www.error);
        }
        else
        {
            characterInfo = www.downloadHandler.text;
            numberOfIncomingInfo = characterInfo.Split('*').Length;

            for (int i = 0; i < numberOfIncomingInfo; i++)
            {
                characterList.Add(characterInfo.Split('*')[i]);
            }

            CharacterSelect();
        }
    }

    public void RightButton()
    {
        if (characterSlot < characterSlotLimit)
        {
            characterSlot++;
            CharacterSelect();
        }
    }

    public void LeftButton()
    {
        if (characterSlot > 0)
        {
            characterSlot--;
            CharacterSelect();
        }
    }
}
