using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupObstaculo : MonoBehaviour
{

    public bool pickup, obstaculo;

    public Sprite[] spritesObstaculo, spritesPickup;

    public Transform limitLeft, limitRight, player;

    public float randomX;

    public float speed, step;

    public GameObject destroyEffectPrefab, effectRock, effectPickup, characterControllerScript;

    Vector2 objectiveRock;

    int typePickup;

    bool pickedByPlayer;

    // Start is called before the first frame update
    void Start()
    {
        characterControllerScript = GameObject.Find("characterMovement");


        randomX = Random.Range(limitLeft.position.x, limitRight.position.x);

        gameObject.transform.position = new Vector2(randomX, gameObject.transform.position.y);

        int isPickup = Random.Range(0,3);

        if (isPickup == 0)
        {
            pickup = true;
            obstaculo = false;
        }
        else {
            pickup = false;
            obstaculo = true;
        }

        if (pickup)
        {
            gameObject.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
            typePickup = Random.Range(0,3); //0 = Speed, 1 = FireRate, 2 = Invincibility
            gameObject.layer = 7;
            if (typePickup == 0)
            {
                gameObject.GetComponent<Image>().sprite = spritesPickup[0];
            }
            else if (typePickup == 1)
            {
                gameObject.GetComponent<Image>().sprite = spritesPickup[1];
            }
            else if (typePickup == 2)
            {
                gameObject.GetComponent<Image>().sprite = spritesPickup[2];
            }

            destroyEffectPrefab = effectPickup;
            objectiveRock = new Vector2(gameObject.transform.position.x, player.position.y - 200f);
        }
        else if (obstaculo)
        {
            gameObject.GetComponent<Image>().sprite = spritesObstaculo[Random.Range(0, spritesObstaculo.Length)];
            destroyEffectPrefab = effectRock;
            objectiveRock = player.position;

        }




    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;



        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, objectiveRock, step);

        if (gameObject.transform.position.y <= player.transform.position.y + 1f && gameObject.transform.position.y >= player.transform.position.y - 1f)
        {
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
#if !UNITY_EDITOR
        if (characterControllerScript.GetComponent<characterControllerScript>().alive)
        {
            if (pickedByPlayer)
            {
                GameObject explosion = Instantiate(destroyEffectPrefab, player.transform.position, Quaternion.identity);
                explosion.transform.SetParent(GameObject.Find("ship").transform);
                explosion.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                destroyEffectPrefab = effectRock;
                GameObject explosion = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
                explosion.transform.SetParent(GameObject.Find("characterMovement").transform);
                explosion.transform.localScale = new Vector3(1, 1, 1);
                characterControllerScript.GetComponent<AudioSource>().Play();

            }



        }
#endif


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "ship")
        {
            if (obstaculo)
            {
                killPlayer(collision);
            }else if (pickup)
            {
                GameObject.Find("characterMovement").GetComponent<characterControllerScript>().powerUp(typePickup);
                pickedByPlayer = true;
                Destroy(gameObject);
            }
            
        }
    }

    public void killPlayer(Collider2D collision)
    {
        if (collision.gameObject.name == player.name)
        {
            if (characterControllerScript.GetComponent<characterControllerScript>().canDie)
            {
                characterControllerScript.GetComponent<characterControllerScript>().alive = false;
                GameObject explosion = Instantiate(destroyEffectPrefab, collision.gameObject.transform.position, Quaternion.identity);
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
                GameObject explosion = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
                explosion.transform.SetParent(GameObject.Find("characterMovement").transform);
                explosion.transform.localScale = new Vector3(1, 1, 1);
                Destroy(gameObject);
            }
        }
    }
}
