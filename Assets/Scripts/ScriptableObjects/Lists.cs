using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create List")]
public class Lists : ScriptableObject
{
    public List<GameObject> List1 = new List<GameObject>();

    public List<GameObject> GetList()
    {
        return List1;
    }
}
