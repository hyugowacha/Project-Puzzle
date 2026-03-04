using UnityEngine;

public class FeverText : MonoBehaviour
{
    [SerializeField] private ParticleSystem feverParticle;

    public void PlayFeverParticle()
    {
        feverParticle.Play();
    }
}
