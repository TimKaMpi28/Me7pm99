using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTimer : MonoBehaviour
{
    private float curTime = 0;
    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= 3f)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(1);
        }
    }
}
