using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTree : MonoBehaviour
{
    public GameObject pickupItem;
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
        Instantiate(pickupItem, transform);
    }

    //public IEnumerator DropPickupItem()
    //{
    //    yield return new WaitForSeconds(3);

        
    //}
}
