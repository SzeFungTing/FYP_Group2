using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflationItem : MonoBehaviour
{
    private Item5 item = null;
    private int resetDay = 2;
    private int soldNum = 0;
    private int boughtNum = 0;


    [SerializeField] private List<InflationItem> inflationItemLists;


    public void AddSoldNum(InflationItem inflationItem)
    {
        foreach (InflationItem inflationItemList in inflationItemLists)
        {
            if(inflationItemList.item == inflationItem.item)
            {

            }
        }
    }

    public void AddboughtNum (InflationItem InflationItem)
    {

    }

}
