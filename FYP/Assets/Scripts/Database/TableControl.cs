using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using UnityEngine.SceneManagement;

public class TableControl : MonoBehaviour
{
    public SQLiteConnection PlayerConnection;
    public SQLiteConnection BackpackConnection;
    public SQLiteConnection AnimoConnection;
    public SQLiteConnection BuildingConnection;
    public SQLiteConnection PuzzleConnection;
    public SQLiteConnection FajroConnection;
    public SQLiteConnection MarketConnection;

    public GameObject player;
    private int count = 0;
    private int currentMap = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerConnection = new SQLiteConnection(Application.streamingAssetsPath + "/PlayerTable.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        PlayerConnection.CreateTable<PlayerTable>();

        BackpackConnection = new SQLiteConnection(Application.streamingAssetsPath + "/BackpackTable.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        BackpackConnection.CreateTable<BackpackTable>();

        AnimoConnection = new SQLiteConnection(Application.streamingAssetsPath + "/AnimoTable.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        AnimoConnection.CreateTable<AnimoTable>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);

        BuildingConnection = new SQLiteConnection(Application.streamingAssetsPath + "/BuildingTable.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        BuildingConnection.CreateTable<BuildingTable>(CreateFlags.ImplicitPK | CreateFlags.AutoIncPK);

        PuzzleConnection = new SQLiteConnection(Application.streamingAssetsPath + "/PuzzleTable.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        PuzzleConnection.CreateTable<PuzzleTable>();

        FajroConnection = new SQLiteConnection(Application.streamingAssetsPath + "/FajroTable.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        FajroConnection.CreateTable<FajroTable>();

        MarketConnection = new SQLiteConnection(Application.streamingAssetsPath + "/MarketTable.db", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        MarketConnection.CreateTable<MarketTable>();

        switch (SceneManager.GetActiveScene().name)
        {
            case "HomeScene":
                currentMap = 1;
                break;

            case "IslandScene":
                currentMap = 2;
                break;

            case "DesertScene":
                currentMap = 3;
                break;

            default:
                currentMap = 0;
                break;
        }

        TestInsertAnimo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))    //save
        {
            PlayerConnection.DeleteAll<PlayerTable>();
            InsertPlayerData(player);

            BackpackConnection.DeleteAll<BackpackTable>();
            InsertAllBackpackData();

            AnimoConnection.DeleteAll<AnimoTable>();
            Animo[] animos = FindObjectsOfType<Animo>();
            GameObject[] animoObjs = new GameObject[animos.Length];
            for (int i = 0; i < animos.Length; i++)
            {
                if (animos[i].gameObject.tag == "Pet")
                animoObjs[i] = animos[i].gameObject;
            }
            InsertAllAnimoData(animoObjs);

            BuildingConnection.DeleteAll<BuildingTable>();
            PlaceableObject[] buildings = FindObjectsOfType<PlaceableObject>();
            GameObject[] buildingObjs = new GameObject[buildings.Length];
            for (int i = 0; i < buildings.Length; i++)
            {
                buildingObjs[i] = buildings[i].gameObject;
            }
            InsertAllBuildingData(buildingObjs);
        }

        if (Input.GetKeyDown(KeyCode.X))    //load
        {
            var playerData = GetPlayerData();
            foreach (var p in playerData)
            {
                player.transform.position = new Vector3(p.PosX, p.PosY, p.PosZ);
                MoneyManager.instance.coins = (float)p.Coin;
                MoneyManager.instance.RefreshCoins();
                ShopManager.instance.haveJetpack = p.HvJetpack;
            }

            var backpackData = GetBackpackData();
            foreach(var b in backpackData)
            {
                //Debug.Log("backpack: " + ItemDictionary.instance.GetItem(b.ItemId));
                InventoryManager5.instance.SpawnNewItem(ItemDictionary.instance.GetItem(b.ItemId), InventoryManager5.instance.inventorySlots[b.PosId], b.Count);
            }

            Animo[] animos = FindObjectsOfType<Animo>();
            for (int i = 0; i < animos.Length; i++)
            {
                Destroy(animos[i].gameObject);
            }
            var animoData = GetAnimoData();
            foreach (var a in animoData)
            {
                //Debug.Log("animo: " + ItemDictionary.instance.GetItem(a.Id).GetItemPrefab());
                Instantiate(ItemDictionary.instance.GetItem(a.AnimoId).objectPrefab, new Vector3(a.PosX, a.PosY, a.PosZ), Quaternion.identity);
            }

            var buildingData = GetBuildingData();
            foreach (var b in buildingData)
            {
                Instantiate(ItemDictionary.instance.GetItem(b.BuildingId).objectPrefab, new Vector3(b.PosX, b.PosY, b.PosZ), new Quaternion(0, b.Rotation, 0, 0));
            }
        }
    }


    public void InsertPlayerData(GameObject player)
    {
        int mapId = 0;
        switch (SceneManager.GetActiveScene().name)
        {
            case "HomeScene":
                mapId = 1;
                break;

            case "IslandScene":
                mapId = 2;
                break;

            case "DesertScene":
                mapId = 3;
                break;

            default:
                mapId = 0;
                break;
        }

        var pt = new PlayerTable
        {
            PosX = player.transform.position.x,
            PosY = player.transform.position.y,
            PosZ = player.transform.position.z,
            Coin = (int)MoneyManager.instance.coins,
            HvJetpack = ShopManager.instance.haveJetpack,
            MapId = mapId
        };
        PlayerConnection.Insert(pt);
    }

    public void InsertAllBackpackData()
    {
        InventorySlot5[] is5 = InventoryManager5.instance.inventorySlots;

        for (int i = 0; i < is5.Length; i++)
        {
            if (is5[i].gameObject.transform.childCount == 1)
            {
                InventoryItem item = is5[i].GetComponentInChildren<InventoryItem>();

                Debug.Log("is5[i].GetSelectedItem()" + is5[i].GetSelectedItem());

                var bt = new BackpackTable
                {
                    ItemId = item.item.id,
                    Count = item.count,
                    PosId = i
                };
                BackpackConnection.Insert(bt);
            }
        }

    }

    public void InsertAllAnimoData(GameObject[] animos)
    {
        int mapId = 0;
        switch (SceneManager.GetActiveScene().name)
        {
            case "HomeScene":
                mapId = 1;
                break;

            case "IslandScene":
                mapId = 2;
                break;

            case "DesertScene":
                mapId = 3;
                break;

            default:
                mapId = 0;
                break;
        }

        for (int i = 0; i < animos.Length; i++)
        {
            var at = new AnimoTable
            {
                AnimoId = animos[i].GetComponent<WorldItem>().GetItemId(),
                PosX = animos[i].transform.position.x,
                PosY = animos[i].transform.position.y,
                PosZ = animos[i].transform.position.z,
                IsHungry = animos[i].GetComponent<EatFood>().GetIsHungey(),
                MapId = mapId
            };
            AnimoConnection.Insert(at);
        }
    }

    public void InsertAllBuildingData(GameObject[] buildings)
    {
        int mapId = 0;
        switch (SceneManager.GetActiveScene().name)
        {
            case "HomeScene":
                mapId = 1;
                break;

            case "IslandScene":
                mapId = 2;
                break;

            case "DesertScene":
                mapId = 3;
                break;

            default:
                mapId = 0;
                break;
        }

        for (int i = 0; i < buildings.Length; i++)
        {
            var bt = new BuildingTable
            {
                BuildingId = buildings[i].GetComponent<WorldItem>().GetItemId(),
                PosX = buildings[i].transform.position.x,
                PosY = buildings[i].transform.position.y,
                PosZ = buildings[i].transform.position.z,
                Rotation = buildings[i].transform.rotation.y,
                MapId = mapId
            };
            AnimoConnection.Insert(bt);
        }
    }

    public void InsertPuzzleData()
    {
        var pt = new PuzzleTable
        {
            //Id = ,
            //IsClear =
        };
        AnimoConnection.Insert(pt);
    }

    public void InsertAllFajroData(GameObject[] fajros)
    {
        for (int i = 0; i < fajros.Length; i++)
        {
            var ft = new FajroTable
            {
                Id = fajros[i].GetComponent<WorldItem>().GetItemId(),
                DefaultPrice = fajros[i].GetComponent<WorldItem>().GetDefaultPrice(),
                CurrentPrice = fajros[i].GetComponent<WorldItem>().GetCurrentPrice()
            };
            AnimoConnection.Insert(ft);
        }
    }

    //public void InsertMarketData()
    //{

    //}

    public TableQuery<PlayerTable> GetPlayerData()
    {
        var data = PlayerConnection.Table<PlayerTable>();
        return data;
    }

    public TableQuery<BackpackTable> GetBackpackData()
    {
        var data = BackpackConnection.Table<BackpackTable>();
        return data;
    }

    public TableQuery<AnimoTable> GetAnimoData()
    {
        var data = AnimoConnection.Table<AnimoTable>().Where(_ => _.MapId == currentMap);
        return data;
    }

    public TableQuery<BuildingTable> GetBuildingData()
    {
        var data = BuildingConnection.Table<BuildingTable>().Where(_ => _.MapId == currentMap);
        return data;
    }

    public TableQuery<PuzzleTable> GetPuzzleData()
    {
        var data = PuzzleConnection.Table<PuzzleTable>();
        return data;
    }

    public TableQuery<FajroTable> GetFajroData()
    {
        var data = FajroConnection.Table<FajroTable>();
        return data;
    }

    public TableQuery<MarketTable> GetMarketData()
    {
        var data = MarketConnection.Table<MarketTable>();
        return data;
    }

    public void TestInsertAnimo()
    {
        var at = new AnimoTable
        {
            AnimoId = 16,
            PosX = 5f,
            PosY = 5f,
            PosZ = 5f,
            IsHungry = true,
            MapId = currentMap
        };
        AnimoConnection.Insert(at);
        
    }
}
