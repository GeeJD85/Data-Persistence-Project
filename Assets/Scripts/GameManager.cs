using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int HiScore;
    public string HiScoreName;

    public string Name;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();

        if (HiScore == 0)
        {
            GameObject.Find("Hi-Score").SetActive(false);
        }
        else
            GameObject.Find("Hi-Score").GetComponent<TMP_Text>().text = "Current Hi-Score: " + HiScoreName + " - " + HiScore;
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void UpdateName()
    {
        TMP_InputField nameField = GameObject.Find("Input_PlayerName").GetComponent<TMP_InputField>();
        if(!string.IsNullOrEmpty(nameField.text))
            Name = nameField.text;
    }

    public void UpdateHiScore(int score)
    {
        HiScoreName = Name;
        HiScore = score;

        SaveGameData();
    }

    public void Quit()
    {
        SaveGameData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    [System.Serializable]
    class SaveData
    {
        public int HiScore;
        public string HiScoreName;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();
        data.HiScore = HiScore;
        data.HiScoreName = HiScoreName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savedata.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            HiScore = data.HiScore;
            HiScoreName = data.HiScoreName;
        }
    }
}
