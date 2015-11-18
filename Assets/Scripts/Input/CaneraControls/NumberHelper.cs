using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ManagerInput.CameraControls
{
	public static class NumberHelper
	{
		public static float GetCloserFloatInList (float maxNumberInList,float currentValue, List<float> list)
		{
			float minNumber = 0F;
			float maxNumber = 0F;
			GetMinAndMaxNumber (maxNumberInList,currentValue, list, out minNumber, out maxNumber);
			float minCloser = currentValue - minNumber;
			float maxCloser = maxNumber - currentValue;
			return minCloser < maxCloser ? minNumber : maxNumber;
		}

		private static void GetMinAndMaxNumber (float maxNumberInList,float currentDegress,List<float> list , out float minNumber, out float maxNumber)
		{
			minNumber = 0;
			maxNumber = 0;
			list.Sort((a, b) => a.CompareTo(b));

			for (int i = 0; i < list.Count; i++) 
			{
				if(i == list.Count-1)
				{
					maxNumber = maxNumberInList;
					minNumber = list[i];
				}

				if (list [i] > currentDegress) 
				{
					maxNumber = list [i];
					minNumber = i-1 < 0 ? list [i] : list [i - 1];
					break;
				}
			}
		}
	}
	
}
