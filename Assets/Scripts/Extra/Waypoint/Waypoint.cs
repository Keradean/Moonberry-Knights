using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points; // Array of points of interest (POIs) for the waypoint system

    public Vector3 entityposition { get; set; }
}
