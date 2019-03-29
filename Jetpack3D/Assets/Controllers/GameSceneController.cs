using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{

    public PlayerController player;
    public Camera gameCamera;
    public Text scoreText;
    public Text timeAliveText;
    public Text hiscoreText;
    public Button restartButton;

    public GameObject[] blockPrefabs;

    private float blockPointer;
    private float safeArea = 30;
    private bool isGameOver;
    private float ellapsedTime;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        restartButton.gameObject.SetActive(false);

        if (PlayerPrefs.HasKey("hiscore"))
        {
            hiscoreText.text = "Hiscore: " + PlayerPrefs.GetFloat("hiscore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            ellapsedTime = Time.time - startTime;
            string minutes = ((int)ellapsedTime / 60).ToString();
            string seconds = (ellapsedTime % 60).ToString("f2");

            timeAliveText.text = minutes + ":" + seconds;

            if (PlayerPrefs.HasKey("hiscore"))
            {
                if (player.score > PlayerPrefs.GetFloat("hiscore"))
                {
                    hiscoreText.text = "Hiscore: " + player.score;
                }
            }
            else if(PlayerPrefs.HasKey("hiscore") == false || PlayerPrefs.GetFloat("hiscore") < player.score)
            {

            hiscoreText.text = "Hiscore: " + PlayerPrefs.GetFloat("hiscore");

            }

        }

        while (player != null && blockPointer < player.transform.position.x + safeArea)
        {

            int blockIndex = Random.Range(0, blockPrefabs.Length);

            if(blockPointer < 20)
            {
                blockIndex = 0;
            }

            GameObject blockObject = GameObject.Instantiate<GameObject>(blockPrefabs[blockIndex]);
            blockObject.transform.SetParent(this.transform);

            BlockController block = blockObject.GetComponent<BlockController>();

            blockObject.transform.position = new Vector3(blockPointer + block.size / 2, 0, 0);

            blockPointer += block.size;
        }

        if(player != null)
        {
            gameCamera.transform.position = new Vector3(
            player.transform.position.x,
            gameCamera.transform.position.y,
            gameCamera.transform.position.z
            );

            scoreText.text = "Score: " + player.score;

        }
        else
        {
            if (!isGameOver)
            {
                isGameOver = true;
                restartButton.gameObject.SetActive(true);
                restartButton.onClick.AddListener(() => restartGame());
            }
        }

    }

    public void restartGame()
    {
        if (PlayerPrefs.HasKey("hiscore"))
        {
            if (PlayerPrefs.GetFloat("hiscore") < player.score)
            {
                PlayerPrefs.SetFloat("hiscore", player.score);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetFloat("hiscore", player.score);
            PlayerPrefs.Save();
        }


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
