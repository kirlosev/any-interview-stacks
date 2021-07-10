using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBallColorButton : MonoBehaviour {
    public static event System.Action SelectedEvent;
    
    [SerializeField] private MaterialVariable paletteMaterialRef;
    [SerializeField] private Image viewImg;

    private Material colorMaterial;
    
    public void BTN_SELECT() {
        paletteMaterialRef.value = colorMaterial;
        SelectedEvent?.Invoke();
    }

    public void Init(Material mat) {
        colorMaterial = mat;
        viewImg.material = colorMaterial;
        viewImg.SetNativeSize();
    }
}