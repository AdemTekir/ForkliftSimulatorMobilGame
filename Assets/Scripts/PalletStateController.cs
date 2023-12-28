using System.Collections;
using UnityEngine;

public class PalletStateController : MonoBehaviour
{
    public InGameMenuEdit inGameMenuEdit;
    
    public float timerForPalletStateCheck = 5f;

    private Coroutine palletCoroutine;
    
    private bool paletOnComplete;

    private float completelyOut = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Palet"))
        {
            if (completelyOut == 2f)
            {
                completelyOut -= 2f;
            }

            if (completelyOut != 0f)
            {
                completelyOut--;
            }

            if (completelyOut == 0)
            {
                if (palletCoroutine == null) 
                {
                    paletOnComplete = false;
                    palletCoroutine = StartCoroutine(PalletStateCheck());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Palet"))
        {
            completelyOut++;

            if (completelyOut == 1f)
            {
                StopAllCoroutines();
                palletCoroutine = null;

                if (paletOnComplete)
                {
                    inGameMenuEdit.PalletCountCheckMenu(-1f);
                }
            }

            if (completelyOut == 2f)
            {
                palletCoroutine = null;
            }

            inGameMenuEdit.SetTimer(5f, false);
        }
    }

    private IEnumerator PalletStateCheck() 
    {
        if (!paletOnComplete)
        {
            float timer = timerForPalletStateCheck;
            inGameMenuEdit.PalletCountCheckMenu(0);

            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                inGameMenuEdit.SetTimer(timer, true);

                yield return new WaitForEndOfFrame();
            }

            if (timer < 0f)
            {
                inGameMenuEdit.SetTimer(timer, false);
                inGameMenuEdit.PalletCountCheckMenu(1);
                paletOnComplete = true;
            }
        }
    }
}
