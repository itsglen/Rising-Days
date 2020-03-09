using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * An item stack is how items are represented in the game world (not ui world)
 * A stack can be 1 or more items
 */
public class ItemStack : MonoBehaviour
{

    public Item item;
    public int stackSize;

    public ItemStack(Item item)
    {
        this.item = item;
        this.stackSize = 1;
    }

    public ItemStack(Item item, int stackSize)
    {
        this.item = item;
        this.stackSize = stackSize;
    }

}
