using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [SerializeField]
    private Spell _currentSpell;
    private Spell _castSpell;
    private SpellBook _spellBook;
    private Vector3 _castingHand;

    private List<Spell> _spells;
    private int next = 0;

    public void SetSpell(string spellName)
    {
        // _currentSpell = _spellBook.GetSpell(spellName);
        _currentSpell = _spells[next++ % _spells.Count];
        SetEffects();
    }

    private void SetEffects()
    {
        GetComponent<RFX4_EffectEvent>().MainEffect = _currentSpell.Effect;
        GetComponent<RFX4_EffectEvent>().AdditionalEffect = _currentSpell.AdditionalEffect;
        GetComponent<RFX4_EffectEvent>().CharacterEffect = _currentSpell.CastEffect;
        GetComponent<RFX4_EffectEvent>().CharacterEffect2 = _currentSpell.AdditionalCastEffect;
    }

    public void CastSpell()
    {
        if (_currentSpell == null)
        {
            Debug.LogError("No spell is ready to cast!");
            return;
        }
        ActivateEffects();
        SetSpell("");
    }

    private void ActivateEffects()
    {
        GetComponent<RFX4_EffectEvent>().ActivateCharacterEffect();
        GetComponent<RFX4_EffectEvent>().ActivateCharacterEffect2();
        GetComponent<RFX4_EffectEvent>().ActivateEffect();
        GetComponent<RFX4_EffectEvent>().ActivateAdditionalEffect();
    }

    void Start()
    {
        _spellBook = GetComponent<SpellBook>();
        _spells = _spellBook.GetAvailableSpells().ToList();
        SetSpell("");
        // if (_currentSpell == null) _currentSpell = _spellBook.GetSpell("");

    }

    public void ReadyCastingHand()
    {
        // ! find a better way to get the hand position from the controller
        var rightHand = GameObject.Find("/MixedRealityPlayspace/Right_RiggedHandRight(Clone)/Palm Proxy Transform");
        var gazePointer = GameObject.Find("/MixedRealityPlayspace/DefaultGazeCursor(Clone)");
        if (rightHand == null || gazePointer == null)
        {
            return;
        }

        SetAttachments(rightHand);
        SetTarget(gazePointer);
    }

    private void SetAttachments(GameObject rightHand)
    {
        GetComponent<RFX4_EffectEvent>().CharacterAttachPoint = rightHand.transform;
        GetComponent<RFX4_EffectEvent>().CharacterAttachPoint2 = rightHand.transform;
        GetComponent<RFX4_EffectEvent>().AttachPoint = rightHand.transform;
        GetComponent<RFX4_EffectEvent>().AdditionalEffectAttachPoint = rightHand.transform;
    }

    private void SetTarget(GameObject target)
    {
        GetComponent<RFX4_EffectEvent>().OverrideAttachPointToTarget = target.transform;
    }
}
