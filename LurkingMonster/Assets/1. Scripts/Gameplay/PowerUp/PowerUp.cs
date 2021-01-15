using Enums;

namespace Gameplay
{
	public class PowerUp
	{
		private bool isActive;
		private float timer;
		private string name;
		private PowerUpType powerUpType;
		private int price;

		public PowerUp(bool isActive, float timer, string name, PowerUpType powerUpType, int price)
		{
			this.isActive    = isActive;
			this.timer       = timer;
			this.name        = name;
			this.powerUpType = powerUpType;
			this.price       = price;
		}

		public bool IsActive
		{
			get => isActive;
			set => isActive = value;
		}

		public float Timer
		{
			get => timer;
			set => timer = value;
		}

		public string Name
		{
			get => name;
			set => name = value;
		}

		public PowerUpType PowerUpType
		{
			get => powerUpType;
			set => powerUpType = value;
		}

		public int Price
		{
			get => price;
			set => price = value;
		}
	}
}