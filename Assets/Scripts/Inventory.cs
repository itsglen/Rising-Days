using System.Collections;
using System.Collections.Generic;
public class Inventory
{

    public List<InventoryItem> inventory = new List<InventoryItem>();

    // Function that consumes an inventory stack and either creates, updates, or removes an item depending on the incremention argument
    public void addRemoveOrUpdateItem(ItemStack itemStack)
    {
        // Check if inventory already contains item and increment value if so
        bool containsItem = false;
        foreach (InventoryItem inventoryItem in inventory)
        {
            if (inventoryItem.item.id == itemStack.item.id)
            {
                containsItem = true;
            }
        }

        if (!containsItem)
        {
            this.inventory.Add(new InventoryItem(itemStack.item, itemStack.stackSize));
        }
    }

}
