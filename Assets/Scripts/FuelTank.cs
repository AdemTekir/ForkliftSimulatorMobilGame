using UnityEngine;

public class FuelTank : MonoBehaviour
{
    public float fuelInc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Forklift"))
        {
            Destroy(gameObject);

            other.gameObject.GetComponent<ForkliftController>().currentFuel += fuelInc;

            if (other.gameObject.GetComponent<ForkliftController>().currentFuel > 100f)
            {
                other.gameObject.GetComponent<ForkliftController>().currentFuel = 100f;
            }
        }
    }
}
