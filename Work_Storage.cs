using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Work_Storage : MonoBehaviour
{
    public List<string> FullWordList = new List<string>()  // The entire list of words.
    {
        "According", "to", "all", "known", "laws", "of", "aviation,", "there", "is", "no",
        "way", "a", "bee", "should", "be", "able", "to", "fly.", "Its", "wings", "are", "too", 
        "small", "to", "get", "its", "fat", "little", "body", "off", "the", "ground.", "The", "bee,",
        "of", "course,", "flies", "anyway", "because", "bees", "don't", "care", "what", "humans",
        "think", "is", "impossible."
    };
    
    private List<string> CurrentWordList = new List<string>();

    private void Awake()
    {
        CurrentWordList.AddRange(FullWordList); // Takes the full word list and applies it to the current list.
    }

    public string GetWord()
    {
        string NewWord = string.Empty;  // Makes an empty string.
        
        if(CurrentWordList.Count != 0)  // If the amount of words left is not zero.
        {
            NewWord = CurrentWordList.First();
            CurrentWordList.Remove(NewWord);    // Gets the first word of the remaining words while also removing it.
        }
        return NewWord;
    }
}
