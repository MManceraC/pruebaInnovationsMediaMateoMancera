using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMovement : MonoBehaviour
{
    public GameObject[] rows;

    public GameObject lowLimit, topLimit;

    public float speed, step;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;


        for (int i = 0; i<rows.Length; i++)
        {
            rows[i].transform.position = Vector2.MoveTowards(rows[i].transform.position, lowLimit.transform.position, step);

            if (rows[i].transform.position.y <= lowLimit.transform.position.y + 1 && rows[i].transform.position.y >= lowLimit.transform.position.y - 1)
            {
                rows[i].transform.position = topLimit.transform.position;
            }
        }
    }
}
