using UnityEngine;

public class OrganismAI : MonoBehaviour
{
    public CreatureStats stats;
    public Percentage hp;
    private CircleCollider2D _col;
    public CircleCollider2D col{get {if (_col == null) _col = GetComponent<CircleCollider2D>(); return _col;} private set {_col = value;}}
    protected virtual void ApplyStats(){}
    protected virtual void ApplyStats(CreatureStats defaultStats){}
    protected virtual void ResetStats(){}
    public virtual bool TakeDamage(int damage){
        if (hp.val > damage) hp.val -= damage;
        else {
            hp.val = 0;
            Die();
            return true;
        }
        return false;
    }
    public virtual void Die(){
        Destroy(gameObject);
    }
}
