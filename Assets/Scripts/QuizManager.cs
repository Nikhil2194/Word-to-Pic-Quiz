using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;
    [SerializeField] private QuizDataScriptable questionData;
    [SerializeField] private Image questionImage;
    [SerializeField] private WordData[] answerwordArray;
    [SerializeField] private WordData[] optionWordArray;
    [SerializeField] private char[] charArray = new char[12];
    private List<int> selectedWordIndex = new List<int>();
    public int currentAnsIndex=0;
    public bool correctAnswer = false;
    private int currntQuestionsIndex;
    private GameStatus gameStatus = GameStatus.Playing;
    private string answerWord;
    [SerializeField] private GameObject WinningGameObject;

    private void Awake()
    {
        if (instance == null) instance = this;      // Quiz Manager Singleton
        else
            Destroy(gameObject);
    }


    private void Start()
    {
        SetQuestion();
    }
    private void SetQuestion()
    {
        currentAnsIndex = 0;
        selectedWordIndex.Clear();
        WinningGameObject.SetActive(false);

        questionImage.sprite = questionData.questions[currntQuestionsIndex].QuestionImage;
        answerWord = questionData.questions[currntQuestionsIndex].answer;

        ResetQuestion();

        for (int i = 0;i< answerWord.Length;i++)
        {
            charArray[i] =char.ToUpper(answerWord[i]);    //Storing Characters from answer string one by one in a charArray
        }

        for(int i = answerWord.Length; i<optionWordArray.Length;i++)
        {
            charArray[i] = (char)UnityEngine.Random.Range(65, 91);    // Storing Characters and also adding random Characters in CharArray
        }

        charArray = ShuffleList.ShuffleListItems<char>(charArray.ToList()).ToArray();   //Shuffling the charArray Characters


        for(int i =0; i<optionWordArray.Length;i++)
        {
            optionWordArray[i].SetChar(charArray[i]);
        }
        currntQuestionsIndex++;
        gameStatus = GameStatus.Playing;
    }

    public void SelectedOption(WordData wordData)
    {
        if (gameStatus==GameStatus.Next || currentAnsIndex >= answerWord.Length) return;

        selectedWordIndex.Add(wordData.transform.GetSiblingIndex());      // Adding that char in the list for deleting
        answerwordArray[currentAnsIndex].SetChar(wordData.charValue);
        wordData.gameObject.SetActive(false);
        currentAnsIndex++;

        if(currentAnsIndex >= answerWord.Length)
        {
            correctAnswer = true;

            for(int i = 0;i< answerWord.Length;i++)     // iterate through answer string
            {
                if(char.ToUpper(answerWord[i]) != char.ToUpper(answerwordArray[i].charValue)) //compare each char value with answer char value
                {
                    correctAnswer = false;
                    break;
                }
            }

            if (correctAnswer)
            {
                Debug.Log("Answer is Correct");
                gameStatus = GameStatus.Next;
                WinningGameObject.SetActive(true);

                if (currntQuestionsIndex< questionData.questions.Count)
                Invoke("SetQuestion",1f);
            }
             
            else
                Debug.Log("wrong Answer");
        }
    }

    public void ResetQuestion()
    {
        for(int i= 0; i<answerwordArray.Length;i++)
        {
            answerwordArray[i].gameObject.SetActive(true);
            answerwordArray[i].SetChar('-');
        }

        for(int i = answerWord.Length; i<answerwordArray.Length;i++)
        {
            answerwordArray[i].gameObject.SetActive(false);
        }


        for(int i =0;i<optionWordArray.Length;i++)
        {
            optionWordArray[i].gameObject.SetActive(true);
        }
    }


    public void ResetLastWord()   // Button for deleting the char
    {
        if(selectedWordIndex.Count>0)
        {
        int index = selectedWordIndex[selectedWordIndex.Count - 1];
        optionWordArray[index].gameObject.SetActive(true);
        selectedWordIndex.RemoveAt(selectedWordIndex.Count - 1);     //Removing from the list 
        currentAnsIndex--;
        answerwordArray[currentAnsIndex].SetChar('-');
        }
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}

[System.Serializable]
public class QuestionData
{
    public Sprite QuestionImage;
    public string answer;
}


public enum GameStatus
{Playing,
    Next

}
