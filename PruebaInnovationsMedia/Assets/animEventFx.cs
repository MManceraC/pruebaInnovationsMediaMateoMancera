using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animEventFx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endAnimation()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
