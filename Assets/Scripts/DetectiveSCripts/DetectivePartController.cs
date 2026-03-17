using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectivePartController : MonoBehaviour
{

    public static DetectivePartController instance;
    private int _numberOfClues;
    [SerializeField] GameObject nextButton;
    [SerializeField] TMPro.TextMeshProUGUI journal;
    [SerializeField] GameObject casualties;
    [SerializeField] Sprite[] newsArticles;
    [SerializeField] RobinGame game;
    [SerializeField] MailCollection mailCollection;
    public bool isReady = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    void Start()
    {
        StreamReader configReader = new("Assets/Configs/ClueConfig.json");
        string config = configReader.ReadToEnd();
        configReader.Close();
        ConfigData data = JsonConvert.DeserializeObject<ConfigData>(config);
        List<string> availableClues = data.defaultLevel.desktop;
        StreamReader saveReader = new("Assets/save/foundClues.data");
        List<string> foundClues = new(saveReader.ReadToEnd().Split(' '));
        List<string> emails = data.defaultLevel.emails;
        saveReader.Close();
        bool addSecretClue = false;
        if (PlayerPrefs.HasKey("SurvivedLevel"))
        {
            casualties.SetActive(true);
            availableClues.AddRange(data.firstLevel.desktop);
            var internet = GameObject.Find("Internet");
            internet.GetComponent<ClickableIcons>().SetFile(newsArticles[0]);
            internet.GetComponent<ClueObject>().SetClueText(data.firstLevel.news);
            game.gameSceneNum = 5;
        }
        else
        {
            casualties.SetActive(false);
        }
        if (PlayerPrefs.HasKey("MMOLevel"))
        {
            var internet = GameObject.Find("Internet");
            internet.GetComponent<ClueObject>().GetIt();
            emails.AddRange(data.secondLevel.emails);
            game.gameSceneNum = 6;
            addSecretClue = true;
        }
        ClueObject[] clues = FindObjectsByType<ClueObject>(FindObjectsSortMode.None);
        _numberOfClues = clues.Length;
        foreach (var clue in clues)
        {
            if (clue.name.Equals("Internet"))
            {
                if (clue.hasGotten()) _numberOfClues--;
                continue;
            }

            if (!availableClues.Contains(clue.name))
            {
                clue.gameObject.SetActive(false);
                _numberOfClues--;
            }

            if (foundClues.Contains(clue.name))
            {
                clue.GetIt();
                _numberOfClues--;
            }
        }
        mailCollection.SetEmails(emails);
        if (PlayerPrefs.HasKey("JournalEntry"))
        {
            journal.text = PlayerPrefs.GetString("JournalEntry");
        }
        if (addSecretClue) _numberOfClues++;
        Debug.Log("Number of Clues: " +  _numberOfClues);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindClue()
    {
        _numberOfClues--;
        if ( _numberOfClues <= 0)
        {
            isReady = true;
        }
    }

    public void ClearDesktop()
    {
        foreach (var icon in GameObject.FindGameObjectsWithTag("Icon"))
        {
            icon.SetActive(false);
        }
        nextButton.SetActive(true); 
    }

    public void AddClue()
    {
        _numberOfClues++;
    }
    private class ConfigData
    {
        public levelData defaultLevel;
        public levelData firstLevel;
        public levelData secondLevel;
    }

    private class levelData
    {
        public List<string> desktop;
        public string news;
        public List<string> emails;
    }
}


