using System.Collections;
using System.Collections.Generic;
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

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
        SetupData();
        //Hide();
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

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
