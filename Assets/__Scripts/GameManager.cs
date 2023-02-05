using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int tileLayer = 9;
    [SerializeField]private List<GameObject> groundTiles = new List<GameObject>();

    [Header("Tree Stuff")]
    public float specialTreeSpawnRate;
    public GameObject[] treePrefabs;
    public TextMeshProUGUI timerText;
    public float treeSpawnInterval = 1f;
    private float treeSpawnTimer;

    [Header("Wolf Stuff")]
    public GameObject wolf;
    public float wolfSpawnInterval = 1f;
    private float wolfSpawnTimer;

    [Header("Item Stuff")]
    public int itemsCollected = -1;
    public int itemsToWin = 5;
    public TextMeshProUGUI itemCount;
    public GameObject WinScreen;
    public float gameTimer;
    public TextMeshProUGUI gameTimerText;
    public TextMeshProUGUI winScreenTimerText;
    public bool gameIsOn = true;

    // Start is called before the first frame update
    void Start()
    {
        FindTiles();
        CountItemsCollected();

        treeSpawnTimer = treeSpawnInterval;
        wolfSpawnTimer = wolfSpawnInterval;


    }

    // Update is called once per frame
    void Update()
    {
        SpawnTrees();
        SpawnWolves();

        if (gameIsOn)
        {
            gameTimer += Time.deltaTime;
            gameTimerText.text = gameTimer.ToString("0:00");
        }
    }

    public void SpawnTrees() // Picks a random tile from the GroundTile list and spawns a tree there every 30 seconds
    {
        //treeSpawn.transform.position = treeSpawn.transform.position + rootOffset; // Un-comment this line for a cool 

        timerText.text = treeSpawnTimer.ToString("0");
        treeSpawnTimer -= Time.deltaTime;  
        
        if (treeSpawnTimer <= 0 && Random.value < specialTreeSpawnRate)
        {
            GameObject treeSpawn = groundTiles[Random.Range(0, groundTiles.Count)];

            Vector3 rootOffset = new Vector3(0, -0.5f, 0);

            Instantiate(treePrefabs[0], treeSpawn.transform);
            //treePrefab.transform.localPosition = rootOffset;
            treeSpawnTimer = treeSpawnInterval;
        }

        if (treeSpawnTimer <= 0 && Random.value >= specialTreeSpawnRate)
        {
            GameObject treeSpawn = groundTiles[Random.Range(0, groundTiles.Count)];

            Vector3 rootOffset = new Vector3(0, -0.5f, 0);

            Instantiate(treePrefabs[1], treeSpawn.transform);
            //treePrefab.transform.localPosition = rootOffset;
            treeSpawnTimer = treeSpawnInterval;
        }
    }

    public void SpawnWolves() // Picks a random tile from the GroundTile list and spawns a tree there every 30 seconds
    {
        //treeSpawn.transform.position = treeSpawn.transform.position + rootOffset; // Un-comment this line for a cool 

        timerText.text = wolfSpawnTimer.ToString("0");
        wolfSpawnTimer -= Time.deltaTime;

        if (wolfSpawnTimer <= 0)
        {
            GameObject wolfSpawn = groundTiles[Random.Range(0, groundTiles.Count)];

            Vector3 rootOffset = new Vector3(0, -0.5f, 0);

            Instantiate(wolf, wolfSpawn.transform);
            //treePrefab.transform.localPosition = rootOffset;
            wolfSpawnTimer = wolfSpawnInterval;
        }

        if (wolfSpawnTimer <= 0)
        {
            GameObject wolfSpawn = groundTiles[Random.Range(0, groundTiles.Count)];

            Vector3 rootOffset = new Vector3(0, -0.5f, 0);

            Instantiate(wolf, wolfSpawn.transform);
            //treePrefab.transform.localPosition = rootOffset;
            wolfSpawnTimer = wolfSpawnInterval;
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

    public void CountItemsCollected()
    {
        itemsCollected++;
        itemCount.text = itemsCollected.ToString("0") + " / " + itemsToWin.ToString("0");
        if(itemsCollected >= itemsToWin)
        {
            //itemCount.gameObject.SetActive(false);
            // Avengers End Game
            gameIsOn = false;
            winScreenTimerText.text = "Time: " + gameTimer.ToString("0:00");
            Time.timeScale = .5f;
            WinScreen.gameObject.SetActive(true);
            Debug.Log("Game Has Been Won");
        }
    }

    public void RestartGameButton()
    {
        Time.timeScale = 1;
        gameIsOn = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
