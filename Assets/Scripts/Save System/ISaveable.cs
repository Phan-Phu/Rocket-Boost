using System.Collections.Generic;

public interface ISaveable
{
    string ISaveableUniqueID { get; set; }

    List<LevelSave> ListLevelSave { get; set; }

    void ISaveableRegister();

    void ISaveableDeregister();

    List<LevelSave> ISaveableSave();

    void ISaveableLoad(GameSave gameSave);
}
