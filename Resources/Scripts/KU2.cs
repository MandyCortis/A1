using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KU2 : MonoBehaviour
{
    [SerializeField]
    GameObject obstacles;

    [SerializeField]
    Transform obsParent;

    [SerializeField]
    List<Vector3> existingObstacles;

    private float timer = 0.0f;
    readonly private float waitTime = 1.0f;

    void Start()
    {
        AddObstacle();
        StartAI();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
            timer = waitTime;
        }
    }

    private Vector3 RandomLocation()
    {
        Vector3 pos;
        int x, y;

        do
        {
            x = Random.Range(-49, 49);
            y = Random.Range(-49, 49);

            pos = new Vector3(x + 1.5f, y + 1.5f);
        }
        while (existingObstacles.Contains(new Vector3(x, y)));

        return pos;
    }

    public void AddObstacle()
    {
        for (int counter = 0; counter < 5; counter++)
        {
            Vector3 pos = RandomLocation();
            existingObstacles.Add(pos);
            GameObject obs = Instantiate(obstacles, pos, Quaternion.identity);
            obs.transform.parent = obsParent;

        }

    }

    public void StartAI()
    {
        GameObject[] aim = GameObject.FindGameObjectsWithTag("AI");
        for (int i = 0; i < aim.Length; i++)
        {
            aim[i].GetComponent<customAIMoveScript>().enabled = true;
        }
    }
}
