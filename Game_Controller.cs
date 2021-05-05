using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    public List<GameObject> EngagementPopup;
    public GameObject EngagementSpawnArea;
    public GameObject TimeLimitAlert;
    public GameObject GameOverObj;
    public GameObject GameWonObj;
    public GameObject EngagementInstruction;
    public Transform TimeLimitLocation;
    public Text TimeDisplay;

    float TimeRemaining;
    void Start()
    {
        StartCoroutine(PopupRespawn());
        Instantiate(TimeLimitAlert, TimeLimitLocation);
    }
    void Update()
    {
        TimeRemaining += Time.deltaTime;
        TimeDisplay.text = TimeRemaining.ToString("F0");

        if (TimeRemaining >= 90)
        {
            GameOverScreen();
        }
    }
    void SpawnEngagement()
    {
        EngagementInstruction.SetActive(true); // Pops up text that gives the player a hint to do the engagements.

        int RandomPopup;
        GameObject SpawnPopup;
        BoxCollider2D Collider = EngagementSpawnArea.GetComponent<BoxCollider2D>(); // MeshCollider makes the script recongize Collider as one, then gets the mesh collider from the Engagement spawn area.

        RandomPopup = Random.Range(0, EngagementPopup.Count); // This randomly chooses a popup between the first popup and the amount of popups there are.
        SpawnPopup = EngagementPopup[RandomPopup]; // Sets the SpawnPopup to be the GameObject list with a number, allowing Unity to go "you want this popup from this list".

        float ScreenX = Random.Range(Collider.bounds.min.x, Collider.bounds.max.x); // Randomly sets the X and Y ranges to be from the colliders starting X/Y point to the max of the end of the X/Y points.
        float ScreenY = Random.Range(Collider.bounds.min.y, Collider.bounds.max.y);
        Vector2 Position = new Vector2(ScreenX, ScreenY); // Combines the results of ScreenX and ScreenY into one set of cordinates.
        GameObject Popup = Instantiate(SpawnPopup, Position, SpawnPopup.transform.rotation); // Spawns the Random popup at the randomly generated cordinates with the popup's location and angle.
        Popup.transform.SetParent(GameObject.Find("Canvas").transform, true); // Sets the spawned object to be a parent of the canvas so it appears on the UI.

        StartCoroutine(PopupRespawn());
    }

    IEnumerator PopupRespawn() // Randomly restarts the SpawnEngagement function.
    {
        yield return new WaitForSeconds(Random.Range(5, 16));
        SpawnEngagement();
    }
    public void GameOverScreen()
    {
        GameOverObj.SetActive(true);
        Time.timeScale = 0f;
    }
    public void GameWin()
    {
        GameWonObj.SetActive(true);
        GameWonObj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Timed completed: " + TimeRemaining.ToString("F0") + " seconds!";
        Time.timeScale = 0f;
    }
    public void GameReset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
