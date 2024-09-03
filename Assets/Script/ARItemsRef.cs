using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARItemsRef : MonoBehaviour
{
    [SerializeField] Image sprite;
    [SerializeField] Text objectName;
    [SerializeField] Text price;
    [SerializeField] Text size;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void DisplayData(ItemsSO items)
    {

        //sprite = new Image(items.image);
        objectName.text = items.name;
        price.text = items.price.ToString() + "€";
        size.text = items.size.ToString() + "m";
    }
}
