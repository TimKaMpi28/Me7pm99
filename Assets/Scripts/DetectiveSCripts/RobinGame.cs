using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class RobinGame : ClickableIcons
{
    [SerializeField] public int gameSceneNum;
    [SerializeField] TextMeshProUGUI journal;
    public override void ClickMethod()
    {
        PlayerPrefs.SetString("JournalEntry", journal.text);
        SceneManager.LoadScene(gameSceneNum);
    }
}
