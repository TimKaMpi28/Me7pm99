using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MMoController : MonoBehaviour
{
    public static MMoController instance;
    [SerializeField] GameObject[] cameras;
    [SerializeField] float waitTime;
    private float _timePassed = 0f;
    private bool _startCounting = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_startCounting)
        {
            _timePassed += Time.deltaTime;
            if (_timePassed >= waitTime)
            {
                BasicGameController.LoadScene(1);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void EndLevel()
    {
        var gun = cameras[0].GetComponentInParent<GunController>();
        foreach (var cam in cameras)
        {
            cam.GetComponent<Camera>().enabled = false;
        }
        cameras[2].GetComponent<Camera>().enabled = true;
        gun.Shoot();
        var journalEntry = PlayerPrefs.GetString("JournalEntry");
        journalEntry += "\n - Похоже речь тут шла совсем не про флаг. Трезвость разума... На чем ты сидел, Робин? Кто этот человек, что говорил с тобой?";
        PlayerPrefs.SetString("JournalEntry", journalEntry);
        PlayerPrefs.SetInt("MMOLevel", 1);
        _startCounting = true;
    }
}
