using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistanceDataSave
{
    void SaveData(GameData data);
    void LoadData(GameData data);
}
