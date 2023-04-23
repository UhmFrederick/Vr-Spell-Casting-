using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour
{
    public RuneContainer Spell;
    public RuneContainer Element;
    public Lists SpellList;

    //find the correct spell in spell alist and then instanciates it at GetSpawnPosition()
    public void Cast()
    {
        Debug.Log("cast");
        //Spawn Spell
        if (CheckElement(Spell,Element))
        {
            Debug.Log("instanciate");
            Instantiate(FindPrefab(Spell), GetSpawnPosition(), Quaternion.identity);
            Spell.Reset();
            Element.Reset();
        }
        else
        {
            CastFail();
        }
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

    /// <summary>
    /// hecks The Current spell is of the correct element
    /// </summary>
    /// <param name="S">Spell Container</param>
    /// <param name="E">Element Container</param>
    /// <returns></returns>
    private bool CheckElement(RuneContainer S, RuneContainer E)
    {
        if (S.GetName().Contains(E.GetName()))
        {
            Debug.Log("check good");
            return true;
        }
        else
        {
            return false;
        }
    }

    private GameObject FindPrefab(RuneContainer s)
    {
        for (int i = 0; i < SpellList.GetList().Count; i++)
        {
            if (s.GetName().Contains(SpellList.GetList()[i].name))
            {
                return SpellList.GetList()[i];
            }
        }
        //no spell match
        Debug.LogWarning("No spell match");
        CastFail();
        return null;
    }
    private void CastFail()
    {
        //Start cast fail event
        Spell.Reset();
        Element.Reset();
    }
}
