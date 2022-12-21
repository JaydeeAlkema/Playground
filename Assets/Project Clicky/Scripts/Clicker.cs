using TMPro;
using UnityEngine;

public class Clicker : MonoBehaviour
{
	private static Clicker instance;

	[SerializeField] private float totalValue = 0;
	[SerializeField] private Clickable clickable = default;
	[Space]
	[SerializeField] private TextMeshProUGUI totalValueText = default;
	[Space]
	[SerializeField] private Upgrade[] upgrades = default;

	public static Clicker Instance { get => instance; set => instance = value; }

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void Start()
	{
		UpdateMoneyTextElement();
	}

	public void AddValue(float value)
	{
		totalValue += value;
		UpdateMoneyTextElement();
	}

	public void BuyUpgrade(int index)
	{
		Upgrade upgrade = upgrades[index];
		if (totalValue - upgrade.cost >= 0 && upgrade.currentLevel < upgrade.maxLevel)
		{
			totalValue -= upgrade.cost;
			clickable.ClickWorth += upgrade.valueIncrease;
			upgrade.valueIncrease *= upgrade.valueIncreaseMultiplier;

			upgrade.cost *= upgrade.costIncreaseMultiplier;
			upgrade.currentLevel += 1;

			UpdateMoneyTextElement();
		}
		upgrades[index] = upgrade;
	}

	private void UpdateMoneyTextElement()
	{
		totalValueText.text = $"Money: {totalValue.ToString("F2")}$";
	}
}

[System.Serializable]
public struct Upgrade
{
	public float cost;
	[Space]
	public int currentLevel;
	public int maxLevel;
	[Space]
	public float costIncreaseMultiplier;
	public float valueIncreaseMultiplier;
	public float valueIncrease;
}
