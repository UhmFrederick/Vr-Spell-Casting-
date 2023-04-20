using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public RuneContainer Spell;
    public Lists SpellList;

    //find the correct spell in spell alist and then instanciates it at GetSpawnPosition()
    public void Cast()
    {
        
    }
    /// <summary>
    /// gets the average position of all the points(where the spell should spawn)
    /// </summary>
    /// <returns></returns>
    private Vector3 GetSpawnPosition()
    {
        Vector3 spellSpawnPoint = Vector3.zero;
        for (int i = 0; i < Spell.GetPoints().Count; i++)
        {
            spellSpawnPoint += Spell.GetPoints()[i];
        }
        spellSpawnPoint = spellSpawnPoint / Spell.GetPoints().Count;
        return spellSpawnPoint;
    }
}
