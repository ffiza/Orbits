using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _energyText;
    [SerializeField] private TextMeshProUGUI _kinEnergyText;
    [SerializeField] private TextMeshProUGUI _potEnergyText;
    [SerializeField] private TextMeshProUGUI _timeText;
    private Orbit _orbit;

    private void Awake()
    {
        _orbit = GetComponent<Orbit>();
    }

    private void Update()
    {
        _energyText.text = "Energy: " + Mathf.Round(_orbit.GetEnergy()) + " J";
        _kinEnergyText.text = "Kinetic Energy: " + Mathf.Round(_orbit.GetKineticEnergy()) + " J";
        _potEnergyText.text = "Potential Energy: " + Mathf.Round(_orbit.GetPotentialEnergy()) + " J";
        _timeText.text = "Time: " + Mathf.Round(_orbit.GetTime()) + " s";
    }
}
