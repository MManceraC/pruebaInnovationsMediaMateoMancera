using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBullet : MonoBehaviour
{

    GameObject bulletDestroy;
    
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 8;
        bulletDestroy = GameObject.Find("bulletDestroy");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y >= bulletDestroy.transform.position.y)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            GameObject explosion =  Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.SetParent(GameObject.Find("characterMovement").transform);
            explosion.transform.localScale = new Vector3(1,1,1);
            Debug.Log(collision.gameObject.GetComponent<AudioSource>());
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
