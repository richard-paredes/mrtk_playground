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
    private SpellBook _spellBook;

    [SerializeField]
    private float _spellCooldownTime = 0.0f;
    private float _cooldownTimer = 0.0f;

    [SerializeField]
    private float _maxMana = 50.0f;

    [SerializeField]
    private float _manaRegenRate = 2.5f;
    private float _manaRegenDelay = 0.0f;
    private float _manaRegenTimer = 0.0f;

    private GameObject _target;

    [SerializeField]
    private ManaBar _manaBar;

    public void SetSpell(string spellName)
    {
        _currentSpell = _spellBook.GetSpell(spellName);
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
        if (_currentSpell == null || _cooldownTimer < _spellCooldownTime || _manaBar.GetCurrentMana() < _currentSpell.ManaCost)
        {
            return;
        }
        _cooldownTimer = 0;
        var newMana = _manaBar.GetCurrentMana() - _currentSpell.ManaCost;
        _manaBar.SetMana(newMana);
        ActivateEffects();
    }

    private void FixedUpdate()
    {
        _cooldownTimer += Time.fixedDeltaTime;
        if (_manaRegenTimer > _manaRegenDelay) {
            var newMana = Mathf.Min(_manaBar.GetCurrentMana() + _manaRegenRate * Time.fixedDeltaTime, _maxMana);
            _manaBar.SetMana(newMana);
            _manaRegenTimer = 0.0f;
        }
        _manaRegenTimer += Time.fixedDeltaTime;
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
        _cooldownTimer = _spellCooldownTime;
        _spellBook = GetComponentInChildren<SpellBook>();
        if (_currentSpell == null) SetSpell("");
        _manaBar.SetMaxMana(_maxMana);
    }

    public void ReadyCastingHand()
    {
        // ! find a better way to get the hand position from the controller
        var rightHand = GameObject.Find("/MixedRealityPlayspace/Right_RiggedHandRight(Clone)/Palm Proxy Transform");
        var palmTarget = GameObject.Find("/MixedRealityPlayspace/Right_RiggedHandRight(Clone)/Palm Proxy Transform/Y");
        if (rightHand == null || palmTarget == null)
        {
            Debug.LogError($"Could not find {rightHand} or {palmTarget}");
            return;
        }

        SetAttachments(rightHand);
        SetTarget(palmTarget);
    }

    private void SetAttachments(GameObject rightHand)
    {
        GetComponent<RFX4_EffectEvent>().CharacterAttachPoint = rightHand.transform;
        GetComponent<RFX4_EffectEvent>().CharacterAttachPoint2 = rightHand.transform;
        GetComponent<RFX4_EffectEvent>().AttachPoint = rightHand.transform;
        GetComponent<RFX4_EffectEvent>().AdditionalEffectAttachPoint = rightHand.transform;
    }

    private void SetTarget(GameObject palmTarget)
    {
        // if (_target != null) {
        //     Destroy(_target);
        // }
        // _target = new GameObject("Target");
        // _target.transform.TransformVector(palmTarget.transform.forward);
        // _target.transform.Translate(Vector3.down, Space.Self);
        // _target.transform.parent = palmTarget.transform;
        palmTarget.transform.Translate(Vector3.down);
        // ! Get the axis that is normal to the palm of the hand and create a game object 10 units away along that axis as target, then delete after spell is cast?
        GetComponent<RFX4_EffectEvent>().OverrideAttachPointToTarget = palmTarget.transform;
    }
}
