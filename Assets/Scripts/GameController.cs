using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject platform;
    public GameObject crystal;
    public GameObject goStartText;
    public Text txtScore;
    public Text txtStartText;
    public float speed = 3.0f;
    public int genLength = 100;
    public int maxPlatformLength = 5;

    private Vector3 moveDir;
    private Vector3 currGenPos = new Vector3(0,0,0);
    private int currDir = 0;
    private int t = 1;
    private int d = 1;
    private int nextPlatforms = 0;
    private bool generated = false;

    public void NewGame()
    {
        GlobalData.score = 0;
        GlobalData.generate = true;
        GlobalData.touchedPlatforms = 0;
        SceneManager.LoadScene(0);
    }
    private void GameOver()
    {
        Time.timeScale = 0;
        goStartText.SetActive(true);
    }
    private void WayGenerate()
    {
        GameObject goTmp;
        for(int i=1; i<=genLength; i++)
        {
            if(t==0)
            {
                t=Random.Range(1, maxPlatformLength);
                d *= -1;
                i--;
            }
            else
            {
                goTmp = Instantiate(platform);              /// create platform
                goTmp.transform.position = currGenPos;
                
                if(Random.Range(1, 5) == 3)                 /// 20% create crystal
                {
                    goTmp = Instantiate(crystal);
                    goTmp.transform.position = currGenPos + new Vector3(0,1,0);
                }
                
                if(d > 0)
                {
                    currGenPos += new Vector3(1,0,0);
                }
                else
                {
                    currGenPos += new Vector3(0,0,1);
                }
                t--;
            }
        }
        nextPlatforms += genLength/2;
    }

    void Update()
    {
        txtScore.text = GlobalData.score.ToString();

        if(nextPlatforms <= GlobalData.touchedPlatforms)
        {
            GlobalData.generate = true;
        }

        if(GlobalData.generate)
        {
            GlobalData.generate = false;
            WayGenerate();
        }

        if(player.transform.position.y < 0)
            GameOver();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(currDir == 0)
            {
                currDir = 1;
                txtStartText.enabled = false;
                Time.timeScale = 1;
            }
            else
            currDir *= -1;
        }

        switch(currDir)
        {
            case 1: moveDir = new Vector3(1,0,0);
            break;
            case -1: moveDir = new Vector3(0,0,1);
            break;
            default: moveDir = new Vector3(0,0,0);
            break;
        }

        player.transform.Translate(moveDir.normalized * Time.deltaTime * speed);
    }
}
