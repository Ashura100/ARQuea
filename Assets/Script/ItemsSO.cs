using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemsSo")]
public class ItemsSO : ScriptableObject
{
    public string objectName;
    public string description;
    public GameObject gameOject;
    public float price;
    public Vector3 size;
}
