using UnityEngine;

public class BossWideEffect : EnemyEffect
{
    private Transform playerTransform; //이것도 나중에 매니저에서 받도록 수정
    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles;
    private int _particleNum;
    private bool _attackSuccess;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
        playerTransform = GamePlaySceneManager.Instance.PlayerClass.transform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _attackSuccess = false;
    }

    private void Update()
    {
        _particleNum = _particleSystem.GetParticles(_particles); //이것도 항상 update에서 받고 있어야 함
        if (_particleNum > 0 && !_attackSuccess)
        {
            if ((playerTransform.position - transform.position).magnitude < _particles[0].GetCurrentSize(_particleSystem) * 0.5f)
            {
                _attackSuccess = true;
                GamePlaySceneManager.Instance.PlayerConditions.TakePhysicalDamage(damage);
            }
        }
    }
}
