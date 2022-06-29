using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    //stores scene values
    public int multiplayerscene;
    public int Level1scene;
    public int Level2scene;
    public int Level3scene;
    public int LevelBossscene;
    //declares settings for which levels have and have not been cleared
    public int level1Cleared;
    public int level2Cleared;
    public int level3Cleared;
    //declares green game obejcts to be shown when a level is accessible
    public GameObject playable1;
    public GameObject playable2;
    public GameObject playable3;
    public GameObject playableBoss;
    
    //update runs every frame
    void Update(){
        //checks which levels have been cleared
        level1Cleared = PlayerPrefs.GetInt("Level1Cleared");
        level2Cleared = PlayerPrefs.GetInt("Level2Cleared");
        level3Cleared = PlayerPrefs.GetInt("Level3Cleared");

        //displays teh green playable indicator for each level is accesible
        if (level1Cleared == 1){
            playable2.SetActive(true);
        }

        if (level2Cleared == 1){
            playable3.SetActive(true);
        }
        
        if (level3Cleared == 1){
            playableBoss.SetActive(true);
        }
    }

    //Loads multiplayer scene
    public void Multiplayer()
    {
        SceneManager.LoadScene(multiplayerscene);
    }

    //loads level1
    public void Level1()
    {
        SceneManager.LoadScene(Level1scene);
    }
    
    //loads level2
    public void Level2()
    {
        if(level1Cleared == 1){
        SceneManager.LoadScene(Level2scene);
        }
    }
    
    //loads level3
    public void Level3()
    {
        if(level2Cleared == 1){
        SceneManager.LoadScene(Level3scene);
        }
    }

    //loads boss level
    public void BossLevel()
    {
        if(level3Cleared == 1){
        SceneManager.LoadScene(LevelBossscene);
        }
    }

}
