using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataHolder : MonoBehaviour
{
    public float maxScore;
    public bool secondTry;
    // Start is called before the first frame update
    void Start()
    {
        int numDataHolders = FindObjectsOfType<dataHolder>().Length;
        if (numDataHolders != 1)
        {
            Destroy(this.gameObject);
        }else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
