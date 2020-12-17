using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : Singleton<WaypointManager>
{
    private Transform[] _waypoints;
    public Transform[] Waypoints { get => _waypoints; }

    protected override void Awake()
    {
        base.Awake();
        _waypoints = new Transform[transform.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i] = transform.GetChild(i);
        }
    }

}
