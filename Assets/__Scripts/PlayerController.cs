//Created by Sam

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Public Variables")]
    public bool stopMovement;
    public bool treeFound = false;
    public bool specialTreeFound = false;
    public int mouseClick = 0;
    public GameObject currentTree = null;
    public TextMeshProUGUI speechBubbleText;
    public List<string> spText = new List<string>();


    [Header("Adjustable Variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] LayerMask whatStopMovement;

    [Header("Add before Runtime")]
    [SerializeField] Transform movePoint;
    [SerializeField] Animator anim;
    [SerializeField] GameObject speechBubble;


    private Animator currentTreeAnim;
    private GameManager itemsCollectedRef;


    void Start()
    {
        movePoint.parent = null;

        itemsCollectedRef = GameObject.FindObjectOfType<GameManager>();

    }


    void Update()
    {
        if (!stopMovement) { movePlayer(); }
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime); //moves player towards the movePoint
        TreeDetectionInput();
    }

    /// <summary>
    /// Moves player a distance of 1 unit (make grid 1 unit each) depending on which direction they input. 
    /// </summary>
    private void movePlayer()
    {
        

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f) //prevents player from moving until they have reached the movePoint position
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) //Checks to see if left or right is pressed
            {
                Vector3 tempPos = movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f); //temp var to limit calculations
                if (!Physics2D.OverlapCircle(tempPos, .1f, whatStopMovement)) //checks to see if the movePoint will collide with an obj with the whatStopMovement layer
                {
                    movePoint.position = tempPos; //moves movePoint to left or right, depending on which direction is pressed
                }
            }

            //prevent diagonal movement by making the following if statment an else if statement

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)//Checks to see if up or down is pressed
            {
                Vector3 tempPos = movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);//temp var to limit calculations
                if (!Physics2D.OverlapCircle(tempPos, .2f, whatStopMovement)) //checks to see if the movePoint will collide with an obj with the whatStopMovement layer
                {
                    movePoint.position = tempPos; //moves movePoint to up or down, depending on which direction is pressed
                }
            }

            anim.SetBool("boolInAnim", false); //for future anim use
        }
        else
        {
            anim.SetBool("boolInAnim", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tree")
        {
            treeFound = true;
            specialTreeFound = false;
            currentTree = collision.gameObject;
            currentTreeAnim = currentTree.GetComponent<Animator>();
            Debug.Log("Collision Detected Bitch");
        }

        if (collision.tag == "Special Tree")
        {
            specialTreeFound = true;
            treeFound = false;
            currentTree = collision.gameObject;
            currentTreeAnim = currentTree.GetComponent<Animator>();
            Debug.Log("Collision Detected Bitch");
        }

        if (collision.tag == "Root")
        {
            treeFound = false;
            Debug.Log("Root Found Bitch");
        }

        if (collision.tag =="Special Item")
        {
            itemsCollectedRef.CountItemsCollected();
            StartCoroutine(ItemInteraction());
            Destroy(collision.gameObject);
        }

        //if (collision.tag == "Wolf")
        //{
        //    StartCoroutine(EndGame());
        //}

    }

    public void collideWithWolf()
    {
        StartCoroutine(EndGame());
    }
    public void collideWithRoot()
    {
        StartCoroutine(EndGame());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Special Item")
        {
            itemsCollectedRef.CountItemsCollected();
            StartCoroutine(ItemInteraction());
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tree")
        {
            treeFound = false;
            currentTree = null;
            mouseClick = 0;
            Debug.Log("Collision Left Bitch");
        }

        if (collision.tag == "Special Tree")
        {
            specialTreeFound = false;
            currentTree = null;
            mouseClick = 0;
            Debug.Log("Special Col LEft Bitch");
        }

        if (collision.tag == "Root")
        {
            treeFound = false;
            Debug.Log("Root Left Bitch");
        }
    }

    public void TreeDetectionInput()
    {
        if (treeFound && currentTree != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(TreeInteraction());
            }

            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                currentTreeAnim.SetBool("Tree shed", true);
                mouseClick++;
            }

            if(mouseClick>= 2)
            {
                StartCoroutine(DestroyTree());
            }
        }

        if (specialTreeFound && currentTree != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(TreeInteraction());
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentTreeAnim.SetBool("Tree shed", true);
                mouseClick++;
            }

            if (mouseClick >= 2)
            {
                StartCoroutine(DestroySpecialTree());
            }
        }
    }

    private IEnumerator TreeInteraction()
    {
        currentTreeAnim.SetBool("Tree shake", true);
        yield return new WaitForSeconds(.4f);
        currentTreeAnim.SetBool("Tree shake", false);

        treeFound = false;
        specialTreeFound = false;

        speechBubble.SetActive(true);

        
        speechBubbleText.text = spText[Random.Range(0, spText.Count)];

        yield return new WaitForSeconds(3);


        speechBubble.SetActive(false);
    }

    private IEnumerator ItemInteraction()
    {
        // Tree Shake Animation

        treeFound = false;
        specialTreeFound = false;   

        speechBubble.SetActive(true);

        speechBubbleText.text = "Item found!";

        yield return new WaitForSeconds(3);

        speechBubble.SetActive(false);
    }

    private IEnumerator DestroyTree()
    {
        currentTreeAnim.SetBool("Tree fall", true);

        yield return new WaitForSeconds(1);

        Destroy(currentTree);
        currentTree = null;
        Debug.Log("Tree Destroyed");
        treeFound = false;
        mouseClick = 0;
    }

    private IEnumerator DestroySpecialTree()
    {
        // Tree Fall Animation

        currentTree.GetComponent<SpecialTree>().DropItem();
        currentTree = null;
        yield return new WaitForSeconds(1);

        //currentTree= null;
        Debug.Log("Tree Destroyed");
        specialTreeFound= false;
        mouseClick = 0; 
    }

    private IEnumerator EndGame()
    {
        stopMovement = true;
        // Player death anim
        Debug.Log("Wolf");
        anim.SetBool("guydie", true);

        yield return new WaitForSeconds(4);

        itemsCollectedRef.EndGame();
    }
}
