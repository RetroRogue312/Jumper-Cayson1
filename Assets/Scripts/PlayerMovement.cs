using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    public float bound = 7.5f;
    public float jumpForce = 15f;
    public float highestPos;
    public int altitude;
    public TMP_Text altText;
    public GameObject ground;
    public GameObject gameOvertxt;
    
    public bool isLanded;

    public bool gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isLanded = true;
        altitude = 0;
        highestPos = 0;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            Vector2 pos = transform.position;
            pos.x = pos.x - speed * Time.deltaTime;
            if (transform.position.x < bound)
            {
                transform.position = pos;
            }
        }
        if (Keyboard.current.dKey.isPressed)
        { 
            Vector2 pos = transform.position; 
            pos.x = pos.x + speed * Time.deltaTime;
            if (transform.position.x > -bound)
            {
                transform.position = pos;
            }
        }

        if (Keyboard.current.wKey.isPressed)
        {
            if (gameOver)
            {
                SceneManager.LoadScene("Menu");
                Time.timeScale = 1f;
            }
            if (isLanded)
            {
                rb.linearVelocity = new(0, jumpForce);
                isLanded = false;
            }
        }

        if (transform.position.y > highestPos)
        {
            highestPos = transform.position.y;
            altitude = (int) highestPos * 20;
            altText.SetText("Altitude: " + altitude);
        }

        if (transform.position.y < ground.transform.position.y - 3)
        {
            gameOver = true;
            Debug.Log("GameOver");
            Time.timeScale = 0.0f;
            gameOvertxt.SetActive(true);
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isLanded = true;
        }

        if (collision.gameObject.CompareTag("platform"))
        {
            if (ground != null)
            {
                Destroy(ground);
            }
            ground = collision.gameObject;
            ground.tag = "ground";
            isLanded = true;
        }
    }
}
