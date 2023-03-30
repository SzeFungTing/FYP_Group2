using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class PlayerTableControl : MonoBehaviour
{
    public SQLiteConnection Connection;

    // Start is called before the first frame update
    void Start()
    {
        Connection = new SQLiteConnection(Application.streamingAssetsPath + "/PlayerTable.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Connection.CreateTable<PlayerTable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InsertData(GameObject player)
    {
        var pt = new PlayerTable
        {
            PosX = player.transform.position.x,
            PosY = player.transform.position.y,
            PosZ = player.transform.position.z,
            //Coin = ,
            //HvJetpack = 
        };
        Connection.Insert(pt);
    }
}
