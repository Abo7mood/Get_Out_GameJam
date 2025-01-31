using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{
    public void LoadScene(int index) => SceneManager.LoadScene(index);

    public void Win() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    public void Menu() => SceneManager.LoadScene(0);

}
