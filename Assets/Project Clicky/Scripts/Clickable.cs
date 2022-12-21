using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IClickable
{
	[SerializeField] private float clickWorth = 0.1f;

	public float ClickWorth { get => clickWorth; set => clickWorth = value; }

	public void Click()
	{
		Clicker.Instance.AddValue(clickWorth);
	}

	public void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject()) return;

		transform.localScale = new Vector3(0.98f, 0.98f, 1);

		Click();
	}

	private void OnMouseUp()
	{
		transform.localScale = Vector3.one;

	}
}
