using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum NamaPosisi
{
    None, RoomInside, RoomEntrance, LevelEntrance, LevelInside, LevelFirstAid, LevelFinish
}
[Serializable]
public class PlayerPositionSave
{
    public LevelMode levelMode;
    [Serializable]
    public class PositionsInLevelMode
    {
        public LevelP3KType levelP3KType;
        [Serializable]
        public class position{
            public NamaPosisi namaPosisi;
            public Vector3 playerPosition;
            public float playerRotation;

        }
        public position[] positions;
    }
    public PositionsInLevelMode[] positionsInLevel;
}
public class SOPlayerPosition : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("SO/PlayerPosition")]
    public static void QuickCreate()
    {
        SOPlayerPosition asset = CreateInstance<SOPlayerPosition>();
        string name =
            AssetDatabase.GenerateUniqueAssetPath("Assets/ScriptableObjects//PlayerPosition.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
#endif


    [SerializeField]public PlayerPositionSave[] playerPosition;

    public PlayerPositionSave.PositionsInLevelMode.position PlayerPositionSearch(LevelMode levelModeNow, LevelP3KType levelP3KTypeNow, LevelMode levelModeLast, InGame_Mode inGame_ModeNow, NamaPosisi specificPosition)
    {
        PlayerPositionSave.PositionsInLevelMode.position playerPos = new PlayerPositionSave.PositionsInLevelMode.position();
        playerPos = null;
        for(int i=0;i<playerPosition.Length;i++)
        {
            if(playerPosition[i].levelMode == levelModeNow)
            {
                for(int j=0;j<playerPosition[i].positionsInLevel.Length;j++)
                {
                    if(levelModeNow == LevelMode.Home)
                    {
                        for(int k=0;k<playerPosition[i].positionsInLevel[j].positions.Length;k++)
                        {
                            if(levelModeLast == LevelMode.Home)
                            {
                                if(playerPosition[i].positionsInLevel[j].positions[k].namaPosisi == NamaPosisi.RoomInside)
                                {
                                    playerPos = playerPosition[i].positionsInLevel[j].positions[k];
                                    break;
                                }
                            }
                            else
                            {
                                if(playerPosition[i].positionsInLevel[j].positions[k].namaPosisi == NamaPosisi.RoomEntrance)
                                {
                                    playerPos = playerPosition[i].positionsInLevel[j].positions[k];
                                    break;
                                }
                            }
                            
                        }
                    }
                    else
                    {
                        if(playerPosition[i].positionsInLevel[j].levelP3KType == levelP3KTypeNow)
                        {
                            for(int k=0;k<playerPosition[i].positionsInLevel[j].positions.Length;k++)
                            {
                                if(levelModeLast == LevelMode.Home)
                                {
                                    if(playerPosition[i].positionsInLevel[j].positions[k].namaPosisi == NamaPosisi.LevelEntrance)
                                    {
                                        playerPos = playerPosition[i].positionsInLevel[j].positions[k];
                                        break;
                                    }
                                }
                                else
                                {
                                    if(inGame_ModeNow == InGame_Mode.NormalWalk)
                                    {
                                        if(specificPosition == NamaPosisi.LevelFinish)
                                        {
                                            if(playerPosition[i].positionsInLevel[j].positions[k].namaPosisi == NamaPosisi.LevelFinish)
                                            {
                                                playerPos = playerPosition[i].positionsInLevel[j].positions[k];
                                                break;
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                        if(playerPosition[i].positionsInLevel[j].positions[k].namaPosisi == NamaPosisi.LevelInside)
                                        {
                                            playerPos = playerPosition[i].positionsInLevel[j].positions[k];
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if(playerPosition[i].positionsInLevel[j].positions[k].namaPosisi == NamaPosisi.LevelFirstAid)
                                        {
                                            playerPos = playerPosition[i].positionsInLevel[j].positions[k];
                                            break;
                                        }
                                    }
                                }
                                
                            }
                        }
                    }

                    if(playerPos != null)
                    {
                        break;
                    }
                }
            }

            if(playerPos != null)
            {
                break;
            }
        }


        return playerPos;
    }
}

