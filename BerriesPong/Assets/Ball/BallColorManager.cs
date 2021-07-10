using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorManager : MonoBehaviour {
    [SerializeField] private MaterialVariable paletteMaterialRef;
    
    private Material[] ballColorMaterials;

    private void Awake() {
        ballColorMaterials = Resources.LoadAll<Material>("Ball Palettes/Materials/");

        Debug.Log($"Saved ball palette: {PaletteName}");
        foreach (var m in ballColorMaterials) {
            if (m.name.Equals(PaletteName)) {
                paletteMaterialRef.value = m;
                break;
            }
        }
    }

    private void OnEnable() {
        SelectBallColorButton.SelectedEvent += OnNewPaletteSelected;
    }

    private void OnDisable() {
        SelectBallColorButton.SelectedEvent -= OnNewPaletteSelected;
    }

    private void OnNewPaletteSelected() {
        PaletteName = paletteMaterialRef.value.name;
    }

    private string paletteKey => "ball-color-palette";

    private string PaletteName {
        get => PlayerPrefs.GetString(paletteKey, "Blue Ball Mat");
        set => PlayerPrefs.SetString(paletteKey, value);
    }
}