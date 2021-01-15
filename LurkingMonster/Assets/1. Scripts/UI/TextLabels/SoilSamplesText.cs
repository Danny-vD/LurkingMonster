using Events.SoilSamplesManagement;
using Singletons;
using TMPro;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextLabels
{
	public class SoilSamplesText : BetterMonoBehaviour
	{
		private TextMeshProUGUI soilSamplesText;
		
		private void Awake()
		{
			soilSamplesText = GetComponent<TextMeshProUGUI>();
		}

		private void Start()
		{
			SetText();
		}

		private void SetText()
		{
			soilSamplesText.SetText($"{MoneyManager.Instance.CurrentSoilSamples:N0}");
		}
		
		private void AddListeners()
		{
			EventManager.Instance.AddListener<IncreaseSoilSamplesEvent>(SetText);
			EventManager.Instance.AddListener<DecreaseSoilSamplesEvent>(SetText);
		}

		private void RemoveListeners()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			EventManager.Instance.RemoveListener<IncreaseSoilSamplesEvent>(SetText);
			EventManager.Instance.RemoveListener<DecreaseSoilSamplesEvent>(SetText);
		}
		
		private void OnEnable()
		{
			AddListeners();
			SetText();
		}

		private void OnDisable()
		{
			RemoveListeners();
		}
	}
}