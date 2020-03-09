using System.Collections;
using System.Collections.Generic;

public class InventoryItem
{

    public Item item;
    public int amount;

    public InventoryItem(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void incrementAmount(int amount)
    {
        if (this.amount + amount < 0)
        {
            this.amount = 0;
        }
        else
        {
            this.amount += amount;
        }
    }

}
