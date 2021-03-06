using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            GetComponent<Text>().text = Mathf.RoundToInt(health.GetHealthPoints()).ToString() + " / " + Mathf.RoundToInt(health.GetMaxHealthPoints()).ToString();
        }
    }
}