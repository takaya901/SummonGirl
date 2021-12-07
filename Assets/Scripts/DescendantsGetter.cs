using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class DecendantsGetter
{
    public static List<GameObject> GetDecendants(this GameObject root)
    {
        var decendants = new List<GameObject>();
        return Get(root, decendants);
    }

    //子要素を取得してリストに追加
    static List<GameObject> Get(GameObject parent, List<GameObject> decendants)
    {
        var children = parent.GetComponentInChildren<Transform>();
        
        //子要素がいなければ終了
        if (children.childCount == 0) {
            return decendants;
        }
        
        foreach (Transform child in children) {
            decendants.Add(child.gameObject);
            Get(child.gameObject, decendants);
        }

        return decendants;
    }
}