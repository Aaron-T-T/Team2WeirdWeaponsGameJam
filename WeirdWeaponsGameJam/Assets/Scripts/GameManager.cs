using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool ButtonClicked =  false;
    //private bool quitButtonClicked = false;
    //private bool settingButtonClicked = false;
    private bool settingsOn = false;
    private bool howToOn = false;
    private string sceneToLoad;
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject HowToMenu;

    void Update()
    {
        if(ButtonClicked) // Checks to see if the button has been pressed 
        {
            StartCoroutine(AsyncLoadScene()); // runs IEnumerator AsyncLoad
            ButtonClicked = false; // sets button to false to say it isn't being pressed
        }

    }

    IEnumerator AsyncLoadScene() // Load the sceneToload asynchronously
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        
        while(!asyncLoad.isDone) // Wait until the scene is fully loaded
        {
            yield return null;
        }
    }

    public void setSceneToLoad(string sceneName)
    {
        
        sceneToLoad = sceneName; // sets the bariable scene to load with the name of the scene name
        ButtonClicked = true;

    }
    
    public void quitGame()
    {
        Application.Quit();
    }
    public void controlSettingsMenu()
    {
        settingsOn = !settingsOn;
        settingsMenu.SetActive(settingsOn);
        mainMenu.SetActive(!settingsOn);

    }
    public void controlHowToMenu()
    {
        howToOn = !howToOn;
        HowToMenu.SetActive(howToOn);
        mainMenu.SetActive(!howToOn);

    }
    public void loadChosenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
