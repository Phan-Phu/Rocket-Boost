using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishPoint : SingletonMonobehaviour<FinishPoint>, ISaveable
{
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
    }

    private void OnDisable()
    {
        ISaveableDeregister();
    }

    public int GetCurrentIndex()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (currentIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        return currentIndex;
    }
    private static string NameOfSceneByBuildIndex(int buildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    // lay lai data trong file add vai list => xoa data trong file => add gia tri moi trong list => add lai data trong file
    public List<LevelSave> ISaveableSave()  
    {
        Debug.Log("save game");

        int currentIndex = GetCurrentIndex();
        if (currentIndex == SceneManager.sceneCountInBuildSettings)
        {
            currentIndex = 0;
        }
        string nameScene = NameOfSceneByBuildIndex(currentIndex);

        foreach (var item in ListLevelSave)
        {
            if (item.sceneName == nameScene)
            {
                item.sceneActive = true;
            }
        }
        return ListLevelSave;
    }

    public void ISaveableLoad(GameSave gameSave)
    {
        Debug.Log("load game");
        if (gameSave.listLevelData.TryGetValue(ISaveableUniqueID, out List<LevelSave> listLevelSave))
        {
            ListLevelSave = listLevelSave;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
        {
            // get data ListLevelSave from load game
            SaveLoadManager.Instance.LoadDataFromFile();

            SaveLoadManager.Instance.SaveDataToFile();
        }
    }
}
