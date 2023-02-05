using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTree : MonoBehaviour
{
    public Sprite pickupItem;
    public bool isTreeDestroyed = false;

    
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpecialItemCheck();
    }

    public void DropItem() 
    {
        GetComponent<SpriteRenderer>().sprite = pickupItem;
        isTreeDestroyed = true;
    }

    public void SpecialItemCheck()
    {
        if (isTreeDestroyed)
        {
            gameObject.tag = "Special Item";
        }
    }


    //public IEnumerator DropPickupItem()
    //{
    //    yield return new WaitForSeconds(3);


    //}
}
