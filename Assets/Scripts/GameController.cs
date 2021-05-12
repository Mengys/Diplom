using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject realPlayerPrefab;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] UIInventory uiInventory;


    private ServerController serverController;
    private bool inventoryOpened = false;


    public long myId;
    public JsonData data;
    public ConcurrentDictionary<long, GameObject> players = new ConcurrentDictionary<long,GameObject>();
    public ConcurrentDictionary<long, GameObject> mobs = new ConcurrentDictionary<long, GameObject>();
    public ConcurrentDictionary<long, GameObject> projectiles = new ConcurrentDictionary<long, GameObject>();
    public ConcurrentDictionary<long, WorldItem> worldItems = new ConcurrentDictionary<long, WorldItem>();
    public Inventory inventory;


    void Start()
    {
        serverController = GameObject.FindGameObjectWithTag("ServerController").GetComponent<ServerController>();
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        uiInventory.gameObject.SetActive(inventoryOpened);
    }

    void Update()
    {

        UpdateWorld();

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (horizontalInput != 0f || verticalInput != 0f)
        {
            ServerController.Move(horizontalInput, verticalInput, Time.deltaTime);
        }

        if (Input.GetMouseButton(0) && !inventoryOpened)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 direction;
            direction.x = mousePosition.x - Screen.width / 2;
            direction.y = mousePosition.y - Screen.height / 2;

            ServerController.Attack(direction.x, direction.y);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryOpened = !inventoryOpened;
            uiInventory.gameObject.SetActive(inventoryOpened);
        }
    }

    void UpdatePlayers()
    {
        List<long> idsForDelete = new List<long>();

        foreach (long id in players.Keys)
        {
            idsForDelete.Add(id);
        }

        foreach (JsonPlayer player in data.players)
        {
            if (!players.ContainsKey(player.id))
            {
                if (player.id == myId)
                {
                    GameObject newPlayer = Instantiate(realPlayerPrefab);
                    players.TryAdd(player.id, newPlayer);
                }
                else
                {
                    GameObject newPlayer = Instantiate(playerPrefab);
                    players.TryAdd(player.id, newPlayer);
                }
            }

            if (player.id == myId)
            {
                players[player.id].GetComponent<RealPlayer>().jsonPlayer = player;
            }
            else
            {
                players[player.id].GetComponent<Player>().jsonPlayer = player;
            }

            if (idsForDelete.Contains(player.id))
            {
                idsForDelete.Remove(player.id);
            }
        }

        foreach (long id in idsForDelete)
        {
            GameObject objForDestroy;
            players.TryRemove(id, out objForDestroy);
            Destroy(objForDestroy);
        }
    }

    void UpdateMobs()
    {
        List<long> idsForDelete = new List<long>();

        foreach (long id in mobs.Keys)
        {
            idsForDelete.Add(id);
        }

        foreach (JsonMob mob in data.mobs)
        {
            if (!mobs.ContainsKey(mob.id))
            {
                enemyPrefab.transform.position = new Vector3(mob.x, mob.y, 0);
                GameObject newMob = Instantiate(enemyPrefab);
                mobs.TryAdd(mob.id, newMob);
            }
            mobs[mob.id].GetComponent<Mob>().jsonMob = mob;
            mobs[mob.id].GetComponent<Mob>().updated = true;
            if (idsForDelete.Contains(mob.id))
            {
                idsForDelete.Remove(mob.id);
            }
        }
        foreach (long id in idsForDelete)
        {
            GameObject objForDestroy;
            mobs.TryRemove(id, out objForDestroy);
            Destroy(objForDestroy);
        }
    }

    void UpdateProjectiles()
    {
        List<long> idsForDelete = new List<long>();

        foreach (long id in projectiles.Keys)
        {
            idsForDelete.Add(id);
        }

        foreach (JsonProjectile projectile in data.projectiles)
        {
            if (!projectiles.ContainsKey(projectile.id))
            {
                Quaternion quat = projectilePrefab.transform.rotation;

                float angle = Vector3.Angle(Vector3.right, new Vector3(projectile.dirX, projectile.dirY, 0));
                if (projectile.dirY < 0)
                {
                    angle = -angle;
                }
                quat.eulerAngles = new Vector3(0, 0, angle);
                GameObject newProjectile = Instantiate(projectilePrefab, new Vector3(projectile.x, projectile.y, 0), quat);
                projectiles.TryAdd(projectile.id, newProjectile);
            }
            projectiles[projectile.id].GetComponent<Projectile>().jsonProjectile = projectile;
            if (idsForDelete.Contains(projectile.id))
            {
                idsForDelete.Remove(projectile.id);
            }
        }

        foreach (long id in idsForDelete)
        {
            GameObject objForDestroy;
            projectiles.TryRemove(id, out objForDestroy);
            Destroy(objForDestroy);
        }
    }

    void UpdateWorldItems()
    {
        List<long> idsForDelete = new List<long>();

        foreach (long id in worldItems.Keys)
        {
            idsForDelete.Add(id);
        }
        
        foreach (JsonDrop worldItem in data.drops)
        {
            if (!worldItems.ContainsKey(worldItem.id))
            {
                WorldItem newWorldItem = WorldItem.SpawnWorldItem(new Vector3(worldItem.x, worldItem.y), new Item(worldItem.itemId,1), 1);
                newWorldItem.drop = worldItem;
                worldItems.TryAdd(worldItem.id, newWorldItem);
            }
            if (idsForDelete.Contains(worldItem.id))
            {
                idsForDelete.Remove(worldItem.id);
            }
        }

        foreach (long id in idsForDelete)
        {
            WorldItem objForDestroy;
            worldItems.TryRemove(id, out objForDestroy);
            objForDestroy.DestroySelf();
        }
    }

    void UpdateInventory()
    {
        inventory.Update(data.inventoryItem);
    }

    void UpdateWorld()
    {
        UpdatePlayers();
        UpdateMobs();
        UpdateProjectiles();
        UpdateWorldItems();
        UpdateInventory();
    }
}
