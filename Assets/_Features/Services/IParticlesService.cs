using UnityEngine;

public interface IParticlesService
{
    void PopParticle(Vector3 position, Color color);
    void BompParticle(Vector3 position);
    void Init();
}
