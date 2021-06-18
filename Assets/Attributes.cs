using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Attributes : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float shield = 100;
    [SerializeField] bool alive = true;
    RectTransform healthBarSize, shieldBarSize;
    string myTag;
    Vector2 healthBarStartSize, shieldBarStartSize;
    // Start is called before the first frame update
    void Start()
    {
        myTag = transform.tag;
        if (myTag == "Player")
        {
            healthBarSize = GameObject.Find("HealthBar").GetComponent<RectTransform>();
            healthBarStartSize = healthBarSize.sizeDelta;
            shieldBarSize = GameObject.Find("ShieldBar").GetComponent<RectTransform>();
            shieldBarStartSize = shieldBarSize.sizeDelta;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damage(float damage)
    {
        if (shield > 0)
        {
            shield -= damage;
        }
        else
        {
            health -= damage;
        }
        if (myTag == "Player")
        {
            healthBarSize.sizeDelta = new Vector2((healthBarStartSize.x / 100) * health, healthBarStartSize.y);
            shieldBarSize.sizeDelta = new Vector2((shieldBarStartSize.x / 100) * shield, shieldBarStartSize.y);
            if (health == 0)
            {
                //GameOver();
            }
        }
        else
        {
            if (health == 0)
            {
                alive = false;
            }
        }
        
    }
    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public void setAlive(bool alive)
    {
        this.alive = alive;
    }
    public bool getAlive()
    {
        return alive;
    }
}
