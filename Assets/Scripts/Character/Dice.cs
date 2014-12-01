using UnityEngine;
using System.Collections;

public static class Dice
// Simple class to easily RNG some numbers, and to control the RNG.
{

		public static int d20 ()
		{
				return Random.Range (1, 20);
		}

		public static int d10 ()
		{
				return Random.Range (1, 10);
		}
}
