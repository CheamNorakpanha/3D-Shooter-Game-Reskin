using UnityEngine;

public class Flamethrow_DamageArea_VG : MonoBehaviour
{
    private Enemy_Boss_VG enemy;

    private float damageCooldown;
    private float lastTimeDamaged;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy_Boss_VG>();
        damageCooldown = enemy.flameDamageCooldown;
    }

    private void OnTriggerStay(Collider other)
    {
        if (enemy.flamethrowActive == false)
            return;

        if (Time.time - lastTimeDamaged < damageCooldown)
            return;

        IDamagable damagable = other.GetComponent<IDamagable>();

        if (damagable != null)
        {
            damagable.TakeDamage();
            lastTimeDamaged = Time.time; // Update the last tiem damage was applied
            damageCooldown = enemy.flameDamageCooldown; // For easier testing I'm updating
                                                        // cooldown everytime we damage enemy
        }
    }
}
