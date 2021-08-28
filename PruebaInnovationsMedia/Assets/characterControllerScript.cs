using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControllerScript : MonoBehaviour
{

    public Transform ship, limitLeft, limitRight, initialPos, targetRotationLeft, targetRotationRight, targetRotationDefault, limitRotationRight, limitRotationLeft;

    public bool goRight, goLeft;

    public float speed, step, RotationSpeed;

    public shooting shootingScript;

    private float baseFireRate, baseSpeed;

    public bool canDie, alive;

    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        baseFireRate = shootingScript.fireRate;
        canDie = true;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDie && shield.activeSelf)
        {
            shield.SetActive(false);
        }
        else if(!canDie && !shield.activeSelf)
        {
            shield.SetActive(true);
        }


        step = speed * Time.deltaTime;


        if (Input.GetKey(KeyCode.A) && !goRight)
        {
            goLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            goLeft = false;
        }

        if (Input.GetKey(KeyCode.D) && !goLeft)
        {
            goRight = true;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            goRight = false;
        }

        if (goRight)
        {
            Debug.Log("MoveRight");
            ship.transform.position = Vector2.MoveTowards(ship.transform.position, limitRight.position, step);

            targetRotationDefault.transform.position = Vector2.MoveTowards(targetRotationDefault.transform.position, limitRotationRight.position, step);

            float singleStep = RotationSpeed * Time.deltaTime;

            ship.up = Vector3.Lerp(ship.up, (targetRotationRight.position - ship.position), RotationSpeed);


        }
        else if (goLeft)
        {
            Debug.Log("MoveLeft");
            ship.transform.position = Vector2.MoveTowards(ship.transform.position, limitLeft.position, step);
            targetRotationDefault.transform.position = Vector2.MoveTowards(targetRotationDefault.transform.position, limitRotationLeft.position, step);

            float singleStep = RotationSpeed * Time.deltaTime;

            ship.up = Vector3.Lerp(ship.up, (targetRotationLeft.position - ship.position), RotationSpeed);

        }
        else
        {
            ship.up = Vector3.Lerp(ship.up, (targetRotationDefault.position - ship.position), RotationSpeed);

        }




    }

    public void powerUp(int type)
    {
        if (type == 0)
        {
            speed = speed + (speed*0.5f);
        }else if (type == 1)
        {
            shootingScript.fireRate = shootingScript.fireRate - (shootingScript.fireRate * 0.5f);
        }
        else if(type == 2){
            canDie = false;
        }
        Invoke("returnToBase",5f);
    }

    private void returnToBase()
    {
        Debug.Log("Do return to base values ");
        speed = baseSpeed;
        shootingScript.fireRate = baseFireRate;
        canDie = true;
    }
}
