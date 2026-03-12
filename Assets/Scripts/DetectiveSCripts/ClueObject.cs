using System.Collections;
using System.Collections.Generic;
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
            Debug.Log(clue_text);
            clue_journal.text += "\n- " + clue_text;
            gotIt = true;
            popUp.SetActive(true);
            DetectivePartController.instance.FindClue();
        }
    }
}
