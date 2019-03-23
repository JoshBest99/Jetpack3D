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
    public Button restartButton;

    public GameObject[] blockPrefabs;

    private float blockPointer;
    private float safeArea = 30;
    private bool isGameOver;
    private float ellapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        ellapsedTime = Time.time;
        restartButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        while(player!= null && blockPointer < player.transform.position.x + safeArea)
        {

            timeAliveText.text = (Time.time - ellapsedTime).ToString();

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
