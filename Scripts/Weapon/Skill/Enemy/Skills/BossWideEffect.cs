using UnityEngine;

public class BossWideEffect : EnemyEffect
{
    private Transform playerTransform; //�̰͵� ���߿� �Ŵ������� �޵��� ����
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
        _particleNum = _particleSystem.GetParticles(_particles); //�̰͵� �׻� update���� �ް� �־�� ��
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
