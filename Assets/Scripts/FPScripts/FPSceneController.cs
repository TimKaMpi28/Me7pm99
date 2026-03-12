using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSceneController : MonoBehaviour
{

    public static FPSceneController instance;
    public int enemiesCount { get; set; }
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int maxEnemy;
    [SerializeField] float time;
    [SerializeField] int nextSceneIndex;
    //public int curEnemy = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void Start()
    {
        //enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesCount = 0;
    }

    public void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            EndLevel();
        }
    }
    public void EndLevel()
    {
        Debug.Log("endingShooterLvl");
        var journalEntry = PlayerPrefs.GetString("JournalEntry");
        journalEntry += "\n - принцесса лиза? Похоже, он конкретно тронулся по этой девчонке.";
        PlayerPrefs.SetString("JournalEntry", journalEntry);
        PlayerPrefs.SetInt("SurvivedLevel", 1);
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void BiggerText()
    {
        text.fontSize *= 1.5f;
    }

    public bool CanSpawn()
    {
        if (enemiesCount >= maxEnemy) return false;
        return true;
    }
}
