using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

/// <summary>
/// The ContentManager is a manager that can be used in any scene with "content" (e.g. items, unlockables, etc).
/// It is to be used as a singleton.
/// </summary>
public class ContentManager : MonoBehaviour
{
    public static ContentManager instance;

    // PlayerData is just stored here for convenience, since I don't want to build an interface for it at the moment.
    public PlayerData playerData;

    /// <summary>
    /// contentToManage is all the content in the scene of this ContentManager. Add any content that has to be enabled/disabled.
    /// </summary>
    public List<Content> contentToManage;

    // Message windows
    [Header("Message Windows")]
    public GameObject messageWindowRestorePurchases;

    private void Awake()
    {
        // Set instance
        if (instance == null)
            instance = this;
        else if (instance != this) // This enforces the singleton pattern
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Do initial content update
        foreach (var c in contentToManage)
        {
            if (playerData.contentIdsUnlocked.Contains(c.contentId))
            {
                c.Unlock();
            }
            else
            {
                c.Lock();
            }
        }
    }

    public void UnlockNewContent(string contentId, bool displayMessage = true)
    {
        var content = contentToManage.Find(c => c.contentId == contentId);
        if (content != null)
        {
            content.Unlock(displayMessage);
        }
        else
        {
            Debug.Log("Content not found: " + contentId);
        }
    }

    public void RestoreContent()
    {
        UnlockNewContent(Content.ContentId_Subscription, displayMessage: false);

        messageWindowRestorePurchases.SetActive(true);
    }
}
