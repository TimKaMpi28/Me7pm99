using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MailCollection : MonoBehaviour
{
    List<string> readyEmails = new List<string>();
    Dictionary<string, int> answeredEmails = new Dictionary<string, int>();
    private void Awake()
    {
        var children = GetComponentsInChildren<InboxMail>();
        StreamReader reader = new("Assets/save/mailAnswers.data");
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var res = line.Split(' ');
            answeredEmails.Add(res[0], int.Parse(res[1]));
        }
        foreach (var child in children)
        {
            Debug.Log(child.name);
            if (readyEmails.Contains(child.name))
            {
                child.gameObject.SetActive(true);
                if (answeredEmails.ContainsKey(child.name))
                {
                    child.MakeAnswer(answeredEmails[child.name]);
                }
                else child.HideReply();
            }
            else if (!child.IsReply())
            {
                Debug.Log("not showing" + child.name);
                child.gameObject.SetActive(false);
            }
        }
       reader.Close();
    }

    public void SetEmails(List<string> emails)
    {
        readyEmails = emails;
    }
}
