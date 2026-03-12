using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ClickableIcons : MonoBehaviour, IPointerClickHandler
{
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    [SerializeField] private Image blue;
    [SerializeField] private Sprite file;
    [SerializeField] private Vector2 windowSize;
    [SerializeField] private GameObject window;
    public static AudioSource source = null;
    bool windowOpen;
    
    void Start()
    {
        //window = GameObject.FindWithTag("Window");
        window.SetActive(false);
        if (source == null) source = GetComponentInParent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData data)
    {
        clicked++;
        source.Play();
        if (clicked == 1)
        {
            clicktime = Time.time;
           blue.gameObject.SetActive(true);
           blue.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
            window.SetActive(false);
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            Debug.Log("Double CLick: " + this.GetComponent<RectTransform>().name);
            blue.gameObject.SetActive(false);
            ClickMethod();

        }
        else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;

    }

    public virtual void ClickMethod()
    {
        blue.gameObject.SetActive(false);
        window.GetComponent<Image>().sprite = file;
        window.GetComponent<RectTransform>().sizeDelta = windowSize;
        window.SetActive(true);
        windowOpen = true;
        if (TryGetComponent<ClueObject>(out var clueObject))
        {
            clueObject.getClue();
        }
        else if (gameObject.name == "Clues" && DetectivePartController.instance.isReady)
        {
            DetectivePartController.instance.ClearDesktop();
        }
    }

    public void CloseWindow()
    {
        source.Play();
        window.SetActive(false);
        windowOpen = false;
    }

    public bool isWindowOpen()
    {
        return windowOpen;
    }

    public void SetFile(Sprite file)
    {
        this.file = file;
    }
}
