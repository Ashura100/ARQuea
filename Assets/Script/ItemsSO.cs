using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ItemsSo")]
public class ItemsSO : ScriptableObject
{
    public Sprite image;
    public string objectName;
    public string description;
    public GameObject gameOject;
    public float price;
    public Vector3 size;
}
