using System.Collections.Generic;

[System.Serializable]
public class LevelSave
{
    public string sceneName;
    public bool sceneActive;

    public LevelSave()
    {
        sceneName = "";
        sceneActive = false;
    }

    public LevelSave(string sceneName, bool sceneActive)
    {
        this.sceneName = sceneName;
        this.sceneActive = sceneActive;
    }
}
