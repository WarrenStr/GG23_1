using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int tileLayer = 9;
    [SerializeField]private List<GameObject> groundTiles = new List<GameObject>();

    [Header("Tree Stuff")]
    public GameObject treePrefab;
    public TextMeshProUGUI timerText;
    public float spawnInterval = 1f;
    private float spawnTimer;   
    

    // Start is called before the first frame update
    void Start()
    {
        FindTiles();

        spawnTimer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTrees();
    }

    public void SpawnTrees() // Picks a random tile from the GroundTile list and spawns a tree there every 30 seconds
    {
        GameObject treeSpawn = groundTiles[Random.Range(0, groundTiles.Count)];

        Vector3 rootOffset = new Vector3(0, -0.5f, 0);

        //treeSpawn.transform.position = treeSpawn.transform.position + rootOffset; // Un-comment this line for a cool 

        timerText.text = spawnTimer.ToString("0");
        spawnTimer -= Time.deltaTime;   
        if (spawnTimer <= 0 )
        {
            Instantiate(treePrefab, treeSpawn.transform);
            //treePrefab.transform.localPosition = rootOffset;
            spawnTimer = spawnInterval;
        }
    }

    void FindTiles() // Runs through gameobjects and finds all the tiles in the game, then populates the tile list 
    {
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        foreach(GameObject tile in allGameObjects)
        {
            if(tile.layer == tileLayer)
            {
                groundTiles.Add(tile);
            }
        }
    }
}
