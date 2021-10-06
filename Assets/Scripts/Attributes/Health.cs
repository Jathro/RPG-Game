using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = 100f;
        bool isDead = false;
        float baseHealth;

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            if (isDead) { return; }
            baseHealth = GetMaxHealthPoints();
            healthPoints = baseHealth;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator,float damage)
        {
            Debug.Log(gameObject.name + " took damage: " + damage);
            if (healthPoints > 0)
            {
                healthPoints = Mathf.Max(healthPoints - damage, 0);
                if (healthPoints == 0)
                {
                    Die();
                    AwardExperience(instigator);
                }
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetPercentage()
        {
            return healthPoints / baseHealth * 100;
        }

        void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void RegenerateHealth()
        {
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) { return; }
            
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
            baseHealth = GetMaxHealthPoints();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints == 0)
            {
                Die();
            }
        }
    }
}