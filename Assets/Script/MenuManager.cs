using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    string url = "https://neplay-games.itch.io";
  
  
    public void StartGame(int i){

       SceneManager.LoadScene(i);
        
   }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Doesnot actually quit the game
    //Load a new url to make it look like the games ends
    public void QuitGame()
    {
        Application.OpenURL(url);
    }
}