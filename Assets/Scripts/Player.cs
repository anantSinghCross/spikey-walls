using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {

    private int jumpX = 8;
    private int jumpY = 15;
    public GameObject wallHitEffect;
    public GameObject deadEffect;
    Rigidbody2D rb;
    bool isDead = false;
    ScoreManager Obj;
    MenuManager menuManager;
    bool isFirstTouch = true;
    float hueValue;




    void Start () {

        menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        Obj = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        //CurrentScore = GetComponent<TextMesh>();
        rb = GetComponent<Rigidbody2D>();
        menuManager.setWallsOff();

        hueValue = Random.Range(0, 10) / 10.0f;
        SetBackgroundColor();

        rb.isKinematic = true;

	}
	
	void Update () {

        
        if (Input.GetMouseButtonDown(0))
        {
            menuManager.jumpSound();
            if (isFirstTouch == true)
            {
                isFirstTouch = false;
                menuManager.startTheGame();
                
                rb.isKinematic = false;
                rb.velocity = new Vector2(-1f, 10);
            }
            if (rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(jumpX, jumpY);
            }
            else
            {
                rb.velocity = new Vector2(-jumpX, jumpY);
            }
        }
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            menuManager.jumpSound();
            GameObject effect = Instantiate(wallHitEffect, collision.contacts[0].point, Quaternion.identity);
            Destroy(effect, 0.35f);

            if(collision.gameObject.name == "Left" || collision.gameObject.name == "Right")
            {
                Obj.addScore();
            }
        }

        if(collision.gameObject.tag == "Triangle" && isDead == false)
        {
            Handheld.Vibrate();
            menuManager.deathSound();
            isDead = true;
            GameObject deadeffect = Instantiate(deadEffect, collision.contacts[0].point, Quaternion.identity);
            Destroy(deadeffect, 0.5f);

            rb.velocity = new Vector2(0, 0);
            

            yield return new WaitForSecondsRealtime(.5f);

            rb.isKinematic = true;
            menuManager.switchToDeath();
            Debug.Log("deactivating player");

            gameObject.SetActive(false);
        }
    }

    void SetBackgroundColor()
    {
        hueValue += 0.1f;
        if (hueValue >= 1)
        {
            hueValue = 0;
        }
        Camera.main.backgroundColor = Color.HSVToRGB(hueValue, 0.6f, 0.8f);
    }
}