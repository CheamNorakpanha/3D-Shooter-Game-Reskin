using UnityEngine;

public class Enemy_AnimationEvents : MonoBehaviour
{
    private Enemy enemy;
    private Enemy_Melee enemyMelee;
    private Enemy_Boss enemyBoss;
    private Enemy_Boss_VG enemyBossVG;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyMelee = GetComponentInParent<Enemy_Melee>();
        enemyBoss = GetComponentInParent<Enemy_Boss>();
    }

    public void AnimationTrigger() => enemy.AnimationTrigger();

    public void StartManualMovement() => enemy.ActivateManualMovement(true);

    public void StopManualMovement() => enemy.ActivateManualMovement(false);

    public void StartManualRotation() => enemy.ActivateManualRotation(true);
    public void StopManualRotation() => enemy.ActivateManualRotation(false);

    public void AbilityEvent() => enemy.AbilityTrigger();
    public void EnableIK() => enemy.visuals.EnableIK(true, true, 1f);

    public void EnableWeaponModel()
    {
        enemy.visuals.EnableWeaponModel(true);
        enemy.visuals.EnableSeconoderyWeaponModel(false);
    }

    public void BossJumpImpact()
    {
        enemyBoss?.JumpImpact();

        if (enemyBossVG == null)
            enemyBossVG = GetComponentInParent<Enemy_Boss_VG>();

        enemyBossVG?.JumpImpact();
    }

    public void BeginMeleeAttackCheck()
    {
        enemy?.EnableMeleeAttackCheck(true);
    }

    public void FinishMeleeAttackCheck()
    {
        enemy?.EnableMeleeAttackCheck(false);
    }
}
