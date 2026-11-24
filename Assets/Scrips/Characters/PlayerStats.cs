using UnityEngine;

namespace Scrips.Characters
{
    public static class PlayerStats 
    {
        private static Vector3 currentPosition;
        private static string playerName = "Character Name";
        private static int maxHealth = 25;
        private static int attackDamage = 5;
        private static int defense = 0;
        private static float attackDelay = 1.5f;
        private static int currentHealth = maxHealth;
        private static float maxPlayerMana = 100;
        
        
        public static int GetMaxHealthP()
        {
            return maxHealth;
        }
        
        public static Vector3 GetCurrentPosition()
        {
            return currentPosition;
        }
        
        public static void SetCurrentPosition(Vector3 value)
        {
            currentPosition = value;
        }
        
        public static string GetPlayerName()
        {
            return playerName;
        }
        
        public static void SetPlayerName(string value)
        {
            playerName = value;
        }

        public static int GetMaxHealth()
        {
            return maxHealth;
        }

        public static void SetMaxHealth(int value)
        {
            maxHealth = value;
        }

        public static int GetAttackDamage()
        {
            return attackDamage;
        }

        public static void SetAttackDamage(int value)
        {
            attackDamage = value;
        }

        public static int GetDefense()
        {
            return defense;
        }
        
        public static void SetDefense(int value)
        {
            defense = value;
        }

        public static float GetAttackDelay()
        {
            return attackDelay;
        }

        public static void SetAttackDelay(float value)
        {
            attackDelay = value;
        }

        public static int GetCurrentHealth()
        {
            return currentHealth;
        }

        public static void SetCurrentHealth(int value)
        {
            currentHealth = value;
        }

        public static float GetMaxPlayerMana()
        {
            return maxPlayerMana;
        }
        
        public static void Reset()
        {
         maxHealth = 25;
         attackDamage = 5;
         defense = 0;
         attackDelay = 1.5f;
         currentHealth = maxHealth;
         maxPlayerMana = 100;
        }
    }
}
