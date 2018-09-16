using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
     private Transform initial;
    private Rigidbody rb;
    [SerializeField]private Renderer rend;
    [SerializeField] private float maxSideAngle = 10;
    [SerializeField] private float minUpAngle = 15;
    [SerializeField] private float maxUpAngle = 20;
    [SerializeField] private float minSpeed = 15;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float hangTime = 0.4f;
    private Vector3 oldVelocity;
    private Vector3 oldAngular;
    [HideInInspector]public TrajGameManager gm;
    private bool hasCollided = false;
    [SerializeField] private bool tumble;
    [SerializeField] private float minAngular = 1;
    [SerializeField] private float maxAngular = 2;
    private Collider col;
    private Vector3 previousPos;
    private Quaternion previousRot;
    [SerializeField] private float embeddingDist = 0.5f;



    public void Reset()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (rend == null) rend = GetComponent<Renderer>();
        if (col == null) col = GetComponent<Collider>();
        transform.position = initial.position;
        transform.rotation = initial.rotation;
        rend.enabled = false;
        rb.detectCollisions = false;
        rb.useGravity = false;
        col.enabled = true;
    }
    public void Throw()
    {
        Reset();
        enabled = true;
        hasCollided = false;
        gameObject.SetActive(true);
        rb.useGravity = true;
        rb.velocity = NewVelocity();
        SetAngular();
        rend.enabled = true;
        StartCoroutine(WaitForHangTime(hangTime));
    }
    void SetAngular()
    {
        Vector3 newAngular = Vector3.zero;
        newAngular.z = RandAng();
        if (tumble)
        {
            newAngular.y = RandAng();
            newAngular.x = RandAng();
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }
        rb.angularVelocity = newAngular;
    }
    float RandAng()
    {
        return Random.Range(minAngular,maxAngular);
    }

    IEnumerator WaitForHangTime(float time)
    {
        yield return new WaitForSeconds(time);
        Pause();
    }


  
    Vector3 NewVelocity()
    {
        float sideAngle = Random.Range(-maxSideAngle,maxSideAngle)+initial.rotation.eulerAngles.y;
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
        rb.detectCollisions = true;
    }
    private void OnCollisionEnter(Collision collision)
    {   
       
        if (hasCollided) return;
        hasCollided = true;
        
        Debug.Log("detected a collision");
        Vector3 landPoint = new Vector3(transform.position.x, 0, transform.position.z);
        if (!tumble)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.detectCollisions = false;
            enabled = false;
            transform.position = previousPos + embeddingDist*transform.forward;
            transform.rotation = previousRot;
            col.enabled = false;
        }
            
        gm.LogLand(landPoint);
    }
    private void Update()
    {
        if (!tumble && rb.velocity.sqrMagnitude>0)
        {
            previousPos = transform.position;
            transform.rotation = Quaternion.LookRotation(rb.velocity,transform.up);
            previousRot = transform.rotation;
        }
    }
    public void SetInitial(Transform t)
    {
        initial = t;
    }

}
