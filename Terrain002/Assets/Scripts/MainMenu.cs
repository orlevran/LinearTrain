using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public Animator animator;
    public Slider slider;
    public int numOfPassengers;

    public void LoadProgram()
    {
        StartCoroutine(LoadNextScene());
    }


    IEnumerator LoadNextScene()
    {
        animator.SetTrigger("GO");
        numOfPassengers = (int)slider.value;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
    void Awake()
    {
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
        }
        else if (instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }
}
