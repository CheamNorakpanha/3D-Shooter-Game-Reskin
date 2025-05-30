using UnityEngine;

public class Enemy_BossVisuals_VG : MonoBehaviour
{
    private Enemy_Boss_VG enemy;

    [SerializeField] private ParticleSystem landingZoneFx;
    [SerializeField] private GameObject[] weaponTrails;

    private void Awake()
    {
        enemy = GetComponent<Enemy_Boss_VG>();

        landingZoneFx.transform.parent = null;
        landingZoneFx.Stop();
    }

    private void Update()
    {

    }

    public void EnableWeaponTrail(bool active)
    {
        if (weaponTrails.Length <= 0)
        {
            Debug.LogWarning("No weapon trails assigned");
            return;
        }

        foreach (var trail in weaponTrails)
        {
            trail.gameObject.SetActive(active);
        }

    }

    public void PlaceLandingZone(Vector3 target)
    {
        landingZoneFx.transform.position = target;
        landingZoneFx.Clear();

        var MainModule = landingZoneFx.main;
        MainModule.startLifetime = enemy.travelTimeToTarget * 2;

        landingZoneFx.Play();
    }
}
