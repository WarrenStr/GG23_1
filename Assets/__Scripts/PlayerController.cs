//Created by Sam

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Public Variables")]
    public bool stopMovement;

    [Header("Adjustable Variables")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] LayerMask whatStopMovement;

    [Header("Add before Runtime")]
    [SerializeField] Transform movePoint;


    void Start()
    {
        movePoint.parent = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMovement) { movePlayer(); }
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
        }
    }
}
