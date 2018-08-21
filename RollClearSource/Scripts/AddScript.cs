using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddScript : MonoBehaviour {

	ShopManager sm;

		public string zoneId;

		Button m_Button;

		void Start ()
		{
			m_Button = GetComponent<Button>();

			if (m_Button) m_Button.onClick.AddListener(ShowAdPlacement);
		}

		void Update ()
		{
			if (m_Button)
			{
				if (string.IsNullOrEmpty(zoneId)) zoneId = null;
				m_Button.interactable = Advertisement.IsReady(zoneId);
			}
		}

		void ShowAdPlacement ()
		{
			if (string.IsNullOrEmpty(zoneId)) zoneId = null;

			var options = new ShowOptions();
			options.resultCallback = HandleShowResult;

			Advertisement.Show(zoneId, options);
		}

		void HandleShowResult (ShowResult result)
		{
			switch(result)
			{
			case ShowResult.Finished:
				Debug.Log ("Video completed. Offer a reward to the player.");
				break;
			case ShowResult.Skipped:
				Debug.LogWarning("Video was skipped.");
				break;
			case ShowResult.Failed:
				Debug.LogError("Video failed to show.");
				break;
			}
		}
	}