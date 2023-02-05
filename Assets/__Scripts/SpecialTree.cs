using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTree : MonoBehaviour
{
    public Sprite pickupItem;
    public bool isTreeDestroyed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem() 
    {
        GetComponent<SpriteRenderer>().sprite = pickupItem;
        isTreeDestroyed = true;
    }



    //public IEnumerator DropPickupItem()
    //{
    //    yield return new WaitForSeconds(3);


    //}
}
