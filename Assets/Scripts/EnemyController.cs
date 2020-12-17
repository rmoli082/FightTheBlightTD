using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    public float speed = 5f;

    private void Awake()
    {
        target = WaypointManager.Instance.Waypoints[0];
    }

    private void Update()
    {

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(target.position, transform.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        LookAtTarget();
    }

    private void GetNextWaypoint()
    {
        waypointIndex++;
        
        if (waypointIndex >= WaypointManager.Instance.Waypoints.Length)
        {
            Destroy(this.gameObject);
            return;
        }

        target = WaypointManager.Instance.Waypoints[waypointIndex];
    }

    private void LookAtTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
