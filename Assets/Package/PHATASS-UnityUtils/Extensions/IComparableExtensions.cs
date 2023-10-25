using System;
using System.Collections.Generic;

#nullable disable
namespace PHATASS.Utils.Extensions
{
	//Utility methods for IComparable<T> implementing classes
	public static class IComparableExtensions
	{
		//returns the smallest value within a list of comparable values
		public static IComparable<T> EMinimum<T> (this IEnumerable<IComparable<T>> comparableList)
		{
			IComparable<T> minimum = null;

			//iterate over the list, storing the smallest found element
			foreach (IComparable<T> comparable in comparableList)
			{
				//if we had no minimum, store the new value
				if (minimum == null)
				{
					minimum = comparable;
					continue;
				}

				//if current comparable doesn't exist, ignore it
				if (comparable == null) 
				{ continue;	}

				//if current comparable is smaller than previous minimum, store it
				if (comparable.CompareTo((T) minimum) < 0)
				{ minimum = comparable;	}
			}

			return minimum;
		}

		//returns the smallest value within a list of comparable values
		public static IComparable<T> EMaximum<T> (this IEnumerable<IComparable<T>> comparableList)
		{
			IComparable<T> maximum = null;

			//iterate over the list, storing the smallest found element
			foreach (IComparable<T> comparable in comparableList)
			{
				//if we had no maximum, store the new value
				if (maximum == null)
				{
					maximum = comparable;
					continue;
				}

				//if current comparable doesn't exist, ignore it
				if (comparable == null) 
				{ continue;	}

				//if current comparable is larger than previous minimum, store it
				if (comparable.CompareTo((T) maximum) > 0)
				{ maximum = comparable;	}
			}

			return maximum;
		}
	}
}
