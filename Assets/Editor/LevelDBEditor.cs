using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelDB))]
public class LevelDBEditor : Editor
{
    private LevelDB levelDB;

    private void Awake()
    {
        levelDB = (LevelDB)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("New Level"))
        {
            levelDB.CreateLevel();
        }
        if(GUILayout.Button("Remove Level"))
        {
            levelDB.RemoveLevel();
        }
        if (GUILayout.Button("<="))
        {
            levelDB.PrevLevel();
        }
        if (GUILayout.Button("=>"))
        {
            levelDB.NextLevel();
        }
        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
