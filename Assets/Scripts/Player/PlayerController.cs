using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool IsIntimidating;
    [SerializeField] private float intimidationDuration = 2f;
    [Header("Inputs")]
    [SerializeField] private InputAction intimidateInput;
    


    private void Start()
    {
        intimidateInput.Enable();
        intimidateInput.performed += OnIntimidateInput;
    }

    private void OnIntimidateInput(InputAction.CallbackContext context)
    {
        Intimidate();
    }

    public void Intimidate()
    {   if(IsIntimidating) return;
        IsIntimidating = true;
        ResetIntimidation();
    }

    private async void ResetIntimidation()
    {
        await Task.Delay((int)intimidationDuration *1000);
        IsIntimidating = false;
    }

    private void OnDestroy()
    {
        intimidateInput.Disable();
    }

    
}
