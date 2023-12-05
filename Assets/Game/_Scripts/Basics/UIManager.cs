using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public Canvas gameCanvas;


    // Start is called before the first frame update
    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }
    private void OnEnable()
    {
        EntityEvents.entityDamaged += EntityDamaged;
        EntityEvents.entityDamaged += EntityAddHealth;
    }
    private void OnDisable()
    {
        EntityEvents.entityDamaged -= EntityDamaged;
        EntityEvents.entityDamaged -= EntityAddHealth;
    }
    public void EntityDamaged(GameObject player, int damageAmount)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageAmount.ToString();
        
    }
    public void EntityAddHealth(GameObject player, int healAmount)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healAmount.ToString();
    }
}
