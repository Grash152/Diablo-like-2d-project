using UnityEngine;

namespace Scrips.Characters
{
    
    public abstract class BaseCharacterStats : MonoBehaviour
    {
        [SerializeField] protected new string name;
        [SerializeField] protected int maxHealth;
        [SerializeField] protected int attackDamage;
        [SerializeField] protected int defense;
        [SerializeField] protected float attackDelay;
        [SerializeField] protected float maxPlayerMana;

        public int CurrentHealth { get; set; }

        public int AttackDamage => attackDamage;

        public float AttackDelay
        {
            get => attackDelay;
            set => attackDelay = value;
        }
    }
}
