using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

public class Wallet : BetterMonoBehaviour
{
	private Text txtWallet;
	private float currentMoney;
	private void Awake()
	{
		txtWallet = GetComponent<Text>();
		RentListener();
	}

	private void RentListener()
	{
		EventManager.Instance.AddListener<CollectRentEvent>(OnCollectRent);
	}

	private void OnCollectRent(CollectRentEvent collectRentEvent)
	{
		IncreaseMoney(collectRentEvent.rent);
	}
        
	private void IncreaseMoney(float increaseAmount)
	{
		currentMoney   += increaseAmount;
		txtWallet.text =  "Money : " + currentMoney;
		print(currentMoney);
	}
}
