using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public GameObject healthbar;
    public int currentHealth { get; private set; }

    public Stat armor;
    public Stat damage;


    void Awake()
    {
        currentHealth = maxHealth;
        healthbar.GetComponent<HealthBar>().SetMaxHealth(currentHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            healthbar.GetComponent<HealthBar>().SetHealth(currentHealth-10);
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log(armor.GetValue());
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue); //Чтоб не опускалось ниже нуля

        currentHealth -= damage;
        healthbar.GetComponent<HealthBar>().SetHealth(currentHealth);

        Debug.Log(transform.name + " takes " + damage + " damage . " + currentHealth + " hp remaining");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");

    }
}
