using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootController : MonoBehaviour
{

    [Header("Public Variables")]
    public bool stopMovement;

    [Header("Adjustable Variables")]
    [SerializeField] float timeToMove = 5f;
    [SerializeField] LayerMask whatStopMovement;

    [Header("Add before Runtime")]
    [SerializeField] Transform movePoint;
    [SerializeField] GameObject RootTrail;
    [SerializeField] GameObject Tree;


    void Start()
    {
        movePoint.parent = null;
        StartCoroutine("moveCycle");
    }

    IEnumerator moveCycle()
    {
        yield return new WaitForSeconds(timeToMove);


        bool findSpotToMove = false;
        int i = 0;
        while (!findSpotToMove)
        {
            int horizontal = 0;
            int verticle = 0;
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
                Instantiate(RootTrail, gameObject.transform.position, Quaternion.identity, Tree.transform);
                
            }

            i++;
            if (i == 6)
            {
                findSpotToMove = true;
            }
        }



        transform.position = movePoint.position;
        StartCoroutine("moveCycle");
    }


    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
    }
}
