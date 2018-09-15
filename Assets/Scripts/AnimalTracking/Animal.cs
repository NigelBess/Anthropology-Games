using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initalRotation;
    private NavMeshAgent agent;
    private Animator anim;
    [SerializeField] private AnimationClip startJumpAnimation;
    [SerializeField] private AnimationClip stopJumpAnimation;
    [SerializeField] private AnimationClip jumpAnimation;
    [SerializeField] private float jumpSpeed = 5;
    private int remainingJumps;
    private action currentAction;
    [SerializeField] private int maxJumps = 3;
    [SerializeField]private float maxIdleTime = 3;
    [SerializeField] private float minIdleTime = 1;

    private enum action
    {
        none,
        idle,
        jump,
        walk
    }

    private void Awake()
    {
        currentAction = action.none;
        initalRotation = transform.rotation;
        initialPosition = transform.position;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void RandomActionAfter(float time)
    {
        StartCoroutine(RandomActionCrt(time));
    }
    IEnumerator RandomActionCrt(float time)
    {
        yield return new WaitForSeconds(time);
        RandomAction();
    }
    void RandomAction()
    {
        action nextAction = RollForAction();
        currentAction = nextAction;
        switch (nextAction)
        {
            case action.idle:
                SomeIdle();
                return;
            case action.jump:
                SomeJumps();
                return;
        }
    }
    action RollForAction()
    {
        action nextAction = currentAction;
        while (nextAction == currentAction)
        {
            int rolled = Random.Range((int)action.idle, (int)action.jump+1);
            nextAction = (action)rolled;
        }
        return nextAction;
    }
    public void SomeIdle()
    {
        float idleTime = Random.Range(minIdleTime,maxIdleTime);
        RandomActionAfter(idleTime);
    }

    public void SomeJumps()
    {
        remainingJumps = Random.Range(1, maxJumps);
        Debug.Log(remainingJumps);
        Jump();
    }
    public void Jump()
    {
        anim.Play("StartJump");
        StartCoroutine(ContinueJump(startJumpAnimation.length));
    }
    IEnumerator ContinueJump(float time)
    {
        yield return new WaitForSeconds(time);
        remainingJumps--;
        if (remainingJumps > 0)
        {
            Move(jumpSpeed, jumpAnimation.length);
            StartCoroutine(ContinueJump(jumpAnimation.length));
        }
        else
        {
            StopJump();
        }
    }
    void StopJump()
    {
        anim.Play("StopJumping");
        Move(jumpSpeed,stopJumpAnimation.length);
        RandomActionAfter(stopJumpAnimation.length);
    }
    private void Move(float speed,float time)
    {
        agent.speed = speed;
        float distance = speed * time;
        Vector3 destination = transform.position + (transform.forward) * distance;
        agent.SetDestination(destination);
    }
}
