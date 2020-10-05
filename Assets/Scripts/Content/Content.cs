using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Content monobehaviour lives on any "Content" type gameobject to manage that content.
/// It can be inherited from to add alternative behaviour for the Lock/Unlock functions.
/// </summary>
public class Content : MonoBehaviour
{
    // Generic content
    public static readonly string ContentId_Subscription = "subscriptionContent";

    public string contentId;

    [Header("Instance Management")]
    public GameObject greyoutBox;
    public GameObject unlockMessageWindow;

    public void Lock()
    {
        greyoutBox.SetActive(true);
    }

    public void Unlock(bool displayMessage = false)
    {
        greyoutBox.SetActive(false);
        if (displayMessage)
            unlockMessageWindow.SetActive(true);
    }
}
