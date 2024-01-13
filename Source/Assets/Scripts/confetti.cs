using System.Collections;
using UnityEngine;

public class confetti : MonoBehaviour
{

    [SerializeField] ParticleSystem particle;

    private void Start()
    {
        StartCoroutine(playEffect()); 
    }

    IEnumerator playEffect()
    {
        while(true)
        {
            particle.Play();
            yield return new WaitForSeconds(2f);
        }

    }
}
