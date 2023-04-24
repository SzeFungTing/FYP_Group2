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
    }

    public Item5 GetItem(int id)
    {
        Item5 temp = null;
        if (itemDict.TryGetValue(id, out temp))
        {
            //success!
            return temp;
        }
        return temp;
    }
}
