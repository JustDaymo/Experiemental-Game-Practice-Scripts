using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Work_Text : MonoBehaviour
{
    public Work_Storage WorkStorage = null; // How this script talks to the Work_Storage scipt and sets the current state of it to null.
    public Text WordCurrentMax; // Refers to the text that will be used to display the current compelted and max amount of words.
    public Text WordDisplay = null; // What word gets display on the Work side of the UI.
    public GameObject WrongWordDisplay; // An unactive text box that says the player typed a thing wrong.
    public GameObject GameController;   // How this script talks to the Game_Controller script.

    public AudioClip TypingSound; // The three sounds this script uses.
    public AudioClip WrongSound;
    public AudioClip WordCompleteSound;

    AudioSource PlaySound;

    int WordCurrent;    // Value for how many words have been completed.
    int WrongWordTimer = 10;    // The penalty value for typing the wrong letter.

    bool WrongLetter;

    private string NotTyped = string.Empty; // The letters of word that haven't been typed.
    private string TheWord = string.Empty;  // The word to type.

    private void Start()    // Start is called before the first frame update
    {
        SetTheWord();   // Begins the SetTheWord function on start.
        WordCurrentMax.text = WordCurrent + "/" + WorkStorage.FullWordList.Count; // Sets the text to show the current amount of typed words and how many are in the list.
        PlaySound = GetComponent<AudioSource>();    // Sets PlaySound to get it's audio compotent ready so it knows it plays sounds.
    }
    private void SetTheWord()
    {
        TheWord = WorkStorage.GetWord(); // Sets TheWord by running the GetWord function in the Work_Storage script.
        SetNotTyped(TheWord);   // Sends the TheWord string to the SetNotTyped function.
    }
    private void SetNotTyped(string NewString)  // Set to accept a string and call it NewString.
    {
        NotTyped = NewString;   // Makes the currently NotTyped into the new string.
        WordDisplay.text = NotTyped;    // And then changes the text on display to what is set on NotTyped.
    }

    private void Update()     // Update is called once per frame
    {
        CheckInput();   // Runs the CheckInput function every frame.
    }
    private void CheckInput()
    {
        if (WrongLetter == true)    // If the bool WrongLetter is true, end the function early.
        {
            return;
        }

        if (Input.anyKeyDown)   // If any key is pressed:
        {
            string LetterPressed = Input.inputString;   // Holds the key placed in a string.

            if (LetterPressed.Length == 1)  // Checks that only one key was presed, if so, runs CheckLetter with the key as the input.
                CheckLetter(LetterPressed);
        }
    }
    private void CheckLetter(string PressedLetter) // CheckLetter runs with the key that was pressed as the input.
    {
        if (CorrectLetter(PressedLetter))   // Asks the CorrectLetter function if this letter is correct, if so, it continues the script.
        {
            RemoveLetter(); // Runs the RemoveLetter fucntion.
            PlaySound.PlayOneShot(TypingSound); // Play the typing sound.

            if (CheckWordComplete())    // Once the remove letter function is complete, checks to see if the word has been completed.
            {
                PlaySound.PlayOneShot(WordCompleteSound);   // Play the word complete sound.
                WordCurrent += 1; // Adds one to the current word counter.
                WordCurrentMax.text = WordCurrent + "/" + WorkStorage.FullWordList.Count; // Sets the word counter text to the current word counter/the amount of words in the full word list.
                SetTheWord();   // Set a new word.

                if (WordCurrent == WorkStorage.FullWordList.Count)  // If the list is complete, ask the Game_Controller script to run the GameWin function.
                {
                    GameController.GetComponent<Game_Controller>().GameWin();
                }
            }

        }
        else // If the letter is not correct, it is wrong.
        {
            WrongLetter = true;
            PlaySound.PlayOneShot(WrongSound);
            StartCoroutine(WrongLetterDisable()); // Since WrongLetterDisable needs to wait, it needs to be started as a Couroutine and not a simple function.
        }
    }
    private bool CorrectLetter(string Letter)
    {
        return NotTyped.IndexOf(Letter) == 0; // Looks at the remaining letters to type and checks if the input matches the first letter.
    }

    IEnumerator WrongLetterDisable()
    {
        WrongWordDisplay.SetActive(true); // Display the inactive WrongWord text.
        WrongWordDisplay.GetComponent<Text>().text = "You got that wrong! Think about your mistake for: " + WrongWordTimer + "s"; // Gets the text and sets it with the timer.

        if (WrongWordTimer <= 0)    // Once time is up, resets everything back to normal play.
        {
            WrongWordDisplay.SetActive(false);
            WrongLetter = false;
            WrongWordTimer = 10;    // Neesd to be set back to 10 as it's 0, ready for next time the player gets the letter wrong.
            yield break;    // Exits the coroutine early.
        }
        yield return new WaitForSeconds(1); // After one second, reduces the timer by 1 and restarts the coroutine to update the text and time again.
        WrongWordTimer -= 1;
        StartCoroutine(WrongLetterDisable());
    }
    private void RemoveLetter()
    {
        string UpdatedWord = NotTyped.Remove(0, 1); // Looks at the first letter currently in NotTpyed and removes that one letter.
        SetNotTyped(UpdatedWord);
    }
    private bool CheckWordComplete()
    {
        return NotTyped.Length == 0; // Checks if the remaining amount of letter typed is 0, returns true if so.
    }
}