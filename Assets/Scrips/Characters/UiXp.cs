using Scrips.Characters;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
public class UiXp : MonoBehaviour
{
    [SerializeField] Slider _xpBars;
    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject UpMenu;
    [SerializeField] private GameObject LevelUP;
    
    private void Start()
    {
        _xpBars.maxValue = 100;
        _xpBars.minValue = 0;
        _xpBars.value = 0;
        UpMenu.SetActive(false);
        LevelUP.SetActive(false);
    }
    private void Update()
    {
        _xpBars.maxValue = Leveling.GetLevelUp();
        _xpBars.value = Leveling.GetCurrentXp();
        if (Leveling._upgradeOpen)
        {
            UpgradeMenu();
        }
        if (Leveling.currentLvl == 3)
        {
            Boss.SetActive(true);
        }
    }
    public void AddAttaque()
    {
        PlayerStats.SetAttackDamage(PlayerStats.GetAttackDamage()+5);
        CloseMenu();
    }
    public void AddHealth()
    {
        PlayerStats.SetMaxHealth(PlayerStats.GetMaxHealth()+10);
        CloseMenu();
    }
    public void AddAttSpeed()
    {
        PlayerStats.SetAttackDelay(PlayerStats.GetAttackDelay()-0.5f);
        CloseMenu();
    }
    public void UpgradeMenu()
    {
        Time.timeScale = 0f;
        UpMenu.SetActive(true);
        LevelUP.SetActive(true);
    }
    private void CloseMenu()
    {
        PlayerStats.SetCurrentHealth(PlayerStats.GetMaxHealthP());
        UpMenu.SetActive(false);
        LevelUP.SetActive(false);
        Time.timeScale =1f;
        Leveling._upgradeOpen = false;
        Leveling.currentXP -= Leveling.levelUp;
    }
}
