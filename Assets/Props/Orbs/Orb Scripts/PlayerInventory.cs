using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Responsible for player inventory and adding and removing orbs
    public List<string> inventory;
    // Start is called before the first frame update
    private void Start()
    {
        inventory = new List<string>();
    }
    public void AddItem(string orb) 
    {
        inventory.Add(orb);
    }
    public int GetInventoryCount() 
    {
        return inventory.Count;
    }
    public void RemoveOrb(string orb) 
    {
        inventory.Remove(orb);
    }
}
