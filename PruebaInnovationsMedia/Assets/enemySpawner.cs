using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab, rockPrefab, pickupPrefab;

    public enemyMovement referenceShip;

    public int tiempo;

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void spawnEnemy()
    {
        int random = Random.Range(0,6);

        int cantidad = 0;

        if (random >= 0 && random<=2)
        {
            cantidad = 1;
        }else if (random > 2 && random < 5)
        {
            cantidad = 2;
        }else if (random == 5)
        {
            cantidad = 3;
        }

        for (int i = 0; i<cantidad; i++)
        {
            GameObject newShip = (GameObject)Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
            newShip.transform.SetParent(gameObject.transform.parent);
            newShip.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            newShip.GetComponent<enemyMovement>().player = referenceShip.player;
            newShip.GetComponent<enemyMovement>().limitleft = referenceShip.limitleft;
            newShip.GetComponent<enemyMovement>().limitRight = referenceShip.limitRight;
        }

        random = Random.Range(0,3);
        if (random == 0)
        {
            GameObject newRock= (GameObject)Instantiate(rockPrefab, gameObject.transform.position, Quaternion.identity);
            newRock.transform.SetParent(gameObject.transform.parent);
            newRock.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            newRock.GetComponent<pickupObstaculo>().player = referenceShip.player.transform;
            newRock.GetComponent<pickupObstaculo>().limitLeft = referenceShip.limitleft;
            newRock.GetComponent<pickupObstaculo>().limitRight = referenceShip.limitRight;
        }
        

        Invoke("spawnEnemy",tiempo);
    }

    

}
