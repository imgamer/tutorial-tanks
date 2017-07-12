using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        for(int i = 0; i < colliders.Length; i++)
        {
            // 击中效果表现：在爆炸中心产生力作用于目标
            Rigidbody targetRigibody = colliders[i].GetComponent<Rigidbody>();
            if(!targetRigibody)
                continue;
            targetRigibody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            // 爆炸产生伤害
            TankHealth targetHealth = targetRigibody.GetComponent<TankHealth>();
            if(!targetHealth)
                continue;
            float damage = CalculateDamage(targetRigibody.position);
            targetHealth.TakeDamage(damage);
        }

        // 子弹爆炸的表现
        m_ExplosionParticles.transform.SetParent(null); // 脱离父子关系
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);   // 5.6版本使用main.duration替代原来的.duration
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        // 从爆炸中心位置到目标位置，伤害按距离百分比递减，从最大伤害到0.
        float distance = (targetPosition - transform.position).magnitude;
        float relativeDistance = (m_ExplosionRadius - distance) / m_ExplosionRadius;
        float damage = Mathf.Max(0f, relativeDistance * m_MaxDamage);
        return damage;
    }
}