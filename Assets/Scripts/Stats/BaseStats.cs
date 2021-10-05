﻿namespace RPG.Stats
{
    using UnityEngine;
    
    public class BaseStats : MonoBehaviour
    {
        [SerializeField] [Range(1, 50)] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null) { return startingLevel; }

            float currentXP = experience.GetExperience();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int levels = 1; levels <= penultimateLevel; levels++)
            {
                float xpToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, levels);
                if (xpToLevelUp > currentXP)
                {
                    return levels;
                }
            }
            return penultimateLevel + 1;
        }   
    }
}