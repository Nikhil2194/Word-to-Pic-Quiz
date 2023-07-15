using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject GameTitleObject, LevelsObject;



    public void TitleTextStartButton()
    {
        GameTitleObject.SetActive(false);
        LevelsObject.SetActive(true);
    }

    #region LevelsButtons

    public void AnimalSceneButton()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void FruitSceneButton()
    {
        SceneManager.LoadScene("Fruits");
    }

    public void SportSceneButton()
    {
        SceneManager.LoadScene("Sports");
    }

    #endregion


}
