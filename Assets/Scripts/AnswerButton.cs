using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnswerButton : MonoBehaviour
{
    public Text answerText;
    private AnswerData answerData;


	//Display answer
    public void Setup(AnswerData data)
    {
		answerData = data;
		answerText.text = answerData.answerText;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
