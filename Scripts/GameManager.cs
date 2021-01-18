using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject obstacles;

    [SerializeField]
    Transform obsParent;

    [SerializeField]
    List<Vector3> existingObstacles;

    [SerializeField]
    GameObject ai;

    void Start()
    {
        existingObstacles = new List<Vector3>();
    }

    public void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
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
        Vector3 pos = RandomLocation();

        existingObstacles.Add(pos);

        GameObject obs = Instantiate(obstacles, pos, Quaternion.identity);
        obs.transform.parent = obsParent;
    }
       

    public void SpawnAI()
    {
        Instantiate(ai, RandomLocation(), Quaternion.identity);
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
