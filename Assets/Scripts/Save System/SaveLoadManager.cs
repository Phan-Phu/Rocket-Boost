// game duoc luu duoi dang json , sau nay day len server thi phai ma hoa file


using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : SingletonMonobehaviour<SaveLoadManager>
{

    public GameSave gameSave;
    public List<ISaveable> iSaveableObjectList;
    private string jsonFile;
    private string path;

    protected override void Awake()
    {
        base.Awake();

        iSaveableObjectList = new List<ISaveable>();
    }

    public void LoadDataFromFile()
    {
        if (File.Exists(Application.streamingAssetsPath + "/Level.json"))
        {
            gameSave = new GameSave();

            path = Application.streamingAssetsPath + "/Level.json";

            string jsonString = File.ReadAllText(path);

            ListLevelSave listLevelSave = JsonUtility.FromJson<ListLevelSave>(jsonString);

            gameSave.listLevelData.Add(SaveSystem.Level.ToString(), listLevelSave.listLevelSave);

            // loop through all ISaveable objects and apply save data
            for (int i = iSaveableObjectList.Count - 1; i > -1; i--)
            {
                if (gameSave.listLevelData.ContainsKey(iSaveableObjectList[i].ISaveableUniqueID))
                {
                    iSaveableObjectList[i].ISaveableLoad(gameSave);
                }
                else
                {
                    Component component = (Component)iSaveableObjectList[i];
                    Destroy(component.gameObject);
                }
            }
        }
    }

    public void SaveDataToFile()
    {
        gameSave = new GameSave();

        // loop through all ISaveable objects and generate save data
        foreach (ISaveable iSaveableObject in iSaveableObjectList)
        {
            gameSave.listLevelData.Add(iSaveableObject.ISaveableUniqueID, iSaveableObject.ISaveableSave());
        }

        path = Application.streamingAssetsPath + "/Level.json";

        gameSave.listLevelData.TryGetValue(SaveSystem.Level.ToString(), out List<LevelSave> listLevel);

        var saveLevel = new ListLevelSave() { listLevelSave = listLevel };

        jsonFile = JsonUtility.ToJson(saveLevel);

        File.WriteAllText(path, jsonFile);
    }
}

[System.Serializable]
class ListLevelSave
{
    public List<LevelSave> listLevelSave;
}
