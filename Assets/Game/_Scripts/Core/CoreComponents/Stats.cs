using System;
using UnityEngine;


public class Stats : CoreComponent
{
    public event Action OnHealthZero;

    private SimpleFlash flashEffect;



    [SerializeField] private float maxHealth;
    private float currentHealth;

    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;

        flashEffect = gameObject.GetComponentInParent<SimpleFlash>();

    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        flashEffect.Flash();
        

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            OnHealthZero?.Invoke();
        }
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
}
