using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startScript : MonoBehaviour
{

    public GameObject spawner, character, dataHolder, start, finish, scoreUI;

    public Text tuPuntaje, puntajeMaximo;


    // Start is called before the first frame update

    private void Awake()
    {
        start.SetActive(true);
        finish.SetActive(false);

        spawner = GameObject.Find("EnemySpawner");
        spawner.SetActive(false);
        character = GameObject.Find("characterMovement");
        dataHolder = GameObject.Find("dataHolder");
        scoreUI = GameObject.Find("scoreUI");
    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        tuPuntaje.text = "TU PUNTAJE: " + scoreUI.GetComponent<scoreUI>().Score;
        puntajeMaximo.text = "PUNTAJE MÁXIMO: " + dataHolder.GetComponent<dataHolder>().maxScore;
    }

    public void startGame()
    {
        spawner.SetActive(true);
        character.GetComponent<characterControllerScript>().enabled = true;
        character.GetComponent<shooting>().enabled = true;

        gameObject.GetComponent<Animator>().SetBool("out",true);
    }

    public void switchFinish()
    {
        start.SetActive(false);
        finish.SetActive(true);
    }

    public void restartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
