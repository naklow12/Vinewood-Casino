using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] coinAnimations;
    private float coinEmissionRate;

    private void Start()
    {
        coinEmissionRate = coinAnimations[0].emission.rateOverTimeMultiplier;
    }

    public void callCoinAnimation(int[] result)
    {
        resetEmissionRate();
        if (result[0] == result[1] && result[1] == result[2])
        {
            float multiplier = getNewEmissionRate(result[0]);
            for (int i = 0; i < coinAnimations.Length; i++)
            {
                var emission = coinAnimations[i].emission;
                emission.rateOverTimeMultiplier = coinEmissionRate * multiplier;
                if (!coinAnimations[i].isPlaying)
                    coinAnimations[i].Play();
            }
        }
    }

    public void resetEmissionRate()
    {
        for (int i = 0; i < coinAnimations.Length; i++)
        {
            var emission = coinAnimations[i].emission;
            emission.rateOverTimeMultiplier = coinEmissionRate;
        }
    }

    public float getNewEmissionRate(int num)
    {
        float emissionMultiplier = 1;
        switch (num)
        {
            case 2:
                emissionMultiplier = 2;
                break;

            case 4:
                emissionMultiplier = 1.8f;
                break;

            case 3:
                emissionMultiplier = 1.6f;
                break;

            case 1:
                emissionMultiplier = 1.4f;
                break;

            case 0:
                emissionMultiplier = 1.2f;
                break;
        }

        return emissionMultiplier;
    }
}
