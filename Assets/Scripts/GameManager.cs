using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;


    #region Unity_Functions
    private void Awake()
    {
        //Check if this object does not already exist in scene
        if (instance == null)
        {
            instance = this;
        }

        //else if instance already exists and is not this object
        else if (instance != this)
        {
            //Get rid of this object so that there is only one singleton gamemanager
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Scene_Transitions

    public void StartGame() 
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void WinGame() {
        SceneManager.LoadScene("WinScene");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("LoseScene");
    }

    public void GoToMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
    }
    #endregion

}
