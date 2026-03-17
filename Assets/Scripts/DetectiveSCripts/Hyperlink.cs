using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hyperlink : MonoBehaviour, IPointerDownHandler
{
    Button button;
    [SerializeField] Sprite newsArticle;
    [SerializeField] string clueText;
    private ClueObject internetClue;
    private ClickableIcons internetIcon;

    void Awake()
    {
        var internet = GameObject.Find("Internet");
        internetClue = internet.GetComponent<ClueObject>();
        internetIcon = internet.GetComponent<ClickableIcons>();
        internetClue.UngetIt();
        button = GetComponent<InboxMail>().GetFirstButton();
        button.onClick.AddListener(delegate { OpenNews(); });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        button.gameObject.SetActive(true);
    }

    private void OpenNews()
    {
        internetIcon.SetFile(newsArticle);
        internetClue.SetClueText(clueText);
        internetIcon.ClickMethod();
    }
}
