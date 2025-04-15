using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("DataManager").AddComponent<DataManager>();
            }
            return _instance;
        }
    }
    private string savePath;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        savePath = Application.persistentDataPath;
    }

    // 저장경로 컴퓨터마다 다름
    // C:/Users/Pc/AppData/LocalLow/DefalutCompany/example
    public void SaveData<T>(T data)
    {
        var saveData = JsonUtility.ToJson(data);
        File.WriteAllText(savePath + $"/{typeof(T).ToString()}.txt", saveData);
        //Debug.Log(savePath);
    }

    public T LoadData<T>()
    {
        Debug.Log(savePath);
        var loadData = File.ReadAllText(savePath + $"/{typeof(T).ToString()}.txt");
        return JsonUtility.FromJson<T>(loadData);
    }
}