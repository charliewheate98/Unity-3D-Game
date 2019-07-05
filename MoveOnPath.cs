using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour
{
    public EditorPath pathToFollow;

    public int currentWaypointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    public string pathName;
    public bool loop;

    public bool play = false;
    private bool stop = false;

    private int _node;

    Vector3 last_position;
    Vector3 current_position;

    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        // pathToFollow = GameObject.Find(pathName).GetComponent<EditorPath>();
        last_position = transform.position;
    }

    public void StopAtNode(int node) { node = _node; }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            float distance = Vector3.Distance(pathToFollow.path_objs[currentWaypointID].position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position,
                pathToFollow.path_objs[currentWaypointID].position, speed * Time.deltaTime);

            var rotation = Quaternion.LookRotation(pathToFollow.path_objs[currentWaypointID].position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

            if (distance <= reachDistance)
            {
                currentWaypointID++;
            }

            if (currentWaypointID >= pathToFollow.path_objs.Count)
            {
                if (loop) currentWaypointID = 0;
                else if (!loop) currentWaypointID = currentWaypointID;
            }

            if (timer >= 5000 * Time.deltaTime)
            {
                currentWaypointID++;
            }
        }
    }
}
