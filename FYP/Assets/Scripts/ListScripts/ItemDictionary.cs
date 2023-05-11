using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ItemDictionary : MonoBehaviour
{
    Dictionary<int, Item5> itemDict = new Dictionary<int, Item5>();

    public static ItemDictionary instance;

    [SerializeField]
    private Item5[] itemArray;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var i in itemArray)
        {
            itemDict.Add(i.id, i);
        }

        //Debug.Log("itemDict.Count: "+itemDict.Count);
        //Debug.Log( GetItem(16));
    }

    public Item5 GetItem(int id)
    {
        //Debug.Log("id: " + id);
        Item5 temp = null;
        //if(id==16)
        //{
        //    foreach (KeyValuePair<int, Item5> element in itemDict)
        //    {
        //        Debug.Log(("param id16, " + element.Key));
        //        if(element.Key == 16)
        //        {
        //            Debug.Log("should be return item 16");
        //        }
        //    }
        //}
        if (itemDict.TryGetValue(id, out temp))
        {
            //success!
            return temp;
        }
        //Debug.Log("itemArray count: "+ itemArray.Length);
        //Debug.Log("itemDict count: " + itemDict.Count);
        //Debug.Log("failed");
        return temp;
    }
}
