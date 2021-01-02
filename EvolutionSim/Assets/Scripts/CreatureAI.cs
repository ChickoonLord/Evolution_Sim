using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class CreatureAI : OrganismAI
{
    public Percentage hunger;
    public State state{get; private set;}
    [HideInInspector] public NavMeshAgent agent;
    float sightRange;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
        
        ApplyStats();

        state = new Idle(this);
    }
    protected override void ApplyStats(){
        transform.localScale = new Vector3(stats.size,stats.size,1);
        agent.speed = stats.speed;
        sightRange = stats.sightRange + (stats.size/2);
        hp = new Percentage(Mathf.RoundToInt(stats.size*10),hp.percentage);
        hunger = new Percentage(Mathf.RoundToInt(stats.size*5)+45,hunger.percentage);
    }
    protected override void ApplyStats(CreatureStats defaultStats){
        stats = defaultStats;
        ApplyStats();
    }
    protected override void ResetStats(){
        stats.size = transform.localScale.x;
        stats.speed = agent.speed;
        stats.sightRange = sightRange - (stats.size/2);
    }
    void Update()
    {
        state.Update();
        if (Input.GetMouseButtonDown(0)){
            SetState(new Idle(this));
            agent.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

    }
    public bool Attack(OrganismAI target){
        return target.TakeDamage(1);
    }
    public List<GameObject> FindObjectsWithTag(string tag){
        List<GameObject> objectsWithTag = new List<GameObject>();
        Collider2D[] allOverlappingColliders = Physics2D.OverlapCircleAll(transform.position, sightRange);
        foreach (Collider2D collider in allOverlappingColliders){
            GameObject obj = collider.gameObject;
            if (obj.tag == tag){
                if (!objectsWithTag.Contains(obj))
                    objectsWithTag.Add(obj);
            }
        }
        return objectsWithTag;
    }
    public GameObject FindNearestObjectWithTag(string tag){
        GameObject objectWithTag = null;
        float distance = sightRange;
        Collider2D[] allOverlappingColliders = Physics2D.OverlapCircleAll(transform.position, sightRange);
        foreach (Collider2D collider in allOverlappingColliders){
            GameObject obj = collider.gameObject;
            if (obj.tag == tag){
                float sqrMagnitude = (collider.ClosestPoint(transform.position)-(Vector2)transform.position).sqrMagnitude;
                if (sqrMagnitude < distance){
                    distance = sqrMagnitude;
                    objectWithTag = obj;
                }
            }
        }
        return objectWithTag;
    }
    public void SetState(State _state){
        state.End();
        state = _state;
        state.Start();
    }
}
