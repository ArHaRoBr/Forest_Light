using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one instance of Inventory found");
                return;
        }
        instance = this;
    }

    #endregion

    public bool isHide = false;
    //public GameObject inventory;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback; 

    public int space = 16;

    public List<Item> items = new List<Item> ();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if(items.Count >= space)
            {
                Debug.Log("Not enough space");
                return false;
            }

            items.Add(item);

            if(onItemChangedCallback != null)   
                onItemChangedCallback.Invoke();
        }

        return true;
    } 

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

    }

/*    public void ShowOrHide()
    {
        inventory.SetActive(isHide);
        if (isHide) isHide = false;
        else isHide = true;
    }
*/
}
