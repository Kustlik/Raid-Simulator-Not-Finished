using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacterStats : MonoBehaviour
{
    [SerializeField] Image[] images;
    [SerializeField] Image[] transparentBars230;
    [SerializeField] Image[] transparentBars135;
    [SerializeField] Text[] texts;
    [SerializeField] Material materialTexture;
    [SerializeField] public PlayerData playerData;
    [SerializeField] public PlayerMenu allPlayers;

    [SerializeField] Image roleIcon;
    [SerializeField] Image classIcon;
    [SerializeField] Image classBar;
    [SerializeField] Text nick;
    [SerializeField] Image minDpsBar;
    [SerializeField] Image maxDpsBar;
    [SerializeField] Image minHpsBar;
    [SerializeField] Image maxHpsBar;
    [SerializeField] Image minSurviBar;
    [SerializeField] Image maxSurviBar;

    [SerializeField] Sprite tankSprite;
    [SerializeField] Sprite mDpsSprite;
    [SerializeField] Sprite rDpsSprite;
    [SerializeField] Sprite mHpsSprite;
    [SerializeField] Sprite rHpsSprite;

    [SerializeField] Sprite priestS;
    [SerializeField] Sprite mageS;
    [SerializeField] Sprite warlockS;
    [SerializeField] Sprite demonRetardS;
    [SerializeField] Sprite druidS;
    [SerializeField] Sprite rogueS;
    [SerializeField] Sprite monkS;
    [SerializeField] Sprite hunterS;
    [SerializeField] Sprite shamanS;
    [SerializeField] Sprite warriorS;
    [SerializeField] Sprite paladinS;
    [SerializeField] Sprite deathKnightS;

    [SerializeField] private UI_StatsRadarChart uiStatsRadarChart;
    private Stats stats;

    bool showingPanel = false;
    bool hidingPanel = false;
    bool generatingChart = false;

    float classBarValue;
    float[] imagesValue = new float[1];
    float[] transparentBars230Value = new float[1];
    float[] transparentBars135Value = new float[1];
    float[] textsValue = new float[1];
    float materialValue;

    float minDpsBarFillValue = 0;
    float maxDpsBarFillValue = 0;
    float minHpsBarFillValue = 0;
    float maxHpsBarFillValue = 0;
    float minSurviBarFillValue = 0;
    float maxSurviBarFillValue = 0;

    float hpsValue = 0;
    float surviValue = 0;
    float stabilityValue = 0;
    float rangeValue = 0;
    float dpsValue = 0;

    float radarHpsValue = 0;
    float radarSurviValue = 0;
    float radarStabilityValue = 0;
    float radarRangeValue = 0;
    float radarDpsValue = 0;

    float ColorRValue = 0;
    float ColorGValue = 0;
    float ColorBValue = 0;

    float radarColorR = 0;
    float radarColorG = 0;
    float radarColorB = 0;

    static float t = 0.0f;
    static float fadet = 1.0f;
    static float chartTimer = 0.0f;

    public Image image;
    public Text text;
    public Material tempMaterial;

    public float maxDpsAnchor;
    public float maxHpsAnchor;
    public float maxParsesAnchor;

    void Start()
    {
        Stats stats = new Stats(0f, 0f, 0f, 0f, 0f);

        uiStatsRadarChart.SetStats(stats);
    }

    public void LoadStats()
    {
        maxDpsAnchor = GetMaxDps();
        maxHpsAnchor = GetMaxHps();
        maxParsesAnchor = GetMaxParses();
    }

    public void ShowPanel()
    {
        nick.text = playerData.GetPlayerName();

        chartTimer = 0.0f;
        t = Mathf.Abs(fadet - 1);

        imagesValue = new float[images.Length];
        transparentBars230Value = new float[transparentBars230.Length];
        transparentBars135Value = new float[transparentBars135.Length];
        textsValue = new float[texts.Length];

        minDpsBarFillValue = minDpsBar.fillAmount;
        maxDpsBarFillValue = maxDpsBar.fillAmount;
        minHpsBarFillValue = minHpsBar.fillAmount;
        maxHpsBarFillValue = maxHpsBar.fillAmount;
        minSurviBarFillValue = minSurviBar.fillAmount;
        maxSurviBarFillValue = maxSurviBar.fillAmount;

        hpsValue = radarHpsValue;
        surviValue = radarSurviValue;
        stabilityValue = radarStabilityValue;
        rangeValue = radarRangeValue;
        dpsValue = radarDpsValue;

        ColorRValue = radarColorR;
        ColorGValue = radarColorG;
        ColorBValue = radarColorB;

        GetRole();
        GetClass();

        for (int index = 0; index < images.Length; index++)
        {
            imagesValue[index] = images[index].GetComponent<Image>().color.a;
        }
        for (int index = 0; index < transparentBars230.Length; index++)
        {
            transparentBars230Value[index] = transparentBars230[index].GetComponent<Image>().color.a;
        }
        for (int index = 0; index < transparentBars135.Length; index++)
        {
            transparentBars135Value[index] = transparentBars135[index].GetComponent<Image>().color.a;
        }
        for (int index = 0; index < texts.Length; index++)
        {
            textsValue[index] = texts[index].GetComponent<Text>().color.a;
        }
        materialValue = materialTexture.color.a;

        showingPanel = true;
        hidingPanel = false;
        generatingChart = true;
    }

    public void HidePanel()
    {
        fadet = Mathf.Abs(t - 1);

        imagesValue = new float[images.Length];
        transparentBars230Value = new float[transparentBars230.Length];
        transparentBars135Value = new float[transparentBars135.Length];
        textsValue = new float[texts.Length];

        for (int index = 0; index < images.Length; index++)
        {
            imagesValue[index] = images[index].GetComponent<Image>().color.a;
        }
        for (int index = 0; index < transparentBars230.Length; index++)
        {
            transparentBars230Value[index] = transparentBars230[index].GetComponent<Image>().color.a;
        }
        for (int index = 0; index < transparentBars135.Length; index++)
        {
            transparentBars135Value[index] = transparentBars135[index].GetComponent<Image>().color.a;
        }
        for (int index = 0; index < texts.Length; index++)
        {
            textsValue[index] = texts[index].GetComponent<Text>().color.a;
        }
        materialValue = materialTexture.color.a;

        hidingPanel = true;
        showingPanel = false;
    }

    void Update()
    {
        if (showingPanel == true)
        {
            for (int index = 0; index < images.Length; index++)
            {
                image = images[index].GetComponent<Image>();

                var tempColor = image.color;
                tempColor.a = Mathf.SmoothStep(imagesValue[index], 1f, t);
                image.color = tempColor;
            }
            for (int index = 0; index < transparentBars230.Length; index++)
            {
                image = transparentBars230[index].GetComponent<Image>();

                var tempColor = image.color;
                tempColor.a = Mathf.SmoothStep(transparentBars230Value[index], 0.9019608f, t);
                image.color = tempColor;
            }
            for (int index = 0; index < transparentBars135.Length; index++)
            {
                image = transparentBars135[index].GetComponent<Image>();

                var tempColor = image.color;
                tempColor.a = Mathf.SmoothStep(transparentBars135Value[index], 0.5294118f, t);
                image.color = tempColor;
            }
            for (int index = 0; index < texts.Length; index++)
            {
                text = texts[index].GetComponent<Text>();

                var tempColor = text.color;
                tempColor.a = Mathf.SmoothStep(textsValue[index], 1f, t);
                text.color = tempColor;
            }

            tempMaterial = materialTexture;

            var tempMaterialColor = tempMaterial.color;
            tempMaterialColor.a = Mathf.SmoothStep(materialValue, 1f, t);
            tempMaterial.color = tempMaterialColor;

            t += 1 * Time.deltaTime;

            if (t >= 1)
            {
                t = 1;
                showingPanel = false;
            }
        }

        if (hidingPanel == true)
        {
            for (int index = 0; index < images.Length; index++)
            {
                image = images[index].GetComponent<Image>();

                var tempColor = image.color;
                tempColor.a = Mathf.SmoothStep(imagesValue[index], 0, fadet);
                image.color = tempColor;
            }
            for (int index = 0; index < transparentBars230.Length; index++)
            {
                image = transparentBars230[index].GetComponent<Image>();

                var tempColor = image.color;
                tempColor.a = Mathf.SmoothStep(transparentBars230Value[index], 0, fadet);
                image.color = tempColor;
            }
            for (int index = 0; index < transparentBars135.Length; index++)
            {
                image = transparentBars135[index].GetComponent<Image>();

                var tempColor = image.color;
                tempColor.a = Mathf.SmoothStep(transparentBars135Value[index], 0, fadet);
                image.color = tempColor;
            }
            for (int index = 0; index < texts.Length; index++)
            {
                text = texts[index].GetComponent<Text>();

                var tempColor = text.color;
                tempColor.a = Mathf.SmoothStep(textsValue[index], 0, fadet);
                text.color = tempColor;
            }

            var tempMaterialColor = tempMaterial.color;
            tempMaterialColor.a = Mathf.SmoothStep(materialValue, 0f, fadet);
            tempMaterial.color = tempMaterialColor;

            fadet += 1 * Time.deltaTime;

            if (fadet >= 1)
            {
                fadet = 1;
                hidingPanel = false;
            }
        }

        if(generatingChart == true)
        {
            GenerateChart();
        }
    }

    void GetRole()
    {
        if(playerData.GetPlayerRole() == "Tank")
        {
            roleIcon.GetComponent<Image>().sprite = tankSprite;
        }
        else if((playerData.GetPlayerRole() == "Dps") && (playerData.GetPlayerProximity() == "Melee"))
        {
            roleIcon.GetComponent<Image>().sprite = mDpsSprite;
        }
        else if((playerData.GetPlayerRole() == "Dps") && (playerData.GetPlayerProximity() == "Ranged"))
        {
            roleIcon.GetComponent<Image>().sprite = rDpsSprite;
        }
        else if((playerData.GetPlayerRole() == "Healer") && (playerData.GetPlayerProximity() == "Melee"))
        {
            roleIcon.GetComponent<Image>().sprite = mHpsSprite;
        }
        else if((playerData.GetPlayerRole() == "Healer") && (playerData.GetPlayerProximity() == "Ranged"))
        {
            roleIcon.GetComponent<Image>().sprite = rHpsSprite;
        }
    }

    void GetClass()
    {
        Image tempClassBar;

        if (playerData.GetPlayerClass() == "Priest")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = priestS;
            classBar.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Warlock")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = warlockS;
            classBar.GetComponent<Image>().color = new Color32(148, 130, 201, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Mage")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = mageS;
            classBar.GetComponent<Image>().color = new Color32(105, 204, 240, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Druid")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = druidS;
            classBar.GetComponent<Image>().color = new Color32(255, 125, 010, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Rogue")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = rogueS;
            classBar.GetComponent<Image>().color = new Color32(255, 245, 105, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Monk")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = monkS;
            classBar.GetComponent<Image>().color = new Color32(000, 255, 150, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Demon Retard")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = demonRetardS;
            classBar.GetComponent<Image>().color = new Color32(163, 048, 201, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Shaman")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = shamanS;
            classBar.GetComponent<Image>().color = new Color32(000, 112, 222, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Hunter")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = hunterS;
            classBar.GetComponent<Image>().color = new Color32(171, 212, 115, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Death Knight")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = deathKnightS;
            classBar.GetComponent<Image>().color = new Color32(196, 031, 059, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Warrior")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = warriorS;
            classBar.GetComponent<Image>().color = new Color32(199, 156, 110, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
        else if (playerData.GetPlayerClass() == "Paladin")
        {
            classBarValue = classBar.GetComponent<Image>().color.a;

            classIcon.GetComponent<Image>().sprite = paladinS;
            classBar.GetComponent<Image>().color = new Color32(245, 140, 186, 255);

            tempClassBar = classBar.GetComponent<Image>();

            var tempClassBarColor = tempClassBar.color;

            tempClassBarColor.a = classBarValue;
            tempClassBar.color = tempClassBarColor;
        }
    }

    float GetMaxDps()
    {
        float[] playerDpsBank = allPlayers.players[0].GetDps();
        float maxDpsValue = playerDpsBank[0];

        for (int index = 0; index < allPlayers.players.Length; index++)
        {
            while ((allPlayers.players[index] == null) && (index < (allPlayers.players.Length - 1)))
            {
                index++;
            }
            if ((index == allPlayers.players.Length - 1) && (allPlayers.players[index] == null))
            {
                break;
            }

            playerDpsBank = allPlayers.players[index].GetDps();

            for (int calculateIndex = 0; calculateIndex < playerDpsBank.Length; calculateIndex++)
            {
                if (playerDpsBank[calculateIndex] > maxDpsValue)
                {
                    maxDpsValue = playerDpsBank[calculateIndex];
                }
            }
        }

        return maxDpsValue;
    }

    float GetMaxHps()
    {
        float[] playerHpsBank = allPlayers.players[0].GetHps();
        float maxHpsValue = playerHpsBank[0];

        for (int index = 0; index < allPlayers.players.Length; index++)
        {
            while ((allPlayers.players[index] == null) && (index < (allPlayers.players.Length - 1)))
            {
                index++;
            }
            if ((index == allPlayers.players.Length - 1) && (allPlayers.players[index] == null))
            {
                break;
            }

            playerHpsBank = allPlayers.players[index].GetHps();

            for (int calculateIndex = 0; calculateIndex < playerHpsBank.Length; calculateIndex++)
            {
                if (playerHpsBank[calculateIndex] > maxHpsValue)
                {
                    maxHpsValue = playerHpsBank[calculateIndex];
                }
            }
        }

        return maxHpsValue;
    }

    float GetMaxParses()
    {
        float[] playerParsesBank = allPlayers.players[0].GetDps();
        float parsesValue = playerParsesBank.Length;

        for (int index = 0; index < allPlayers.players.Length; index++)
        {
            while ((allPlayers.players[index] == null) && (index < (allPlayers.players.Length - 1)))
            {
                index++;
            }
            if ((index == allPlayers.players.Length - 1) && (allPlayers.players[index] == null))
            {
                break;
            }

            playerParsesBank = allPlayers.players[index].GetDps();

            if (playerParsesBank.Length > parsesValue)
            {
                parsesValue = playerParsesBank.Length;
            }
        }

        return parsesValue;
    }

    void GenerateChart()
    {
        minDpsBar.fillAmount = Mathf.SmoothStep(minDpsBarFillValue, (CurrentPlayerMinDps() / maxDpsAnchor), chartTimer);
        maxDpsBar.fillAmount = Mathf.SmoothStep(maxDpsBarFillValue, (CurrentPlayerMaxDps() / maxDpsAnchor), chartTimer);
        minHpsBar.fillAmount = Mathf.SmoothStep(minHpsBarFillValue, (CurrentPlayerMinHps() / maxHpsAnchor), chartTimer);
        maxHpsBar.fillAmount = Mathf.SmoothStep(maxHpsBarFillValue, (CurrentPlayerMaxHps() / maxHpsAnchor), chartTimer);
        minSurviBar.fillAmount = Mathf.SmoothStep(minSurviBarFillValue, (CurrentPlayerMinSurvi() / 100), chartTimer);
        maxSurviBar.fillAmount = Mathf.SmoothStep(maxSurviBarFillValue, (CurrentPlayerMaxSurvi() / 100), chartTimer);

        radarHpsValue = (Mathf.SmoothStep(hpsValue, (CurrentPlayerAvrgHps() / maxHpsAnchor), chartTimer));
        radarSurviValue = (Mathf.SmoothStep(surviValue, (CurrentPlayerAvrgSurvi() / 100), chartTimer));
        radarStabilityValue = (Mathf.SmoothStep(stabilityValue, CurrentPlayerStability(), chartTimer));
        radarRangeValue = (Mathf.SmoothStep(rangeValue, (CurrentPlayerRange()), chartTimer));
        radarDpsValue = (Mathf.SmoothStep(dpsValue, (CurrentPlayerAvrgDps() / maxDpsAnchor), chartTimer));

        radarColorR = Mathf.SmoothStep(ColorRValue, GetClassColorR(), chartTimer);
        radarColorG = Mathf.SmoothStep(ColorGValue, GetClassColorG(), chartTimer);
        radarColorB = Mathf.SmoothStep(ColorBValue, GetClassColorB(), chartTimer);

        var tempMaterialColor = tempMaterial.color;
        tempMaterialColor.r = radarColorR;
        tempMaterialColor.g = radarColorG;
        tempMaterialColor.b = radarColorB;
        tempMaterial.color = tempMaterialColor;

        Stats stats = new Stats(radarRangeValue, radarHpsValue, radarStabilityValue, radarSurviValue, radarDpsValue);

        uiStatsRadarChart.SetStats(stats);

        chartTimer += 1 * Time.deltaTime;

        if (chartTimer >= 2)
        {
            generatingChart = false;
        }
    }

    float CurrentPlayerMinDps()
    {
        float[] playerDpsBank = playerData.GetDps();
        float playerMinDpsValue = playerDpsBank[0];

        for (int index = 0; index < playerDpsBank.Length; index++)
        {
            if (playerDpsBank[index] < playerMinDpsValue)
            {
                playerMinDpsValue = playerDpsBank[index];
            }
        }

        return playerMinDpsValue;
    }

    float CurrentPlayerMaxDps()
    {
        float[] playerDpsBank = playerData.GetDps();
        float playerMaxDpsValue = playerDpsBank[0];

        for (int index = 0; index < playerDpsBank.Length; index++)
        {
            if (playerDpsBank[index] > playerMaxDpsValue)
            {
                playerMaxDpsValue = playerDpsBank[index];
            }
        }

        return playerMaxDpsValue;
    }

    float CurrentPlayerMinHps()
    {
        float[] playerHpsBank = playerData.GetHps();
        float playerMinHpsValue = playerHpsBank[0];

        for (int index = 0; index < playerHpsBank.Length; index++)
        {
            if (playerHpsBank[index] < playerMinHpsValue)
            {
                playerMinHpsValue = playerHpsBank[index];
            }
        }

        return playerMinHpsValue;
    }

    float CurrentPlayerMaxHps()
    {
        float[] playerHpsBank = playerData.GetHps();
        float playerMaxHpsValue = playerHpsBank[0];

        for (int index = 0; index < playerHpsBank.Length; index++)
        {
            if (playerHpsBank[index] > playerMaxHpsValue)
            {
                playerMaxHpsValue = playerHpsBank[index];
            }
        }

        return playerMaxHpsValue;
    }

    float CurrentPlayerMinSurvi()
    {
        float[] playerSurviBank = playerData.GetSurvi();
        float playerMinSurviValue = playerSurviBank[0];

        for (int index = 0; index < playerSurviBank.Length; index++)
        {
            if (playerSurviBank[index] < playerMinSurviValue)
            {
                playerMinSurviValue = playerSurviBank[index];
            }
        }

        return playerMinSurviValue;
    }

    float CurrentPlayerMaxSurvi()
    {
        float[] playerSurviBank = playerData.GetSurvi();
        float playerMaxSurviValue = playerSurviBank[0];

        for (int index = 0; index < playerSurviBank.Length; index++)
        {
            if (playerSurviBank[index] > playerMaxSurviValue)
            {
                playerMaxSurviValue = playerSurviBank[index];
            }
        }

        return playerMaxSurviValue;
    }

    float CurrentPlayerAvrgDps()
    {
        float[] playerDpsBank = playerData.GetDps();
        float playerAvrgDpsValue = 0;

        for (int index = 0; index < playerDpsBank.Length; index++)
        {
            playerAvrgDpsValue += playerDpsBank[index];
        }
        playerAvrgDpsValue = playerAvrgDpsValue / playerDpsBank.Length;

        return playerAvrgDpsValue;
    }

    float CurrentPlayerAvrgHps()
    {
        float[] playerHpsBank = playerData.GetHps();
        float playerAvrgHpsValue = 0;

        for (int index = 0; index < playerHpsBank.Length; index++)
        {
            playerAvrgHpsValue += playerHpsBank[index];
        }
        playerAvrgHpsValue = playerAvrgHpsValue / playerHpsBank.Length;

        return playerAvrgHpsValue;
    }

    float CurrentPlayerAvrgSurvi()
    {
        float[] playerSurviBank = playerData.GetSurvi();
        float playerAvrgSurviValue = 0;

        for (int index = 0; index < playerSurviBank.Length; index++)
        {
            playerAvrgSurviValue += playerSurviBank[index];
        }
        playerAvrgSurviValue = playerAvrgSurviValue / playerSurviBank.Length;

        return playerAvrgSurviValue;
    }

    float CurrentPlayerStability()
    {
        float[] playerStabilityBank = playerData.GetDps();
        float playerStabilityValue = 0;

        playerStabilityValue = (playerStabilityBank.Length / maxParsesAnchor);

        return playerStabilityValue;
    }

    float CurrentPlayerRange()
    {
        float minValue = 0;
        float maxValue = 0;
        float playerRange = 0;

        if ((playerData.GetPlayerRole() == "Dps") || (playerData.GetPlayerRole() == "Tank"))
        {
            minValue = CurrentPlayerMinDps();
            maxValue = CurrentPlayerMaxDps();

            playerRange = (maxValue - minValue) / maxDpsAnchor;
        }
        else if (playerData.GetPlayerRole() == "Healer")
        {
            minValue = CurrentPlayerMinHps();
            maxValue = CurrentPlayerMaxHps();

            playerRange = (maxValue - minValue) / maxHpsAnchor;
        }

        return playerRange;
    }

    float GetClassColorR()
    {
        float colorR = 0;
        if (playerData.GetPlayerClass() == "Priest")
        {
            colorR = 255;
        }
        else if (playerData.GetPlayerClass() == "Warlock")
        {
            colorR = 148;
        }
        else if (playerData.GetPlayerClass() == "Mage")
        {
            colorR = 105;
        }
        else if (playerData.GetPlayerClass() == "Druid")
        {
            colorR = 255;
        }
        else if (playerData.GetPlayerClass() == "Rogue")
        {
            colorR = 255;
        }
        else if (playerData.GetPlayerClass() == "Monk")
        {
            colorR = 000;
        }
        else if (playerData.GetPlayerClass() == "Demon Retard")
        {
            colorR = 163;
        }
        else if (playerData.GetPlayerClass() == "Shaman")
        {
            colorR = 000;
        }
        else if (playerData.GetPlayerClass() == "Hunter")
        {
            colorR = 171;
        }
        else if (playerData.GetPlayerClass() == "Death Knight")
        {
            colorR = 196;
        }
        else if (playerData.GetPlayerClass() == "Warrior")
        {
            colorR = 199;
        }
        else if (playerData.GetPlayerClass() == "Paladin")
        {
            colorR = 245;
        }

        colorR = (colorR / 255);

        return colorR;
    }
    
    float GetClassColorG()
    {
        float colorG = 0;
        if (playerData.GetPlayerClass() == "Priest")
        {
            colorG = 255;
        }
        else if (playerData.GetPlayerClass() == "Warlock")
        {
            colorG = 130;
        }
        else if (playerData.GetPlayerClass() == "Mage")
        {
            colorG = 204;
        }
        else if (playerData.GetPlayerClass() == "Druid")
        {
            colorG = 125;
        }
        else if (playerData.GetPlayerClass() == "Rogue")
        {
            colorG = 245;
        }
        else if (playerData.GetPlayerClass() == "Monk")
        {
            colorG = 255;
        }
        else if (playerData.GetPlayerClass() == "Demon Retard")
        {
            colorG = 048;
        }
        else if (playerData.GetPlayerClass() == "Shaman")
        {
            colorG = 112;
        }
        else if (playerData.GetPlayerClass() == "Hunter")
        {
            colorG = 212;
        }
        else if (playerData.GetPlayerClass() == "Death Knight")
        {
            colorG = 031;
        }
        else if (playerData.GetPlayerClass() == "Warrior")
        {
            colorG = 156;
        }
        else if (playerData.GetPlayerClass() == "Paladin")
        {
            colorG = 140;
        }

        colorG = (colorG / 255);

        return colorG;
    }
    
    float GetClassColorB()
    {
        float colorB = 0;
        if (playerData.GetPlayerClass() == "Priest")
        {
            colorB = 255;
        }
        else if (playerData.GetPlayerClass() == "Warlock")
        {
            colorB = 201;
        }
        else if (playerData.GetPlayerClass() == "Mage")
        {
            colorB = 240;
        }
        else if (playerData.GetPlayerClass() == "Druid")
        {
            colorB = 010;
        }
        else if (playerData.GetPlayerClass() == "Rogue")
        {
            colorB = 105;
        }
        else if (playerData.GetPlayerClass() == "Monk")
        {
            colorB = 150;
        }
        else if (playerData.GetPlayerClass() == "Demon Retard")
        {
            colorB = 201;
        }
        else if (playerData.GetPlayerClass() == "Shaman")
        {
            colorB = 222;
        }
        else if (playerData.GetPlayerClass() == "Hunter")
        {
            colorB = 115;
        }
        else if (playerData.GetPlayerClass() == "Death Knight")
        {
            colorB = 059;
        }
        else if (playerData.GetPlayerClass() == "Warrior")
        {
            colorB = 110;
        }
        else if (playerData.GetPlayerClass() == "Paladin")
        {
            colorB = 186;
        }

        colorB = (colorB / 255);

        return colorB;
    }
}
