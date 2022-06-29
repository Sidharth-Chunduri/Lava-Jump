using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //delcares game objects within the canvas
    public GameObject mainMenu;
    public GameObject levelSelect;
    public GameObject instructions;

    //sends teh user to the level select screen
    public void Play()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    //Loads the instructions screen
    public void Instructions(){
        mainMenu.SetActive(false);
        instructions.SetActive(true);
    }

    //exits the game
    public void Quit()
    {
        Application.Quit();
    }

    //deletes all save data 
    public void Delete(){
        PlayerPrefs.DeleteAll();
    }
}
