using Scrips.Characters;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    [SerializeField] Slider manaBar;
    [SerializeField] private float maxPlayerMana;
    [SerializeField] private float manaRegenInterval;
    private float _manaRegenTimer;
    private static float _manaCost;
    private static float _currentMana;
    private static float _maxMana;
    private static float _manaCostPercent;

    void Start()
    {
        InitializeVariables();
    }
    
    void Update()
    {
        ManaRegen();
        UpdateManaBar();
    }

    void InitializeVariables()
    {
        _maxMana = PlayerStats.GetMaxPlayerMana();
        _currentMana = _maxMana;
        manaBar.maxValue = _maxMana;
        manaBar.value = _currentMana;
        manaRegenInterval = 0.4f;
        _manaRegenTimer = 0.0f;
    }
    
    public static void UseMana(float manaCostPercent)
    {
        _manaCost = _maxMana * manaCostPercent;// 40% du maxMana du joueur
        _currentMana -= _manaCost;
        _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);
        
        //Debug.Log("Current Mana: " + _currentMana);
        //Debug.Log("BOOM!!!");
    }

    private void ManaRegen()
    {
        _manaRegenTimer += Time.deltaTime;
        
        if (_currentMana < _maxMana && _manaRegenTimer >= manaRegenInterval)
        {
            _manaRegenTimer = 0.0f;
            _currentMana++;
            _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);
            Debug.Log("Current Mana Regen: " + _currentMana);
        }
    }

    public static bool HasEnoughMana()
    {
        return _currentMana >= _manaCost;
    }
    
    private void UpdateManaBar()
    {
        manaBar.value = _currentMana;
    }
    public void SetManaMax(float value)
    {
        _maxMana = value;
    }
    public float GetManaMax()
    {
        return _maxMana;
    }
}