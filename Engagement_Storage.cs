using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engagement_Storage : MonoBehaviour
{
    public Game_Controller GameController;
    public Slider EngagementBar;
    public int EngagementLevels;


    // Start is called before the first frame update
    void Start()
    {
        EngagementBar.maxValue = EngagementLevels; //Sets the max value of the slider to match the Engagement levels, which at start is always full.
        EngagementBar.value = EngagementLevels; // Sets the current value of the slider to match.
        StartCoroutine (EngagementDrop()); //Begins the depletion over time of the Engagement stat.
    }
    IEnumerator EngagementDrop()
    {
        yield return new WaitForSeconds(1); //Waits one second then takes away 1 from Engagement.
        EngagementLevels -= 1;
        EngagementBar.value = EngagementLevels;

        if (EngagementLevels <= 0)
        {
            GameController.GameOverScreen(); // If Engagement hits 0, activate the GameOverScreen function on the GameController script.
        }

        StartCoroutine(EngagementDrop());
    }
}
