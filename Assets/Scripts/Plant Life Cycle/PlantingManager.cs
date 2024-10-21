using Smarteye;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Smarteye
{
    public class PlantingManager : MonoBehaviour
    {
        [SerializeField] private List<PlantProperties> _plantKind;
        public List<PlantProperties> PlantKind => _plantKind;

        private void OnValidate()
        {
            if (_plantKind.Count != System.Enum.GetValues(typeof(PlantType)).Length)
            {
                _plantKind = new List<PlantProperties>(new PlantProperties[System.Enum.GetValues(typeof(PlantType)).Length]);
            }
        }

        public GameObject GetPlantModel(PlantType type, PlantGrowStatus growthStatus)
        {
            int index = (int)type;

            if (index < _plantKind.Count)
            {
                int modelIndex = (int)growthStatus;
                Debug.Log(index.ToString() + " - " + modelIndex.ToString());

                return _plantKind[index].plantModel[modelIndex];
            }

            // Jika tidak ada model yang ditemukan, kembalikan null atau bisa mengembalikan GameObject default
            return null;
        }
    }

    [System.Serializable]
    public class PlantProperties
    {
        public List<GameObject> plantModel;
    }
}

public enum PlantGrowStatus
{
    None,
    Bibit,
    TanamanKecil,
    Berbunga,
    Berbuah,
    Layu
}

public enum PlantType
{
    Tomat,
    Jeruk
}
