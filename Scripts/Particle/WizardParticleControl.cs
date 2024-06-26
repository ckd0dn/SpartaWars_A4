
using UnityEngine;

public class WizardParticleControl : MonoBehaviour
{
    //[SerializeField] private bool createWizardOnWalk = true;
    [SerializeField] private ParticleSystem MagickParticleSystem;

    public void CreateMagickParticles()
    {
            MagickParticleSystem.Play();
    }
}
