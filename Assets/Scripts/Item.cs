using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public GameObject item;
    public int id;
    public string name;

    public Item(GameObject item, int id, string name)
    {
        this.item = item;
        this.id = id;
        this.name = name;
    }

}
