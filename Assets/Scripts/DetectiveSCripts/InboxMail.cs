using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InboxMail : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] Sprite humanInfo;
    [SerializeField] string emailInfo;
    [SerializeField] bool canAnswer;
    [SerializeField] string[] options;
    [SerializeField] Image hat;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button[] answerButtons;
    [SerializeField] TextMeshProUGUI answerText;
    [SerializeField] GameObject reply;
    [SerializeField] bool isReply;

    private bool hasAnswered { get; set;}
    private string pickedOption;
    public static GameObject selection = null;

    void Awake()
    {
        /*StreamReader reader = new("Assets/save/mailAnswers.data");
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            Debug.Log(line);
            var res = line.Split(' ');
            if (name.Equals(res[0]))
            {
                Debug.Log("found this object: " + res[0]);
                pickedOption = options[int.Parse(res[1])];
                hasAnswered = true;
                reply.SetActive(true);
                break;
            }
        }
        reader.Close();*/
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting " + name);
        if (reply != null && !hasAnswered) reply.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickableIcons.source.Play();
        hat.sprite = humanInfo;
        hat.color = Color.white;
        text.text = emailInfo;
        if (canAnswer)
        {
            if (!hasAnswered)
            {
                for (int i = 0; i < 2; i++)
                {
                    int number = i;
                    answerButtons[i].gameObject.SetActive(true);
                    answerButtons[i].onClick.AddListener(delegate { answerMail(number); });
                }
                answerText.gameObject.SetActive(false);
            }
            else
            {
                answerText.text = pickedOption;
                answerText.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (var button in answerButtons)
            {
                button.gameObject.SetActive(false);
            }
            answerText.gameObject.SetActive(false);
        }
    }

  private void answerMail(int number)
  {
        Debug.Log(number);
        hasAnswered = true;
        pickedOption = options[number];
        answerText.text = pickedOption;
        answerText.gameObject.SetActive(true);
        foreach (var button in answerButtons)
        {
            button.gameObject.SetActive(false);
        }
        if (reply != null)
        {
            reply.SetActive(true);
        }
        StreamWriter writer = new("Assets/save/mailAnswers.data", true);
        writer.WriteLine(name + " " + number);
        writer.Close();
    }

    public void MakeAnswer(int answer)
    {
        pickedOption = options[answer];
        hasAnswered = true;
        reply.SetActive(true);
        Debug.Log("has answered, showing: " + reply.name);
    }

    public bool IsReply()
    {
        return isReply;
    }

    public void HideReply()
    {
        if (canAnswer)
        {
            Debug.Log("Hiding reply: " + reply.name);
            reply.SetActive(false);
        }
    }

    public void OnDisable()
    {
        Debug.Log("Disabling " + name);
    }

    public Button GetFirstButton()
    {
        return answerButtons[0];
    }
}
