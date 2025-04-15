using System.Collections.Generic;
using UnityEngine;

//외부 저장할 데이터 모음
[System.Serializable]
public class SaveData
{
    public PlayerData playerdata;
    public PuzzleData puzzleData;

    public SaveData()
    {
        playerdata = new PlayerData();
        puzzleData = new PuzzleData();
    }
}

//외부 저장할 퍼즐 클리어 데이터 클래스
[System.Serializable]
public class PuzzleData
{
    public List<bool> completedPuzzle;
    
    public PuzzleData()
    {
        completedPuzzle = new List<bool>();
    }
    
}

public class GameManager : MonoBehaviour
{
    public List<bool> Puzzle;
    public static GameManager instance;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        Puzzle = new List<bool> { false, false, false, false };
        if(StartButton.instance.dataLoad)
        {
            LoadPlayerData();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SavePlayerData();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //게임 저장
    public void SavePlayerData()
    {
        SaveData saveData = new SaveData();
        saveData.playerdata.playerPosition = CharacterManager.Instance.Player.controller.transform.position;
        saveData.puzzleData.completedPuzzle.AddRange(Puzzle);
        
        //player
        DataManager.Instance.SaveData(saveData);
        Debug.Log("저장 완료!");
    }

    //게임 로드
    public void LoadPlayerData()
    {
        SaveData loadData = DataManager.Instance.LoadData<SaveData>();
        if (loadData != null)
        {
            CharacterManager.Instance.Player.controller.transform.position = loadData.playerdata.playerPosition;
            Puzzle.Clear();
            Puzzle.AddRange(loadData.puzzleData.completedPuzzle);
            for (int i = 0; i < Puzzle.Count; i++)
            {
                if (Puzzle[i])
                {
                    UIManager.Instance.TurnOnText(UIManager.Instance.text[i]);
                }
            }
            //UIManager.Instance.TurnOnText(UIManager.Instance.text[(int)(Puzzle.JumpTower)]);
        }
        else
        {
            Debug.Log("No data found or failed to load.");
        }
    }
}