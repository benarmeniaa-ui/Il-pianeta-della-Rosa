using UnityEngine;
using UnityEngine.UI;

public class MuteButtonColor : MonoBehaviour
{
    public Image iconImage; // Trascina qui l'immagine del pulsante
    public Color activeColor = Color.white; // Colore quando è acceso (es. bianco pieno)
    public Color mutedColor = new Color(0.3f, 0.3f, 0.3f, 1f); // Colore grigio scuro/nero quando è spento
    
    private bool isOn = true;

    public void ToggleVisual()
    {
        isOn = !isOn;
        
        // Cambia il colore dell'immagine
        if (iconImage != null)
        {
            iconImage.color = isOn ? activeColor : mutedColor;
        }
    }
}