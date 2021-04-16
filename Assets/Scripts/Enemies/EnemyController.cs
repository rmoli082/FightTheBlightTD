using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyController : MonoBehaviour
{
    private Transform target;
    public Transform Target { get => target;}
    private int waypointIndex = 0;
    public int WaypointIndex { get => waypointIndex;}

    public bool isStunned = false;
    public float stunTime = 0f;

    private float speed = 5f;
    private float originalSpeed;

    public float Speed { get => speed; }

    private void Awake()
    {
        target = WaypointManager.Instance.Waypoints[0];
        originalSpeed = speed;
    }

    private void Update()
    {
        if (isStunned && stunTime <= 0)
        {
            speed = originalSpeed;
            isStunned = false;
        }
            
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * GetComponent<Enemy>().speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(target.position, transform.position) <= 0.4f)
        {
            GetNextWaypoint();
        }

        LookAtTarget();

        stunTime -= Time.deltaTime;
    }

    private void GetNextWaypoint()
    {
        waypointIndex++;
        
        if (waypointIndex >= WaypointManager.Instance.Waypoints.Length)
        {
            LevelManager.Instance.AdjustLives(-(Mathf.CeilToInt(gameObject.GetComponent<Enemy>().health)));
            WaveSpawner.Instance.livesLostThisWave += (Mathf.CeilToInt(gameObject.GetComponent<Enemy>().health));
            GameManager.Instance.EnemiesRemaining--;
            if (gameObject.GetComponent<Enemy>().isBoss)
            {
                GameManager.Instance.Lose();
            }
            Destroy(gameObject);
            return;
        }
        target = WaypointManager.Instance.Waypoints[waypointIndex];
    }

    public void SetTarget(Transform target, int waypointIndex)
    {
        this.target = target;
        this.waypointIndex = waypointIndex;
    }

    private void LookAtTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
