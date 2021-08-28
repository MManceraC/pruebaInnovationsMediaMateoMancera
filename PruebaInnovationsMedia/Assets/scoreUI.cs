using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scoreUI : MonoBehaviour
{
    public float Score;
    public Text textScore, textHiScore;
    public GameObject corona, dataHolder;
    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        corona.SetActive(false);
        dataHolder = GameObject.Find("dataHolder");
        textHiScore.text = "HIGH SCORE:\n" + dataHolder.GetComponent<dataHolder>().maxScore;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "SCORE:\n" + Score;
        textHiScore.text = "HIGH SCORE:\n" + dataHolder.GetComponent<dataHolder>().maxScore;

        if (dataHolder.GetComponent<dataHolder>().maxScore <= Score)
        {
            corona.SetActive(true);
            dataHolder.GetComponent<dataHolder>().maxScore = Score;
        }
    }
}
