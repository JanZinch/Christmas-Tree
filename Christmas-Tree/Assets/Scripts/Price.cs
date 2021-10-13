using UnityEngine;


public class Price : MonoBehaviour
{
    [SerializeField] private float _price = default;

    public float GetPrice() {

        return _price;
    }

}

