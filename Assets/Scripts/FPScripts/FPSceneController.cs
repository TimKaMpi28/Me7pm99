using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSceneController : MonoBehaviour
{

    public static FPSceneController instance;
    [SerializeField] public int enemiesCount;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int maxEnemy;
    [SerializeField] float time;
    [SerializeField] bool isTimed = true;
    [SerializeField] int nextSceneIndex;
    [SerializeField] GameObject bossEnemy;
    //public int curEnemy = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void Start()
    {
        //enemiesCount = FindObjectsByType<EnemyScript>(FindObjectsSortMode.None).Length;
    }

    public void Update()
    {
        if (isTimed)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                EndLevel();
            }
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

    public void EnemyKilled()
    {
        enemiesCount--;
        Debug.Log(enemiesCount);
        if (!isTimed && enemiesCount == 0)
        {
            bossEnemy.SetActive(true);
        }
        else if (isTimed)
        {
            BiggerText();
        }
    }

    public void EndMissionLevel()
    {
        SceneManager.LoadScene(0);
    }
}
