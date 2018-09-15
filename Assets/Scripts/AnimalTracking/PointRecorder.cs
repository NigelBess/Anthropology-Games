using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRecorder : MonoBehaviour
{
    private bool recording;
    private bool recorded;
    private List<Vector2> positions;
    [SerializeField] private float minDistance;
    private Vector2 lastPos;

    private void Awake()
    {
        Reset();
    }
    public void Reset()
    {
        recording = false;
        recorded = false;
        positions = new List<Vector2>();
    }
    public void StartRecording()
    {
        if (recorded)
        {
            Debug.LogWarning("You are trying to record datapoints, but data has already been recorded");
            return;
        }
        recording = true;
        lastPos = GetPos();
    }
    private void Update()
    {
        if (!recording) return;
        Vector2 currentPos = GetPos();
        if (Vector2.Distance(currentPos, lastPos) > minDistance)
        {
            positions.Add(currentPos);
        }
    }
    public void StopRecording()
    {
        recording = false;
        recorded = true;
    }
    private Vector2 GetPos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
    public List<Vector2> GetInfo()
    {
        if (recording)
        {
            Debug.LogWarning("You are trying to read datapoints, but recording is in process");
            return new List<Vector2>(); ;
        }
        return positions;
    }
}
