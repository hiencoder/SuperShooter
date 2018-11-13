using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text timeRemainingDisplayText;
    public Text highScoreDisplayText;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;
    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playScore;

    private List<GameObject> answerButtonObjects = new List<GameObject>();
    public SimpleObjectPool answerButtonObjectPool;
    // Use this for initialization
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.getCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemainingDisplay();

        playScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActive = true;
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);


        }
    }

    private void RemoveAnswerButtons()
    {
        while (answerButtonObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonObjects[0]);
            answerButtonObjects.RemoveAt(0);
        }
    }

    public void UpdateTimeRemainingDisplay()
    {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Update time remaining
        UpdateTimeRemainingDisplay();
        if (timeRemaining <= 0f)
        {
            EndRound();
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playScore += currentRoundData.pointAdded;
            scoreDisplayText.text = "Score: " + playScore.ToString();
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }
    }

    public void EndRound()
    {
        isRoundActive = false;
        dataController.SubmitNewHightScore(playScore);
        roundEndDisplay.SetActive(true);

        highScoreDisplayText.text = "Highest Score: " + dataController.GetHighestScore().ToString();

        questionDisplay.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Persistent");
    }

}
