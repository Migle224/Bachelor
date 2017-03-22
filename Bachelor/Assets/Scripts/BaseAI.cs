using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAI
{
    ShelduleInfo currentDestination, nextDestination;
    GameManager gameManager;
    protected List<ShelduleInfo> sheldule = new List<ShelduleInfo>();
    protected NavMeshAgent agent;

    public BaseAI() {
        Debug.Log("BaseAI construct");
    }

    public virtual void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        this.SetDestination(gameManager.timeOfDay);

    }

    void SetDestination(float _timeOfDay)
    {
        ShelduleInfo last = sheldule[0];

        foreach (ShelduleInfo info in sheldule)
        {
            if (info.time > _timeOfDay)
            {
                agent.SetDestination(last.destination.transform.position);
                currentDestination = last;
                nextDestination = info;
                break;
            }
            else
            {
                last = info;
            }
        }

        if (currentDestination.Equals( sheldule[sheldule.Count - 1]))
        {
            nextDestination = sheldule[0];
        }
    }


    void SetDestination()
    {
        currentDestination = nextDestination;
        agent.SetDestination(currentDestination.destination.transform.position);

        if (currentDestination.Equals(sheldule[sheldule.Count - 1]))
        {
            nextDestination = sheldule[0];
        }
        else
        {
            nextDestination = sheldule[sheldule.FindIndex(isCurrent) + 1];
        }

    }

    private bool isCurrent(ShelduleInfo name)
    {

        return (name.Equals(currentDestination));
    }


    public virtual void Update()
    {


    }

    public virtual void OnTriggerEnter(Collider other)
    {

    }

    public virtual void InitSheldule() { }



}

