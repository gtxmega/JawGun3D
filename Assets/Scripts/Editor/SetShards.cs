using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using GameCore;

public class SetShards : EditorWindow
{

    [MenuItem("Custom/AddShardToPlaque")]
    public static void Shards()
    {
        List<Transform> childrenTransforms = new List<Transform>();

        childrenTransforms.AddRange(Selection.activeGameObject.GetComponentsInChildren<Transform>());
        childrenTransforms.RemoveAt(0);

        foreach(var iter in childrenTransforms)
        {
            iter.gameObject.AddComponent<Rigidbody>().isKinematic = true;
        }

        Rigidbody[] rigidbodies = Selection.activeGameObject.GetComponentsInChildren<Rigidbody>();
        Selection.activeGameObject.GetComponent<HardPlaque>().SetShards(rigidbodies);

        foreach(var iter in childrenTransforms)
        {
            iter.gameObject.SetActive(false);
        }
    }
}
