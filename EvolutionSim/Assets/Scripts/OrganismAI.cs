using UnityEngine;

public class OrganismAI : MonoBehaviour
{
    public int maxHp  = 1;
    public int hp = 1;
    public float hpPercentage{get {return (float)hp/maxHp;}}
    public CreatureStats stats;
    private CircleCollider2D _col;
    public CircleCollider2D col{get {if (_col == null) _col = GetComponent<CircleCollider2D>(); return _col;} private set {_col = value;}}
    protected virtual void ApplyStats(){}
    protected virtual void ApplyStats(CreatureStats defaultStats){}
    protected virtual void ResetStats(){}
    public void SetMaxHp(int value){
        float hpPercent = hpPercentage;
        maxHp = value;
        hp = Mathf.RoundToInt(hpPercent*maxHp);
    }
    public virtual bool TakeDamage(int damage){
        hp -= damage;
        if (hp <= 0){
            Die();
            return true;
        }
        return false;
    }
    public virtual void Die(){
        Destroy(gameObject);
    }
}
