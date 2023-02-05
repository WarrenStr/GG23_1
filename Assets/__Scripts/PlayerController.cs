//Created by Sam

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Public Variables")]
    public bool stopMovement;
    public bool treeFound = false;
    public bool specialTreeFound = false;
    public int mouseClick = 0;
    public GameObject currentTree = null;


    [Header("Adjustable Variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] LayerMask whatStopMovement;

    [Header("Add before Runtime")]
    [SerializeField] Transform movePoint;
    [SerializeField] Animator anim;
    [SerializeField] GameObject speechBubble;


    void Start()
    {
        movePoint.parent = null;
        
    }


    void Update()
    {
        if (!stopMovement) { movePlayer(); }

        TreeDetectionInput();
    }

    /// <summary>
    /// Moves player a distance of 1 unit (make grid 1 unit each) depending on which direction they input. 
    /// </summary>
    private void movePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime); //moves player towards the movePoint

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f) //prevents player from moving until they have reached the movePoint position
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) //Checks to see if left or right is pressed
            {
                Vector3 tempPos = movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f); //temp var to limit calculations
                if (!Physics2D.OverlapCircle(tempPos, .2f, whatStopMovement)) //checks to see if the movePoint will collide with an obj with the whatStopMovement layer
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
            currentTree = collision.gameObject;
            Debug.Log("Collision Detected Bitch");
        }

        if (collision.tag == "Special Tree")
        {
            specialTreeFound = true;
            currentTree = collision.gameObject;
            Debug.Log("Collision Detected Bitch");
        }

        if (collision.tag == "Root")
        {
            treeFound = false;
            Debug.Log("Root Found Bitch");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Tree")
        {
            treeFound = false;
            currentTree = null;
            Debug.Log("Collision Left Bitch");
        }

        if (collision.tag == "Tree")
        {
            treeFound = false;
            currentTree = null; ;
            Debug.Log("Collision LEft Bitch");
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

            if(Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                // Tree Shake Animation
                mouseClick++;
            }

            if(mouseClick>= 3)
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

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                // Tree Shake Animation
                mouseClick++;
            }

            if (mouseClick >= 3)
            {
                StartCoroutine(DestroySpecialTree());
            }
        }
    }

    private IEnumerator TreeInteraction()
    {
        // Tree Shake Animation

        treeFound= false;

        speechBubble.SetActive(true);

        yield return new WaitForSeconds(3);

        speechBubble.SetActive(false);
    }

    private IEnumerator DestroyTree()
    {
        // Tree Fall Animation

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

        yield return new WaitForSeconds(1);

        currentTree= null;
        Debug.Log("Tree Destroyed");
        specialTreeFound= false;
        mouseClick = 0; 
    }
}
