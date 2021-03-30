using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpellBook : MonoBehaviour
{

    [SerializeField]
    private Spell[] AvailableSpells;

    public IEnumerable<Spell> GetAvailableSpells()
    {
        return new List<Spell>(AvailableSpells);
    }

    public Spell GetSpell(string spellName)
    {
        if (AvailableSpells.Any())
        {
            var spell = AvailableSpells.Where(spell => spell.name == spellName).FirstOrDefault();
            return spell == null ? AvailableSpells.First() : spell;
        }
        throw new System.Exception("No available spells!");
    }
}