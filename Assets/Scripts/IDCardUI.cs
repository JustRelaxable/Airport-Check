using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IDCardUI : MonoBehaviour
{
    public ID personID;

    public TextMeshProUGUI personName;
    public TextMeshProUGUI personAge;
    public TextMeshProUGUI personIncome;
    public TextMeshProUGUI personTemperature;
    public TextMeshProUGUI personNation;
    public TextMeshProUGUI personCharacteristics;
    public Image nationFlag;
    public Image personImage;

    private void Start()
    {
        //FillIDCard();
    }

    public void FillIDCard()
    {
        personName.text = personID.personName;
        personAge.text = "Age:" + personID.personAge.ToString();
        personIncome.text = "Passport";
        personTemperature.text = "Temperature:" + personID.bodyTemperature.ToString();
        personNation.text = personID.nationality.nationName;
        personImage.color = personID.bodyColor;
        personCharacteristics.text = "Gender:" + personID.personGender;

        nationFlag.sprite = personID.nationality.nationFlag;
    }
}


