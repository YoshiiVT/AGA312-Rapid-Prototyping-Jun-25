
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField, ReadOnly] private float health;
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private float lerpTimer;
    [SerializeField] private float chipSpeed = 2f;

    [SerializeField] private Image frontHealthBar;
    [SerializeField] private Image backHealthBar;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if (health == 0)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }

        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage(6);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            RestoreHealth(20);
        }
    }

    public void End()
    {
        StartCoroutine(DeathEnd());
    }

    private IEnumerator DeathEnd()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("EndingScene");
        SceneController SC = new SceneController();
        SC.LoadTitle();
        Debug.Log("SceneEnded");
    }
    private void UpdateHealthUI()
    {
        //Debug.Log(health);

        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

}
