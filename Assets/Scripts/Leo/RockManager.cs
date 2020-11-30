using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    public static RockManager Main;

    public GameObject Rock;

    private void Awake()
    {
        Main = this;
    }
    void Start()
    {
        transform.position = new Vector3(0, -9, 0);
        //Spawn(new Vector2(-5,5), new Vector2(5,5), 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) 
        {
            Spawn(new Vector2(-5, -6), new Vector2(5, -5), 5);
        }
    }

    public void Spawn(Vector2 PointA, Vector2 PointB,int SpawnAmount)
    {
        for (int i = 0; i < SpawnAmount; i++) 
        {
            Instantiate(Rock, new Vector3(Random.Range(PointA.x,PointB.x),
                                          Random.Range(PointA.y,PointB.y),
                                          -5),transform.rotation);
        }
    }
}
