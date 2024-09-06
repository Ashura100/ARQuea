using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;

public class ARItemsRef : MonoBehaviour
{
    [SerializeField] ItemsSO items;
    [SerializeField] Image sprite;
    [SerializeField] Text objectName;
    [SerializeField] Text price;
    [SerializeField] Text size;

    Quaternion originalRotation;
    public bool isShowing = false;
    public bool isPlaced = true;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        SetupData();
        PrefabManager.Instance.Hide();
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation * originalRotation;

    }

    public void SetupData()
    {

        sprite.sprite = items.image;
        objectName.text = items.name;
        price.text = items.price.ToString() + "€";
        size.text = items.size.ToString() + "m";
    }

    public void HandleFingerTap(LeanFinger finger)
    {
        Debug.Log("Touch");

        if (isShowing)
        {
            PrefabManager.Instance.Hide();
            isShowing = false;
        }
        else if(isPlaced)
        {
            PrefabManager.Instance.Show();
            isShowing = true;
        }
    }
}
