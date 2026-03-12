using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDialogueScript : DialogueScript
{
    [SerializeField] int nextSceneIndex;
    private bool enabledInput = false;



    protected override void Start()
    {
        base.Start();
        TypeLine();
        StartCoroutine(DisableInput());
    }

    protected override void Update()
    {
        if (enabledInput) base.Update();
    }
    public override void EndDialogue()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(nextSceneIndex);
    }

    IEnumerator DisableInput()
    {
        yield return new WaitForSeconds(0.1f);
        enabledInput = true;
    }
}
