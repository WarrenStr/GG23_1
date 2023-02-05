using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Public Variables")]
    public bool stopMovement;

    [Header("Adjustable Variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] LayerMask whatStopMovement;

    [Header("Add before Runtime")]
    [SerializeField] Transform movePoint;
    [SerializeField] Animator anim;

    private bool nearPlayer;
    private int directionToPlayer;
    private GameObject player;

    void Start()
    {
        movePoint.parent = null;
        StartCoroutine("moveCycle");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //public void FoundPlayer(int direction)
    //{
    //    //0 north, 1 south, 2 east, 3 west
    //    // v1          v-1   h1       h-1

    //    directionToPlayer = direction;
    //    nearPlayer = true;
    //}
    //public void LostPlayer()
    //{
    //    nearPlayer = false;
    //}

    IEnumerator moveCycle()
    {

        yield return new WaitForSeconds(1f);

        Vector3 comparison =  player.transform.position - gameObject.transform.position;


        bool findSpotToMove = false;
        int horizontal = 0;
        int verticle = 0;

        if (Random.Range(0, 2) == 0)
        {
            if(comparison.x < 0)
            {
                horizontal = -1;
            }
            else
            {
                horizontal = 1;
            }
        }
        else
        {
            if (comparison.y < 0)
            {
                verticle = -1;
            }
            else
            {
                verticle = 1;
            }
        }


        if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontal, verticle, 0f), .2f, whatStopMovement))
        {
            movePoint.position = movePoint.position += new Vector3(horizontal, verticle, 0f);
            
        }
        else
        {
            int i = 0;
            while (!findSpotToMove)
            {
                horizontal = 0;
                verticle = 0;
                switch (Random.Range(0, 4))
                {
                    case 3:
                        horizontal = 1;
                        break;
                    case 2:
                        horizontal = -1;
                        break;
                    case 1:
                        verticle = 1;
                        break;
                    case 0:
                        verticle = -1;
                        break;

                }


                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontal, verticle, 0f), .2f, whatStopMovement))
                {
                    movePoint.position = movePoint.position += new Vector3(horizontal, verticle, 0f);
                    findSpotToMove = true;
                }
                i++;
                if (i == 6)
                {
                    findSpotToMove = true;
                }
            }
        }

        

        //if (nearPlayer)
        //{
        //    switch (directionToPlayer)
        //    {
        //        case 3:
        //            horizontal = -1;
        //            break;
        //        case 2:
        //            horizontal = 1;
        //            break;
        //        case 1:
        //            verticle = -1;
        //            break;
        //        case 0:
        //            verticle = 1;
        //            break;

        //    }
        //    print(directionToPlayer);
        //    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontal, verticle, 0f), .2f, whatStopMovement))
        //    {
        //        movePoint.position = movePoint.position += new Vector3(horizontal, verticle, 0f);
        //        findSpotToMove = true;
        //    }
            
        //}

        transform.position = movePoint.position;
        StartCoroutine("moveCycle");
    }


    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }
}
