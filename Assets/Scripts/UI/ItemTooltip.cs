using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private Text textRadius;
    [SerializeField] private TextMeshProUGUI textDescription;

    public void SetupTooltip(ItemDetails itemDetails)
    {
        textName.text = itemDetails.name;
        textRadius.text = itemDetails.itemEffectRadius.ToString();
        textDescription.text = itemDetails.itemDescription;
    }
}
