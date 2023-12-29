using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class EditorSpawnAhri : Editor
{
    [MenuItem("AHRI/Yes/SPAWN %&n")]
    static void CreateAPrefab()
    {
        int maxX = 20;
        int maxY = 25;
        int maxZ = 20;
        float offsetX = 1.5f;
        float offsetY = 1.5f;
        float offsetZ = 1.5f;
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                for (int z = 0; z < maxZ; z++)
                {
                    GameObject prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>("Assets/Prefabs/ahri.prefab"));
                    prefab.name = $"ahri[{x + 1}, {y + 1}, {z + 1}]";

                    prefab.transform.localPosition = new Vector3(x * offsetX, y * offsetY, z * offsetZ);
                    prefab.transform.localEulerAngles = Vector3.zero;
                    prefab.transform.localScale = Vector3.one;

                    Undo.RegisterCreatedObjectUndo(prefab, prefab.name);
                }
            }
        }
    }
}
#endif