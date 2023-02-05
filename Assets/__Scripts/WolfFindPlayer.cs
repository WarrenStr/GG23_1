using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFindPlayer : MonoBehaviour
{
    [SerializeField] bool north, south, east, west;
    [SerializeField] EnemyController ec;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Player")
    //    {
    //        //0 north, 1 south, 2 east, 3 west
    //        if (west)
    //        {
    //            ec.FoundPlayer(3);
    //        }
    //        if (east)
    //        {
    //            ec.FoundPlayer(2);
    //        }
    //        if (south)
    //        {
    //            ec.FoundPlayer(1);
    //        }
    //        if (north)
    //        {
    //            ec.FoundPlayer(0);
    //        }
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        ec.LostPlayer();
    //    }
    //}
}
