using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class WinLevel : MonoBehaviour
{
    public ObjectiveMarker winObjective;
    public GameObject winPanel;
    public bool winYet = false, checkWin = true;
    NavMeshSurface surface;

    private void Start()
    {
        surface = FindObjectOfType<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    void Update()
    {
        
        if(winYet == winObjective.objectiveMet && checkWin == true)
        {
            
        }
        else if( winYet != winObjective.objectiveMet && checkWin == true)
        {
            checkWin = false;
            DisplayWinAndNext();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void DisplayWinAndNext()
    {
        winPanel.gameObject.SetActive(true);
    }

    public void LoadNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.loadedSceneCount +1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else { SceneManager.LoadScene(0); }
    }
}
