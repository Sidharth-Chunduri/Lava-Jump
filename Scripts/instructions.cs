using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructions : MonoBehaviour
{
    //game objects under the canvas
    public GameObject Instructions;
    public GameObject Text1;
    public GameObject Text2;
    public GameObject NextButton;
    public GameObject MainMenu;
    
    //back button to go back to main menu
    public void Back(){
        Instructions.SetActive(false);
        MainMenu.SetActive(true);
    }

    //moves to the next screen of instructions
    public void Next(){
        Text1.SetActive(false);
        Text2.SetActive(true);
        NextButton.SetActive(false);
    }
}
