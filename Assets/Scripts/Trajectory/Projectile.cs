using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform initial;
    private Rigidbody rb;
    private Renderer rend;
    public float maxSideAngle;
    public float minUpAngle;
    public float maxUpAngle;
    public float minSpeed;
    public float maxSpeed;
    public float hangTime;
    private Vector3 oldVelocity;
    private Vector3 oldAngular;
    [HideInInspector]public TrajGameManager gm;
    public GameObject pointer;
    private bool hasCollided = false;


    private void Awake()
    {
        Reset();
    }
    public void Throw()
    {
        Reset();
        hasCollided = false;
        gameObject.SetActive(true);
        rb.velocity = NewVelocity();
        rend.enabled = true;
        StartCoroutine(WaitForHangTime(hangTime));
    }

    IEnumerator WaitForHangTime(float time)
    {
        yield return new WaitForSeconds(time);
        Pause();
    }


    public void Reset()
    {   
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (rend == null) rend = GetComponent<Renderer>();
        transform.position = initial.position;
        transform.rotation = initial.rotation;
        pointer.SetActive(false);
        rend.enabled = false;
    }
    Vector3 NewVelocity()
    {
        float sideAngle = Random.Range(-maxSideAngle,maxSideAngle);
        float upAngle = Random.Range(minUpAngle,maxUpAngle);
        float speed = Random.Range(minSpeed,maxSpeed);
        Vector3 outVar = Vector3.zero;
        float xz = speed * Mathf.Cos(Rad(upAngle));
        outVar.y = speed * Mathf.Sin(Rad(upAngle));
        outVar.z = xz * Mathf.Cos(Rad(sideAngle));
        outVar.x = xz * Mathf.Sin(Rad(sideAngle));
        return outVar;
    }
    float Rad(float deg)
    {
        return deg * 3.14159f / 180f;
    }
    public void Pause()
    {
        Debug.Log("paused");
        oldVelocity = rb.velocity;
        oldAngular = rb.angularVelocity;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gm.WaitForInput();
    }

    public void UnPause()
    {
        rb.velocity = oldVelocity;
        rb.angularVelocity = oldAngular;
        rb.useGravity = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (hasCollided) return;
        hasCollided = true;
        Debug.Log("detected a collision");
        Vector3 landPoint = new Vector3(transform.position.x, 0, transform.position.z);
        pointer.SetActive(true);
        pointer.transform.position = landPoint;
        gm.LogLand(landPoint);
    }
}
