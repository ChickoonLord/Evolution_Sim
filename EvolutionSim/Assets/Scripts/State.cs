using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class State {
    protected readonly CreatureAI creature;
    public State(CreatureAI creatureAI){
        creature = creatureAI;
    }
    public virtual void Start(){}
    public virtual void Update(){}
    public virtual void End(){}
}
public class Idle : State
{
    public Idle(CreatureAI creatureAI) : base(creatureAI){}
    public override void Update(){
        if (creature.hunger.percentage < 0.5){
            creature.SetState(new SearchingForFood(creature));
        }
    }
}

public class SearchingForFood : State
{
    GameObject targetFood = null;
    OrganismAI target;
    public SearchingForFood(CreatureAI creatureAI) : base(creatureAI){}
    public override void Update(){
        if (!targetFood){
            targetFood = creature.FindNearestObjectWithTag("Plant");
            if (targetFood){
                target = targetFood.GetComponent<OrganismAI>();
                creature.agent.SetDestination(targetFood.transform.position);
            } else return;
        }
        if (creature.col.Distance(target.col).distance < 0.1){
            creature.SetState(new Eating(creature, target));
        }
    }
}

public class Eating : State
{
    readonly OrganismAI food;
    public Eating(CreatureAI creatureAI, OrganismAI targetFood) : base(creatureAI){
        food = targetFood;
    }
    public override void Update(){
        if (!food){
            creature.SetState(new Idle(creature));
            return;
        }
        if (creature.col.Distance(food.col).distance < 0.1){
            Debug.Log("Eating "+food.name);
            if (creature.Attack(food)) {
                creature.hunger.percentage = 1;
            }
        } else {
            creature.SetState(new Idle(creature));
        }
    }
}