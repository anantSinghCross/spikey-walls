using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    public GameObject triangleObj;
    private float X;
    float xOffSet;
    
    int randomY;

    void Start () {
        if(gameObject.name == "Left")
        {
            xOffSet = 0f;
        }
        else
        {
            xOffSet = 0f;
        }
        initObstacles();
	}

    void initObstacles()
    {
        foreach(Transform triangle in transform)
        {
            Destroy(triangle.gameObject);
        }

        for(int i = 0; i<8; i++)
        {
            randomY = Random.Range(-6, 6);
            GameObject tempTriangle = Instantiate(triangleObj, new Vector2(transform.position.x + xOffSet, randomY * 2.5f), transform.rotation);
            tempTriangle.transform.SetParent(transform);

            //triangle.transform.position = position;
            //triangle.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Name = " + collision.gameObject.name);
        initObstacles();
    }
}