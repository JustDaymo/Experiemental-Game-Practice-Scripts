using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engagement_Popup : MonoBehaviour
{
    public Button PopupButton;

    AudioSource PlaySoundPopup;
    public AudioClip ActivitySpawn;
    public AudioClip ActivityTimeout;
    public AudioClip ActivityComplete;

    GameObject EngagementStorage;

    int PopupTimer = 3;

    private void Start()
    {
        EngagementStorage = GameObject.Find("Engagement_Checks"); // Sets it up so it can interact with the Engagement script.
        StartCoroutine(PopupCoundown());
        PlaySoundPopup = GetComponent<AudioSource>();
        PlaySoundPopup.PlayOneShot(ActivitySpawn);
    }
    IEnumerator PopupCoundown()
    {
        yield return new WaitForSeconds(1);
        PopupTimer -= 1;
        StartCoroutine(PopupCoundown());

        if (PopupTimer == 0)
        {
            PlaySoundPopup.PlayOneShot(ActivityTimeout);
            EngagementStorage.GetComponent<Engagement_Storage>().EngagementLevels -= 10; // If the player fails to hit the button in time, remove engagement.
            Destroy(gameObject, 0.5f);
        }
    }

    public void IncreaseEngagement()
    {
        PlaySoundPopup.PlayOneShot(ActivityComplete);
        EngagementStorage.GetComponent<Engagement_Storage>().EngagementLevels += 5; // Hitting the button adds engagement.
        Destroy(gameObject, 0.5f);
    }
}
