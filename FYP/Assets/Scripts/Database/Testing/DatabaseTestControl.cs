using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseTestControl : MonoBehaviour
{
    public SQLiteConnection Connection;

    // Start is called before the first frame update
    void Start()
    {
        Connection = new SQLiteConnection(Application.streamingAssetsPath + "/TestDatabase.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Connection.CreateTable<TestDatabase>();

        //InsertData();

        //var data = Connection.Table<TestDatabase>().Where(_ => _.Name == "John").FirstOrDefault();
        //Connection.Delete(data);
        Connection.Delete<TestDatabase>(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InsertData()
    {
        /*var tDb = new TestDatabase
        {
            Id = 1,
            Name = "Peter",
            Age = 20,
            Height = 180.5f,
            Weight = 140.3f
        };
        Connection.Insert(tDb);*/

        Connection.InsertAll(new[]
        {
            new TestDatabase
            {
                Name = "Mary",
                Age = 12,
                Height = 121.0f,
                Weight = 38.7f
            },

            new TestDatabase
            {
                Name = "John",
                Age = 23,
                Height = 175.3f,
                Weight = 153.2f
            },

            new TestDatabase
            {
                Name = "Conner",
                Age = 999,
                Height = 180.2f,
                Weight = 75.8f
            },
        });
    }
}
