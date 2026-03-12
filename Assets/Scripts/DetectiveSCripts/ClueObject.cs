using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class ClueObject : MonoBehaviour
{
    [SerializeField] string clue_text;
    [SerializeField] ClickableIcons icon;
    bool gotIt = false;
    [SerializeField] TMPro.TextMeshProUGUI clue_journal;
    [SerializeField] GameObject popUp;

    private void Awake()
    {
        icon = GetComponent<ClickableIcons>();
        popUp.SetActive(false);
    }

    public void getClue()
    {
        if (icon.isWindowOpen() && !gotIt)
        {
            WriteInJournal();
            popUp.SetActive(true);
            DetectivePartController.instance.FindClue();
            if (!name.Equals("Internet"))
            {
                StreamWriter writer = new("Assets/save/foundClues.data", true);
                writer.Write(name + " ");
                writer.Close();
            }
        }
    }
    public void GetIt()
    {
        gotIt = true;
    }
    public void WriteInJournal()
    {
        Debug.Log(clue_text);
        clue_journal.text += "\n- " + clue_text;
        gotIt = true;
    }

    public void SetClueText(string text)
    {
        clue_text = text;
    }
}
