using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public GameObject rootParent;


    // Start is called before the first frame update
    void Awake()
    {
        Instantiate(rootParent, transform);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
