using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharListMenu : MonoBehaviour
{
    // Text fields
    public Text levelText, hitpointText, goldenText, upgradeCostText, xpText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // Character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            // If we went far away
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            // If we went far away
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void onUpgradeClick()
    {
        if(GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    // Update the char info
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "ÌÀÊÑÈÌÓÌ";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        // Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        goldenText.text = GameManager.instance.goldens.ToString();

        // XP
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (GameManager.instance.GetCurrentLevel() == GameManager.instance.xpTable.Count)
        {
            xpText.text = "Âñüîãî " + GameManager.instance.experience.ToString() + " î÷îê äîñâ³äó"; // Display total xp
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }
}
