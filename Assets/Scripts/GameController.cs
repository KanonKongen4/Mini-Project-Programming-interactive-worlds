using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//:) This script is responsible for:
public class GameController : MonoBehaviour
{
    public int score;
    private TMP_Text deadText;
    bool dead;
    void Start()
    {
        deadText = FindObjectOfType<TMP_Text>();
        deadText.enabled = false;
    }


    void Update()
    {
        if (Input.anyKeyDown && dead)
        {
            RestartGame();
        }
    }

    public void DisplayDeadText()
    {
        Invoke(nameof(SetDeadTrue), 2);
        deadText.enabled = true;
        deadText.text = "Score: " + score;

    }
    private void SetDeadTrue()
    {
        dead = true;
        deadText.text = "Score: " + score + "     Press Any key to restart!";
    }
    public void RestartGame()
    {
        dead = false;
        score = 0;
        SceneManager.LoadScene(0);
    }
}
