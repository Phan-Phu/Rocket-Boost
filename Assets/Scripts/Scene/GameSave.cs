using System.Collections.Generic;

[System.Serializable]
public class GameSave
{
    public Dictionary<string, List<LevelSave>> listLevelData;

    public GameSave()
    {
        listLevelData = new Dictionary<string, List<LevelSave>>();
    }

    public GameSave(Dictionary<string, List<LevelSave>> listLevelData)
    {
        this.listLevelData = listLevelData;
    }
}
