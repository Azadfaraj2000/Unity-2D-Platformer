using UnityEngine;


public class Death : CoreComponent
{
    [SerializeField] private GameObject deathPrefab;

    private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);
    private Stats stats;

    public void Die()
    {

        core.transform.parent.gameObject.SetActive(false);
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
    }

    private void OnEnable()
    {
        Stats.OnHealthZero += Die;
        
    }

    private void OnDisable()
    {
        Stats.OnHealthZero -= Die;
    }
}
