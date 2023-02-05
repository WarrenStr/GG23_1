using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loseGame : MonoBehaviour
{
    public PlayerController pc;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.tag == "Wolf")
        {

            pc.collideWithWolf();
        }
        if (collision.tag == "Root")
        {

            pc.collideWithWolf();
        }

    }
}
