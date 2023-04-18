using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Rune Container")]
public class RuneContainer : ScriptableObject
{
    private string Name;
    private float Score;
    private List<Vector3> PointList;
    public float scoreThreshold;
    //Compairs the Score to the score threshold.
    private bool ScoreCompair(float score)
    {
        if (score >= scoreThreshold)
        {
            return true;
        }
        else // cast failure
        {
            return false;
        }
    }
    /// <summary>
    /// Sets the value of the Spell or Element as well as its Score and a list of its points
    /// </summary>
    /// <param name="name">Name of the Spell or Element</param>
    /// <param name="score">Confidence score given to this Rune</param>
    /// <param name="points">List of points making up the Rune</param>
    public void SetValues(string name, float score, List<Vector3> points)
    {
        if (ScoreCompair(score))
        {
            Name = name;
            Score = score;
            PointList = points;
        }
    }
    /// <summary>
    /// Returns The Name of the Spell or Element as a String
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return Name;
    }
    /// <summary>
    /// Returns the Score of the Rune
    /// </summary>
    /// <returns></returns>
    public float GetScore()
    {
        return Score;
    }
    /// <summary>
    /// Returns a List of the points making up the Rune
    /// </summary>
    /// <returns></returns>
    public List<Vector3> GetPoints()
    {
        return PointList;
    }
}
