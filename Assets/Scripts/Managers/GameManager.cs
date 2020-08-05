
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float GameDifficult { get; set; }
    public float playerSpeed = 15f;



    public Transform player;
    public Transform camera;


    void Start()
    {
        Managers.WorldRotation.ChangeRotation(Managers.WorldRotation.getRotation());
    }

    void Awake()
    {
        GameDifficult = 0f;

    }



    // Update is called once per frame
    void Update()
    {

        Vector3 playerPosition = player.position;
        if (playerPosition.x > 50f || playerPosition.y > 50f || playerPosition.x < -10f || playerPosition.y < -10f)
        {
            RestartScene();
        }
    }

    public void FreezGame(float freezSeconds,float cameraTeleportAnimationSeconds)
    {
        camera.GetComponent<followCamera>().TeleportAnimation(cameraTeleportAnimationSeconds);
        Managers.BlockManager.FreezBlockVelocity(freezSeconds);
        player.GetComponent<playerMovement>().FreezPlayerkVelocity_Gravity(freezSeconds);
        

    }

    public void RestartScene()
    {
        SaveScore();
        ReplayAudio();
        SceneManager.LoadScene("LoadScene");
    }

    public void SaveScore()
    {
        if(PlayerPrefs.GetInt("BestScore",0) < (int)GameDifficult)
        {
            PlayerPrefs.SetInt("BestScore", (int)GameDifficult);
        }
    }

    public void ReplayAudio()
    {
       GameObject audio =  GameObject.Find("Audio");
        if (audio != null)
        {
            Destroy(audio.gameObject);
        }
    }

    public void GamePause()
    {
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        Time.timeScale = 1;
    }
}
