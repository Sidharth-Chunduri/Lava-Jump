using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{

    public float maxTime = 1;
    private float timer = 0;
    public GameObject bullet;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        //creates a bullet at a raondom height
        GameObject newbullet = Instantiate(bullet);
        newbullet.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
    }

    // Update is called once per frame
    void Update()
    {
        //creates a new bullet every few seconds at a random height
        if(timer > maxTime)
        {
            GameObject newbullet = Instantiate(bullet);
            newbullet.transform.position = transform.position + new Vector3(0, Random.Range(-height, height), 0);
            Destroy(newbullet, 15);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
