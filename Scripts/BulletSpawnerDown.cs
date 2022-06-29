using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnerDown : MonoBehaviour
{

    public float maxTime = 1;
    private float timer = 0;
    public GameObject bullet;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newbullet = Instantiate(bullet);
        newbullet.transform.position = transform.position + new Vector3(Random.Range(-height, height), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > maxTime)
        {
            GameObject newbullet = Instantiate(bullet);
            newbullet.transform.position = transform.position + new Vector3(Random.Range(-height, height), 0 , 0);
            Destroy(newbullet, 15);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
