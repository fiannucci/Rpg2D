using UnityEngine;
using UnityEditor;
using System.Collections;

public class EntityAsstCreator : Editor
{

    [MenuItem("Assets/Create/Entity Player")]
    public static void CreatePlayerAsset()
    {
        CustomAssetUtility.CreateAsset<Player>();
    }


}
