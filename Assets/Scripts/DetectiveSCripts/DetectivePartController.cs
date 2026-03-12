using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectivePartController : MonoBehaviour
{

    public static DetectivePartController instance;
    private int _numberOfClues;
    [SerializeField] GameObject nextButton;
    [SerializeField] TMPro.TextMeshProUGUI journal;
    [SerializeField] GameObject casualties;
    public bool isReady = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    void Start()
    {
        _numberOfClues = FindObjectsByType<ClueObject>(FindObjectsSortMode.None).Length;
        if (PlayerPrefs.HasKey("JournalEntry"))
        {
            journal.text = PlayerPrefs.GetString("JournalEntry");
        }
        if (PlayerPrefs.HasKey("SurvivedLevel"))
        {
            casualties.SetActive(true);
        }
        else
        {
            casualties.SetActive(false);
        }
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
}
