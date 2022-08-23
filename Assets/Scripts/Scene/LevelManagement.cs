using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagement : SingletonMonobehaviour<LevelManagement>, ISaveable
{
    [SerializeField] private Image hiddenImage;
    [SerializeField] private GameObject[] listScene = null;
    [Tooltip("First level to save game")]
    private string firstLevel;

    private Transform parentTrasform;

    private string _iSaveableUniqueID;
    public string ISaveableUniqueID { get => _iSaveableUniqueID; set => _iSaveableUniqueID = value; }

    private List<LevelSave> _listlevelSave;
    public List<LevelSave> ListLevelSave { get { return _listlevelSave; } set { _listlevelSave = value; } }

    protected override void Awake()
    {
        base.Awake();

        ISaveableUniqueID = nameof(SaveSystem.Level);
        ListLevelSave = new List<LevelSave>();
    }

    private void OnEnable()
    {
        ISaveableRegister();
        AfterSceneLoad();
        EventHandler.SaveGameEvent += SaveGame;
        EventHandler.LoadGameEvent += LoadGame;
    }

    private void OnDisable()
    {
        ISaveableDeregister();
        EventHandler.SaveGameEvent -= SaveGame;
        EventHandler.LoadGameEvent -= LoadGame;
    }

    private void Start()
    {
        firstLevel = listScene[0].name.ToString();
        InitializeLevel();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            print("load game");
            SaveLoadManager.Instance.LoadDataFromFile();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("save game");
            SaveLoadManager.Instance.SaveDataToFile();
            //EventHandler.CallSaveGameEvent();
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            print("New game");
            NewGame();
        }
    }

    private void InitializeLevel()
    {
        ListLevelSave.Clear();

        GameSave gameSave = new GameSave();

        for (int i = 1; i < listScene.Length; i++)
        {
            ListLevelSave.Add(new LevelSave(listScene[i].name.ToString(), false));
        }

        gameSave.listLevelData.Add(ISaveableUniqueID, ListLevelSave);
    }

    private void NewGame()
    {
        ListLevelSave.Clear();
        InitializeLevel();
        EventHandler.CallSaveGameEvent();
        SceneManager.LoadScene("Menu");
    }

    public List<LevelSave> ISaveableSave()
    {
        LevelSave levelSave = new LevelSave();

        levelSave.sceneName = firstLevel;
        levelSave.sceneActive = true;

        ListLevelSave.Add(levelSave);

        return ListLevelSave;
    }

    public void ISaveableLoad(GameSave gameSave)
    {
        if (gameSave.listLevelData.TryGetValue(ISaveableUniqueID, out List<LevelSave> listLevelSave))
        {
            ListLevelSave = listLevelSave;

            foreach (LevelSave level in listLevelSave)
            {
                foreach (GameObject scene in listScene)
                {
                    if (level.sceneActive == false && level.sceneName == scene.name.ToString())
                    {
                        if (FindObjectOfType<LoadSceneMenu>().blockLevel == false)
                            Instantiate(hiddenImage, scene.transform.position, Quaternion.identity, parentTrasform);
                    }
                }
            }
        }
    }

    public void ISaveableRegister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Add(this);
    }

    public void ISaveableDeregister()
    {
        SaveLoadManager.Instance.iSaveableObjectList.Remove(this);
    }

    private void AfterSceneLoad()
    {
        parentTrasform = GameObject.FindGameObjectWithTag(Tags.Hidden).transform;
    }

    private void SaveGame()
    {
        SaveLoadManager.Instance.SaveDataToFile();
    }

    private void LoadGame()
    {
        SaveLoadManager.Instance.LoadDataFromFile();
    }
}
