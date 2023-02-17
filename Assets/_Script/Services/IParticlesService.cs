using UnityEngine;

public interface IParticlesService
{
    void PopParticle(Vector3 position, Color color);
    void Init();
}
