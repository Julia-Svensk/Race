using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject[] obstaclePrefabs;
    public Transform[] lanes;

    public float minDelay = 10f, maxDelay = 40f;

    private GameObject obstaclesParent;
    private float halvGroundSize;
    private Movement playerController;

    private void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        halvGroundSize = GameObject.Find("Environment").GetComponent<Environment>().halfLength; //104.5
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

        StartCoroutine(DelayForObstacles());

        obstaclesParent = GameObject.Find("ObstaclesParent");
        if (!obstaclesParent)
        {
            obstaclesParent = new GameObject();
            obstaclesParent.name = "ObstaclesParent";
            obstaclesParent.tag = "Obstacles";
        }
    }


    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    IEnumerator DelayForObstacles()
    {
        float timer = Random.Range(minDelay, maxDelay) / playerController.forwardSpeed; //Depends on how fast player is going.
        yield return new WaitForSeconds(timer);

        CreateObstacles(playerController.gameObject.transform.position.z + halvGroundSize); //Has offset.
       // Debug.Log(playerController.gameObject.transform.position.z + halvGroundSize);

        StartCoroutine(DelayForObstacles());
    }


    void CreateObstacles(float zPos)
    {
        int randomNumber = Random.Range(0, 10);

        //To instantiate obstacles depends on random number.
        if (randomNumber >= 0 && randomNumber <= 8)
        {
            int obstacleLane = Random.Range(0, lanes.Length);

            AddObstacles(new Vector3(lanes[obstacleLane].transform.position.x, 0f, zPos), Random.Range(0, obstaclePrefabs.Length));
        }

    }

    void AddObstacles(Vector3 pos, int type)
    {
        GameObject obs = Instantiate(obstaclePrefabs[type], pos, Quaternion.identity) as GameObject;
        obs.transform.position = pos;

        obs.transform.parent = obstaclesParent.transform;
    }
}
