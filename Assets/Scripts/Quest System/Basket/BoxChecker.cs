using Smarteye;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Fruit")) return;

        PlantSeed plantSeed = other.gameObject.GetComponent<PlantSeed>();
        if (plantSeed == null) return;

        if (!plantSeed.hasScored) {
            switch (plantSeed.plantType) {
                case PlantType.Tomat:
                GameManager.Instance.AddTomato();
                break;
                case PlantType.Jeruk:
                GameManager.Instance.AddOrange();
                break;
            }
            plantSeed.hasScored = true;
        } else {
            switch (plantSeed.plantType) {
                case PlantType.Tomat:
                GameManager.Instance.AddTomatoWithoutScore();
                break;
                case PlantType.Jeruk:
                GameManager.Instance.AddOrangeWithoutScore();
                break;
            }
        }
    }


    private void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Fruit")) return;

        PlantSeed plantSeed = other.gameObject.GetComponent<PlantSeed>();
        if (plantSeed == null) return;

        switch (plantSeed.plantType) {
            case PlantType.Tomat:
            GameManager.Instance.ReduceTomato();
            break;
            case PlantType.Jeruk:
            GameManager.Instance.ReduceOrange();
            break;
        }
    }

}
