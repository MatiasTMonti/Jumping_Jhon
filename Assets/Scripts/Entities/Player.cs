using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private TextMeshProUGUI startText = null;
    [SerializeField] private SpriteRenderer playerImage = null;

    [SerializeField] private AudioClip takeCoinSfx = null;

    private Rigidbody2D rigidBody = null;

    private float moveInput = 1.0f;

    private bool isStarted = false;

    private float topScore = 0.0f;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.gravityScale = 0;
        rigidBody.velocity = Vector3.zero;
    }

    private void Start()
    {
        ChangePlayerSkin();
    }

    private void Update()
    {
        GameNotStart();
        Score();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void ChangePlayerSkin()
    {
        Character character = GameDataManager.GetSelectedCharacter();
        if (character.image != null)
        {
            playerImage.sprite = character.image;
        }
    }

    private void PlayerMove()
    {
        moveInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(moveInput * speed, rigidBody.velocity.y);
    }

    private void GameNotStart()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isStarted == false)
        {
            isStarted = true;
            startText.gameObject.SetActive(false);
            rigidBody.gravityScale = 5.0f;
        }
    }

    private void Score()
    {
        if (rigidBody.velocity.y > 0 && transform.position.y > topScore)
        {
            topScore = transform.position.y;
        }

        scoreText.text = "Score: " + Mathf.Round(topScore).ToString();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            /*
            #if UNITY_EDITOR
            if (Input.GetKey(KeyCode.C))
            {
                GameDataManager.AddCoins(179);
            }
            #endif
            */

        if (collision.gameObject.CompareTag("Coin"))
        {
            AudioController.instance.PlaySound(takeCoinSfx);

            GameDataManager.AddCoins(10);

            GameSharedUI.Instance.UpdateCoinsUIText();

            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "DeadZone")
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
