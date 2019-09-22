using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //variables
    public float speed;
    public Text countText;
    public Text winText;
    public Text livesText;
    public Text loseText;
    private Rigidbody2D rb2d;
    private int count;
    private int lives;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        count = 0;
        lives = 3;
        winText.text = "";
        loseText.text = "";
        SetCountText();
        SetLivesText();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();

            if (lives == 0)
            {
                //stops player from being able to control the ufo
                Destroy(this);
                //stops the ufo from moving right there
                this.gameObject.SetActive(false);
            }
        }

        if (count == 12)
        {
            //stops the ufo from keeping the momentum when changing levels
            rb2d.velocity = new Vector2(0.0f, 0.0f);
            transform.position = new Vector2(0, 70.0f); 
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 20)
        {
            winText.text = "You win! Game created by Julianne Truong!";
        }
    }

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives == 0)
        {
           loseText.text = "You lose! :(";
        }
    }
}
