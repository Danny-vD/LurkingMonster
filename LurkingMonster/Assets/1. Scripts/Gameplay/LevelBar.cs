using System;
using UnityEngine.UI;
using VDFramework;

namespace Gameplay
{
	public class LevelBar : BetterMonoBehaviour
	{
		private int level;
		private int experience;
		private int experienceToNextLevel;
		
		public Slider Slider;
		public Text TextLevel;
		
		public void Awake()
		{
			level                 = 0;
			experience            = 0;
			experienceToNextLevel = 200;
			Slider.value          = experience;
			Slider.maxValue       = experienceToNextLevel;
			TextLevel.text        = "Level : " + level;
		}

		
		//Todo test purposes
		private void Update()
		{
			//AddExperience(20);
		}

		public void AddExperience(int amount)
		{
			experience += amount;

			if (experience >= experienceToNextLevel)
			{
				level++;
				TextLevel.text        =  "Level : " + level;
				experience            -= experienceToNextLevel;
				
				experienceToNextLevel = experienceToNextLevel * 2;
				Slider.maxValue       = experienceToNextLevel;
			}

			Slider.value = experience;
		}

		public int Level
		{
			get => level;
		}
	}
}