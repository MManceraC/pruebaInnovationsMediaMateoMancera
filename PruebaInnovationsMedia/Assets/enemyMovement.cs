using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyMovement : MonoBehaviour
{

    public GameObject player;

    private Vector2 targetPositionY, targetPositionX;

    public float speedX, speedY, step, RotationSpeed;

    public Transform limitleft, limitRight;

    public float randomX;

    public Sprite[] navesA, navesB, navesC;

    public bool naveA, naveB, naveC;

    public GameObject explosionPrefab, characterControllerScript;

    public GameObject scoreUI;


    // Start is called before the first frame update
    void Start()
    {
        characterControllerScript = GameObject.Find("characterMovement");
        scoreUI = GameObject.Find("scoreUI");

        randomX = Random.Range(limitleft.position.x, limitRight.position.x);

        int type = Random.Range(0,6);

        if (type >=0 && type<=3)
        {
            naveA = true;
            naveB = false;
            naveC = false;
        }
        else if (type > 3 && type <5)
        {
            naveA = false;
            naveB = true;
            naveC = false;
        }else if (type == 5)
        {
            naveA = false;
            naveB = false;
            naveC = true;
        }

        if (naveA)
        {
            int indexA = Random.Range(0, navesA.Length);
            gameObject.GetComponent<Image>().sprite = navesA[indexA];
            speedX = 200;
        }
        else if (naveB)
        {
            int indexB = Random.Range(0, navesB.Length);
            gameObject.GetComponent<Image>().sprite = navesB[indexB];
            speedX = 400;
        }
        else if (naveC)
        {
            int indexC = Random.Range(0, navesC.Length);
            gameObject.GetComponent<Image>().sprite = navesC[indexC];
            speedX = 600;
        }
    }

    // Update is called once per frame
    void Update()
    {
        step = speedY * Time.deltaTime;

        targetPositionY = new Vector2(gameObject.transform.position.x,player.transform.position.y);

        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPositionY, step);

        if (gameObject.transform.position.y != player.transform.position.y)
        {
            targetPositionX = new Vector2(randomX, gameObject.transform.position.y);
        }
        else {
            targetPositionX = new Vector2(player.transform.position.x, gameObject.transform.position.y);

        }

        float difference = gameObject.transform.position.x - randomX;

        if ((Mathf.Round(gameObject.transform.position.x * 100f) / 100f)  == (Mathf.Round(randomX * 100f) / 100f))
        {
            randomX = Random.Range(limitleft.position.x, limitRight.position.x);
        }

        step = speedX * Time.deltaTime;

       
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPositionX, step);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        killPlayer(collision);
    }

    private void OnDestroy()
    {
#if !UNITY_EDITOR
        if (characterControllerScript.GetComponent<characterControllerScript>().alive)
        {
            characterControllerScript.GetComponent<AudioSource>().Stop();
            characterControllerScript.GetComponent<AudioSource>().Play();

            if (naveA)
            {
                scoreUI.GetComponent<scoreUI>().Score += 50;
            }
            else if (naveB)
            {
                scoreUI.GetComponent<scoreUI>().Score += 100;
            }
            else if (naveC)
            {
                scoreUI.GetComponent<scoreUI>().Score += 150;
            }
        }
#endif




    }

    public void killPlayer(Collider2D collision)
    {
        if (collision.gameObject.name == player.name)
        {
            if (characterControllerScript.GetComponent<characterControllerScript>().canDie)
            {
                characterControllerScript.GetComponent<characterControllerScript>().alive = false;
                GameObject explosion = Instantiate(explosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
                explosion.transform.SetParent(GameObject.Find("Enemies").transform);
                explosion.transform.localScale = new Vector3(1, 1, 1);
                GameObject.Find("characterMovement").SetActive(false);
                GameObject.Find("EnemySpawner").GetComponent<enemySpawner>().enabled = false;
                GameObject.Find("EnemySpawner").GetComponent<enemySpawner>().CancelInvoke();
                GameObject.Find("Background").GetComponent<backgroundMovement>().enabled = false;
                GameObject.Find("EnemySpawner").SetActive(false);
                enemyMovement[] enemies = FindObjectsOfType<enemyMovement>();
                pickupObstaculo[] pickups = FindObjectsOfType<pickupObstaculo>();
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].enabled = false;
                }
                for (int i = 0; i < pickups.Length; i++)
                {
                    pickups[i].enabled = false;
                }
                GameObject.Find("startFinishWindows").GetComponent<Animator>().SetBool("in", true); ;
                GameObject.Find("startFinishWindows").GetComponent<Animator>().SetBool("out", false); ;
                GameObject.Find("startFinishWindows").GetComponent<startScript>().switchFinish();
                gameObject.GetComponent<AudioSource>().Stop();
                gameObject.GetComponent<AudioSource>().Play();

            }
            else
            {
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                explosion.transform.SetParent(GameObject.Find("characterMovement").transform);
                explosion.transform.localScale = new Vector3(1, 1, 1);
                Destroy(gameObject);
            }
        }
    }
}
