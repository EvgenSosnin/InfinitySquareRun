using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{

    public Text scoreText;
    public Text florDetectionText;
    public Text dangerBlockChanceText;
    public Text blockSpeed;
    public Text start_end_Text;
    public Text bestScore;


    void Start()
    {
        InputEvents.WORLD_ROTATE_EVENT.AddListener(OnWorldRotate);
        OnWorldRotate();
        StartCoroutine(StartUIMassages());
        bestScore.text = "Best score : " +  PlayerPrefs.GetInt("BestScore", 0).ToString();
    }

    void OnDestroy()
    {
        InputEvents.WORLD_ROTATE_EVENT.RemoveListener(OnWorldRotate);
    }

    public void OnWorldRotate()
    {
        florDetectionText.text = Managers.WorldRotation.getRotation().ToString();

    }

    void Update()
    {
        scoreText.text = "Score : " + ( Managers.GameManager.GameDifficult - Managers.BlockManager.maxBlocksInArray) ;
        dangerBlockChanceText.text = "Danger platform chance : " +  Managers.BlockManager.dangerBlockChance;
        blockSpeed.text = "Platform speed : " + Managers.BlockManager.blockSpeed;
    }


    private IEnumerator StartUIMassages()
    {
        float freez = 1f;
        int inSeconds = 3;
        while (inSeconds > 0)
        {
            if(inSeconds == 0)
            {
                start_end_Text.text = "GO";
            }
            else
            {
                start_end_Text.text = inSeconds.ToString();
            }
            --inSeconds;
            yield return new WaitForSeconds(freez);
        }

        start_end_Text.text = "";
    }
}

