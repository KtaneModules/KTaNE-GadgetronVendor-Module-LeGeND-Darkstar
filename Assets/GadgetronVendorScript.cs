using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;
using KModkit;

public class GadgetronVendorScript : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMBombModule BombModule;
    public KMAudio Audio;

    public KMSelectable SubmitButton;
    public KMSelectable P1Button;
    public KMSelectable P10Button;
    public KMSelectable P100Button;
    public KMSelectable P1KButton;
    public KMSelectable S1Button;
    public KMSelectable S10Button;
    public KMSelectable S100Button;
    public KMSelectable S1KButton;

    public TextMesh boltCounter;

    public TextMesh currentAmmo;

    public TextMesh maxAmmo;

    public TextMesh submitCounter;

    public SpriteRenderer pdaIcon;

    public SpriteRenderer yourWeaponIcon;

    public SpriteRenderer saleWeaponIcon;

    public Sprite[] iconList;

    public Sprite[] weaponImageList;

    public Sprite[] pdaIconList;

    static int moduleIdCounter = 1;

    int moduleId;

    private bool moduleSolved;

    int boltAmount = 0;

    int pdaIconStatus = 0;

    int yourWeaponIndex = 0;

    int saleWeaponIndex = 0;

    public int[] ammoPrices;

    public int[] saleWeaponPrices;

    public int[] maxAmmoList;

    int currentAmmoAmount = 0;

    int requiredAmmoPurchaseAmount = 0;

    int correctSubmitAmount = 0;

    int answerNumber = 0;

    #region Starting Stuff
    void Awake()
    {
        moduleId = moduleIdCounter++;
    }

    void Start()
    {
        boltAmount = UnityEngine.Random.Range(20000, 200001);
        string boltCounterDisplay = Convert.ToString(boltAmount);
        boltCounter.text = boltCounterDisplay;
        Debug.LogFormat("[Gadgetron Vendor #{0}] You have {1} bolts.", moduleId, boltAmount);

        yourWeaponIndex = UnityEngine.Random.Range(0, 15);
        yourWeaponIcon.sprite = iconList[yourWeaponIndex];

        saleWeaponIndex = UnityEngine.Random.Range(0, 15);
        if(saleWeaponIndex == yourWeaponIndex)
        {
            saleWeaponIndex = saleWeaponIndex + 1;
            if (saleWeaponIndex == 15)
            {
                saleWeaponIndex = UnityEngine.Random.Range(0, 14);
            }
        }     
        saleWeaponIcon.sprite = weaponImageList[saleWeaponIndex];

        pdaIconStatus = UnityEngine.Random.Range(0, 2);
        if (pdaIconStatus == 1)
        {
            pdaIcon.sprite = pdaIconList[1];
            Debug.LogFormat("[Gadgetron Vendor #{0}] The PDA icon is lit.", moduleId);
        }
        else
        {
            pdaIcon.sprite = pdaIconList[0];
            Debug.LogFormat("[Gadgetron Vendor #{0}] The PDA icon is unlit.", moduleId);
        }

        SubmitButton.OnInteract += delegate () { SubmitAnswer(); return false; };
        P1Button.OnInteract += delegate () { Plus1(); return false; };
        P10Button.OnInteract += delegate () { Plus10(); return false; };
        P100Button.OnInteract += delegate () { Plus100(); return false; };
        P1KButton.OnInteract += delegate () { Plus1K(); return false; };
        S1Button.OnInteract += delegate () { Subtract1(); return false; };
        S10Button.OnInteract += delegate () { Subtract10(); return false; };
        S100Button.OnInteract += delegate () { Subtract100(); return false; };
        S1KButton.OnInteract += delegate () { Subtract1K(); return false; };

        CalculateValues();
	}
    #endregion
    #region Calculations
    void CalculateValues()
    {
        if(yourWeaponIndex == 0)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Bomb Glove.", moduleId);
            if(pdaIconStatus == 1)
            {
                ammoPrices[0] = ammoPrices[0] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[0]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[0]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[0] - currentAmmoAmount) * ammoPrices[0];
        }
        else if (yourWeaponIndex == 1)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Pyrocitor.", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[1] = ammoPrices[1] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[1]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[1]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[1] - currentAmmoAmount) * ammoPrices[1];
        }
        else if (yourWeaponIndex == 2)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Blaster.", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[2] = ammoPrices[2] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[2]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[2]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[2] - currentAmmoAmount) * ammoPrices[2];
        }
        else if (yourWeaponIndex == 3)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Glove of Doom.", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[3] = ammoPrices[3] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[3]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[3]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[3] - currentAmmoAmount) * ammoPrices[3];
        }
        else if (yourWeaponIndex == 4)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Mine Glove.", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[4] = ammoPrices[4] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[4]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[4]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[4] - currentAmmoAmount) * ammoPrices[4];
        }
        else if (yourWeaponIndex == 5)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Taunter.", moduleId);
            ammoPrices[5] = ammoPrices[5] + Bomb.GetPortCount();
            if (pdaIconStatus == 1)
            {
                ammoPrices[5] = ammoPrices[5] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[5]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[5]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[5] - currentAmmoAmount) * ammoPrices[5];
        }
        else if (yourWeaponIndex == 6)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Suck Cannon.", moduleId);
            ammoPrices[6] = (ammoPrices[6] + Bomb.GetPortPlateCount() + Bomb.GetBatteryCount()) * 2;
            if (pdaIconStatus == 1)
            {
                ammoPrices[6] = ammoPrices[6] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[6]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[6]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[6] - currentAmmoAmount) * ammoPrices[6];
        }
        else if (yourWeaponIndex == 7)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Devastator.", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[7] = ammoPrices[7] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[7]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[7]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[7] - currentAmmoAmount) * ammoPrices[7];
        }
        else if (yourWeaponIndex == 8)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Walloper.", moduleId);
            ammoPrices[8] = ammoPrices[8] + Bomb.GetSerialNumberNumbers().First() + Bomb.GetSerialNumberNumbers().Last();
            if (pdaIconStatus == 1)
            {
                ammoPrices[8] = ammoPrices[8] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[8]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[8]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[8] - currentAmmoAmount) * ammoPrices[8];
        }
        else if (yourWeaponIndex == 9)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Visibomb.", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[9] = ammoPrices[9] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[9]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[9]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[9] - currentAmmoAmount) * ammoPrices[9];
        }
        else if (yourWeaponIndex == 10)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Decoy Glove.", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[10] = ammoPrices[10] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[10]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[10]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[10] - currentAmmoAmount) * ammoPrices[10];
        }
        else if (yourWeaponIndex == 11)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Drone Device.", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[11] = ammoPrices[11] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[11]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[11]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[11] - currentAmmoAmount) * ammoPrices[11];
        }
        else if (yourWeaponIndex == 12)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Tesla Claw.", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[12] = ammoPrices[12] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[12]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[12]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[12] - currentAmmoAmount) * ammoPrices[12];
        }
        else if (yourWeaponIndex == 13)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the Morph-O-Ray.", moduleId);
            ammoPrices[13] = ammoPrices[13] + Bomb.GetIndicators().Count();
            if (pdaIconStatus == 1)
            {
                ammoPrices[13] = ammoPrices[13] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[13]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[13]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[13] - currentAmmoAmount) * ammoPrices[13];
        }
        else if (yourWeaponIndex == 14)
        {
            Debug.LogFormat("[Gadgetron Vendor #{0}] Your weapon is the R.Y.N.O..", moduleId);
            if (pdaIconStatus == 1)
            {
                ammoPrices[14] = ammoPrices[14] * 10;
            }
            currentAmmoAmount = UnityEngine.Random.Range(0, maxAmmoList[14]);
            string ammoCounterDisplay = Convert.ToString(currentAmmoAmount);
            string maxCounterDisplay = Convert.ToString(maxAmmoList[14]);
            currentAmmo.text = ammoCounterDisplay;
            maxAmmo.text = maxCounterDisplay;
            requiredAmmoPurchaseAmount = (maxAmmoList[14] - currentAmmoAmount) * ammoPrices[14];
        }

        CalculateAnswer();
    }
    #endregion
    #region Answer Calculations
    void CalculateAnswer()
    {
        Debug.LogFormat("[Gadgetron Vendor #{0}] The total price of your ammo is {1}.", moduleId, requiredAmmoPurchaseAmount);
        //If the price of ammo and a new weapon is too much:
        if(boltAmount - (saleWeaponPrices[saleWeaponIndex] + requiredAmmoPurchaseAmount) < 0)
        {
            correctSubmitAmount = boltAmount - requiredAmmoPurchaseAmount;
            Debug.LogFormat("[Gadgetron Vendor #{0}] Not enough bolts for a new weapon.", moduleId);
        }
        //Otherwise if it isn't too much:
        else
        {
            correctSubmitAmount = (boltAmount - saleWeaponPrices[saleWeaponIndex]) - requiredAmmoPurchaseAmount;
            Debug.LogFormat("[Gadgetron Vendor #{0}] You have enough bolts to buy a new weapon.", moduleId);
        }
        Debug.LogFormat("[Gadgetron Vendor #{0}] The price of the weapon for sale is {1}.", moduleId, saleWeaponPrices[saleWeaponIndex]);
        if(correctSubmitAmount > 9999)
        {
            correctSubmitAmount = correctSubmitAmount % 10000;
        }
        Debug.LogFormat("[Gadgetron Vendor #{0}] The correct amount to submit is {1}.", moduleId, correctSubmitAmount);
    }
    #endregion
    #region Button Stuff
    void SubmitAnswer()
    {
        if (!moduleSolved)
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            SubmitButton.AddInteractionPunch();
            Debug.LogFormat("[Gadgetron Vendor #{0}] Answered {1}. Expected {2}.", moduleId, answerNumber, correctSubmitAmount);
            if(answerNumber == correctSubmitAmount)
            {
                moduleSolved = true;
                GetComponent<KMBombModule>().HandlePass();
                Debug.LogFormat("[Gadgetron Vendor #{0}] Answer is correct! Module disarmed. Thank you for choosing Gadgetron.", moduleId);
                Audio.PlaySoundAtTransform("solvesound", transform);

                if (boltAmount - (saleWeaponPrices[saleWeaponIndex] + requiredAmmoPurchaseAmount) < 0)
                {
                    boltAmount = boltAmount - requiredAmmoPurchaseAmount;
                    string boltCounterDisplay = Convert.ToString(boltAmount);
                    boltCounter.text = boltCounterDisplay;
                }
                else
                {
                    boltAmount = (boltAmount - saleWeaponPrices[saleWeaponIndex]) - requiredAmmoPurchaseAmount;
                    string boltCounterDisplay = Convert.ToString(boltAmount);
                    boltCounter.text = boltCounterDisplay;
                }

                saleWeaponIcon.sprite = weaponImageList[15];
                string maxCounterDisplay = Convert.ToString(maxAmmoList[yourWeaponIndex]);
                currentAmmo.text = maxCounterDisplay;
                submitCounter.text = "";
            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Gadgetron Vendor #{0}] Strike! Answer is incorrect.", moduleId);
            }
        }
    }
    void Plus1()
    {
        if(!moduleSolved)
        {
            if (answerNumber < 9999)
            {
                answerNumber = answerNumber + 1;
                string SubmitDisplayNumber = Convert.ToString(answerNumber);
                submitCounter.text = SubmitDisplayNumber;
            }
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            P1Button.AddInteractionPunch(0.25f);
        }
    }
    void Plus10()
    {
        if (!moduleSolved)
        {
            if (answerNumber < 9990)
            {
                answerNumber = answerNumber + 10;
                string SubmitDisplayNumber = Convert.ToString(answerNumber);
                submitCounter.text = SubmitDisplayNumber;
            }
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            P10Button.AddInteractionPunch(0.25f);
        }
    }
    void Plus100()
    {
        if (!moduleSolved)
        {
            if (answerNumber < 9900)
            {
                answerNumber = answerNumber + 100;
                string SubmitDisplayNumber = Convert.ToString(answerNumber);
                submitCounter.text = SubmitDisplayNumber;
            }
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            P100Button.AddInteractionPunch(0.25f);
        }
    }
    void Plus1K()
    {
        if (!moduleSolved)
        {
            if (answerNumber < 9000)
            {
                answerNumber = answerNumber + 1000;
                string SubmitDisplayNumber = Convert.ToString(answerNumber);
                submitCounter.text = SubmitDisplayNumber;
            }
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            P1KButton.AddInteractionPunch(0.25f);
        }
    }
    void Subtract1()
    {
        if (!moduleSolved)
        {
            if (answerNumber >= 1)
            {
                answerNumber = answerNumber - 1;
                string SubmitDisplayNumber = Convert.ToString(answerNumber);
                submitCounter.text = SubmitDisplayNumber;
            }
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            S1Button.AddInteractionPunch(0.25f);
        }
    }
    void Subtract10()
    {
        if (!moduleSolved)
        {
            if (answerNumber >= 10)
            {
                answerNumber = answerNumber - 10;
                string SubmitDisplayNumber = Convert.ToString(answerNumber);
                submitCounter.text = SubmitDisplayNumber;
            }
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            S10Button.AddInteractionPunch(0.25f);
        }
    }
    void Subtract100()
    {
        if (!moduleSolved)
        {
            if (answerNumber >= 100)
            {
                answerNumber = answerNumber - 100;
                string SubmitDisplayNumber = Convert.ToString(answerNumber);
                submitCounter.text = SubmitDisplayNumber;
            }
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            S100Button.AddInteractionPunch(0.25f);
        }
    }
    void Subtract1K()
    {
        if (!moduleSolved)
        {
            if (answerNumber >= 1000)
            {
                answerNumber = answerNumber - 1000;
                string SubmitDisplayNumber = Convert.ToString(answerNumber);
                submitCounter.text = SubmitDisplayNumber;
            }
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            S1KButton.AddInteractionPunch(0.25f);
        }
    }
    #endregion
}
