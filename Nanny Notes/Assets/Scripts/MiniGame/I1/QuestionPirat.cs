using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPirat : MonoBehaviour
{
    public Questions[] questions;
    public GameObject OkOrNotAnswer1, OkOrNotAnswer2, OkOrNotAnswer3;
    public Text questionsText, answerText1, answerText2, answerText3;
    public GameObject BlockClick;
    public Animation animationGame;
    public GameObject ObjectOff;
    public GameObject ObjectOn;

    public AudioSource PirateGameOver, PiratListQuestOk, PiratListQuestWrong;

    int numberQuestion = 0;
    int countRightAnswer = 0;

    [System.Serializable]
    public class Questions
    {
        public string question;
        public string answer1, answer2, answer3;
        public int rightAnswer;
    }

    void PrintQuestion()
    {
        numberQuestion++;
        answerText1.color = new Color(0, 0, 0);
        answerText2.color = new Color(0, 0, 0);
        answerText3.color = new Color(0, 0, 0);
        questionsText.text = questions[numberQuestion].question;
        answerText1.text = questions[numberQuestion].answer1;
        answerText2.text = questions[numberQuestion].answer2;
        answerText3.text = questions[numberQuestion].answer3;
    }

    public void CheckAnswer(int numberAnswer)
    {
        BlockClick.SetActive(true);
        StartCoroutine("Answer", numberAnswer);
    }

    IEnumerator Answer(int numberAnswer)
    {
        if (numberAnswer == questions[numberQuestion].rightAnswer)
        {
            switch (numberQuestion)
            {
                case 0:
                    for (float i = 0; i < 1; i += 0.05f)
                    {
                        OkOrNotAnswer1.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, i);
                        OkOrNotAnswer2.GetComponent<Image>().color = new Color(255, 255, 255, i);
                        if (numberAnswer == 1)
                            answerText1.color = new Color(0, answerText1.color.g + 0.035f, 0, 1);
                        if (numberAnswer == 2)
                            answerText2.color = new Color(0, answerText2.color.g + 0.035f, 0, 1);
                        if (numberAnswer == 3)
                            answerText3.color = new Color(0, answerText3.color.g + 0.035f, 0, 1);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    break;
                case 1:
                    for (float i = 0; i < 1; i += 0.05f)
                    {
                        OkOrNotAnswer2.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, i);
                        OkOrNotAnswer3.GetComponent<Image>().color = new Color(255, 255, 255, i);
                        if (numberAnswer == 1)
                            answerText1.color = new Color(0, answerText1.color.g + 0.035f, 0, 1);
                        if (numberAnswer == 2)
                            answerText2.color = new Color(0, answerText2.color.g + 0.035f, 0, 1);
                        if (numberAnswer == 3)
                            answerText3.color = new Color(0, answerText3.color.g + 0.035f, 0, 1);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    break;
                case 2:
                    for (float i = 0; i < 1; i += 0.05f)
                    {
                        OkOrNotAnswer3.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, i);
                        if (numberAnswer == 1)
                            answerText1.color = new Color(0, answerText1.color.g + 0.035f, 0, 1);
                        if (numberAnswer == 2)
                            answerText2.color = new Color(0, answerText2.color.g + 0.035f, 0, 1);
                        if (numberAnswer == 3)
                            answerText3.color = new Color(0, answerText3.color.g + 0.035f, 0, 1);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
            countRightAnswer++;
            PiratListQuestOk.Play();
        }
        else
        {
            PiratListQuestWrong.Play();
            switch (numberQuestion)
            {
                case 0:
                    for (float i = 0; i < 1; i += 0.05f)
                    {
                        OkOrNotAnswer1.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, i);
                        OkOrNotAnswer2.GetComponent<Image>().color = new Color(255, 255, 255, i);
                        if (numberAnswer == 1)
                            answerText1.color = new Color(answerText1.color.r + 0.035f, 0, 0, 1);
                        if (numberAnswer == 2)
                            answerText2.color = new Color(answerText2.color.r + 0.035f, 0, 0, 1);
                        if (numberAnswer == 3)
                            answerText3.color = new Color(answerText3.color.r + 0.035f, 0, 0, 1);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    break;
                case 1:
                    for (float i = 0; i < 1; i += 0.05f)
                    {
                        OkOrNotAnswer2.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, i);
                        OkOrNotAnswer3.GetComponent<Image>().color = new Color(255, 255, 255, i);
                        if (numberAnswer == 1)
                            answerText1.color = new Color(answerText1.color.r + 0.035f, 0, 0, 1);
                        if (numberAnswer == 2)
                            answerText2.color = new Color(answerText2.color.r + 0.035f, 0, 0, 1);
                        if (numberAnswer == 3)
                            answerText3.color = new Color(answerText3.color.r + 0.035f, 0, 0, 1);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    break;
                case 2:
                    for (float i = 0; i < 1; i += 0.05f)
                    {
                        OkOrNotAnswer3.transform.GetChild(1).GetComponent<Image>().color = new Color(255, 255, 255, i);
                        if (numberAnswer == 1)
                            answerText1.color = new Color(answerText1.color.r + 0.035f, 0, 0, 1);
                        if (numberAnswer == 2)
                            answerText2.color = new Color(answerText2.color.r + 0.035f, 0, 0, 1);
                        if (numberAnswer == 3)
                            answerText3.color = new Color(answerText3.color.r + 0.035f, 0, 0, 1);
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
        answerText1.GetComponent<Button>().enabled = false;
        answerText1.GetComponent<Button>().enabled = true;
        answerText2.GetComponent<Button>().enabled = false;
        answerText2.GetComponent<Button>().enabled = true;
        answerText3.GetComponent<Button>().enabled = false;
        answerText3.GetComponent<Button>().enabled = true;

        if (numberQuestion == 2)
        {
            if (countRightAnswer == 3)
            {
                animationGame.Play("GameOver");
                ObjectOff.SetActive(false);
                ObjectOn.SetActive(true);
                C_PlayerPrefs.mc_this.SaveSceneState();
            }
            else
            {
                countRightAnswer = 0;
                numberQuestion = -1;
                System.Random rand = new System.Random();

                for (int i = questions.Length - 1; i >= 1; i--)
                {
                    int j = rand.Next(i + 1);

                    Questions tmp = questions[j];
                    questions[j] = questions[i];
                    questions[i] = tmp;
                }
                animationGame.Play("TryAgain");
                PirateGameOver.Play();
            }
        }
        else
        {
           
            for (float i = 1; i > 0; i -= 0.05f)
            {
                questionsText.color = new Color(0, 0, 0, i);
                answerText1.color = new Color(answerText1.color.r, answerText1.color.g, 0, i);
                answerText2.color = new Color(answerText2.color.r, answerText2.color.g, 0, i);
                answerText3.color = new Color(answerText3.color.r, answerText3.color.g, 0, i);
                yield return new WaitForSeconds(0.01f);
            }

            PrintQuestion();
            for (float i = 0; i < 1; i += 0.05f)
            {
                questionsText.color = new Color(0, 0, 0, i);
                answerText1.color = new Color(0, 0, 0, i);
                answerText2.color = new Color(0, 0, 0, i);
                answerText3.color = new Color(0, 0, 0, i);

                yield return new WaitForSeconds(0.01f);
            }
        }
        BlockClick.SetActive(false);
    }
}
