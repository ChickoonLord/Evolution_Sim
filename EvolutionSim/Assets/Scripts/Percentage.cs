using UnityEngine;
[System.Serializable]
public class Percentage
{
    [SerializeField] private int _max;
    public int max{get{return _max;}}
    [SerializeField] private int _val;
    public int val{get{
        if (_val < max){
            if (_val > 0) return _val;
            else return 0;
        } else return max;}
        set{
            if (value > max) Debug.LogWarning("Percentage val "+value+" cannot be greater than max of "+max+".");
            else if (value < 0) Debug.LogWarning("Percentage val "+value+" cannot be less than 0.");
            _val = value;}}
    public float percentage{get {if (val == max) return 1; else return (float)val/max;} set {val = Mathf.RoundToInt(value*max);}}
    public Percentage(int maxVal = 1){
        _max = maxVal;
        val = maxVal;
    }
    public Percentage(int maxVal, int defaultVal){
        _max = maxVal;
        val = defaultVal;
    }
    public Percentage(int maxVal, float percent){
        _max = maxVal;
        percentage = percent;
    }
    public override string ToString()
    {
        return val+" / "+max+" = "+percentage*100+"%";
    }
    public static implicit operator Vector2(Percentage _percent){
        return new Vector2(_percent.val,_percent.max);
    }
    public static implicit operator Vector2Int(Percentage _percent){
        return _percent;
    }
    public static implicit operator Vector3(Percentage _percent){
        return new Vector3(_percent.val,_percent.max,_percent.percentage);
    }
    public static explicit operator Percentage(Vector2Int vector){
        return new Percentage(vector.y,vector.x);
    }
    public static explicit operator Percentage(Vector2 vector){
        return (Percentage)Vector2Int.RoundToInt(vector);
    }
}
