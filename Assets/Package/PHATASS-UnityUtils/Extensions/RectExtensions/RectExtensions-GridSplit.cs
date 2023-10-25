using Rect = UnityEngine.Rect;
using Vector2 = UnityEngine.Vector2;
using Debug = UnityEngine.Debug;

namespace PHATASS.Utils.Extensions
{
//Extension methods for UnityEngine.Rect
//	Grid splitting method
	public static partial class RectExtensions
	{
//public static space
	//splits a Rect into a grid. returns an array of rows, each entry being an array of columns
		//returns Rect[row,column]
		//takes two float arrays defining each column's width and each row's height
		//will contain as many columns and rows as values contained in columns and rows arrays
		//
		//positive column/row size requirements will be allocated as requested, even if it exceeds initial rect
		//negative column/row sizes will automatically split proportionally to their negative value
		//warning: behaviour is undefined if positive (fixed) sizes exceed initial rect while including negative (auto) size requirements in the list
	public static Rect[,] EGridSplitRect (
			this Rect self,
			float[] columns = null,
			float[] rows = null,
			float columnMargin = 0f,	//margin between each row/column
			float rowMargin = 0f
		) {
			if (columns == null) { columns = new float[1] {-1f}; }
			if (rows == null) { rows = new float[1] {-1f}; }

			//initialize a results grid
			Rect[,] returnGrid = new Rect[rows.Length, columns.Length];

			//calculate row and column size split
			float[] columnWidths = SplitSizes(self.width, columns, columnMargin);
			float[] rowHeights = SplitSizes(self.height, rows, rowMargin);

			//initialize positions
			float posX = self.xMin;
			float posY = self.yMin;

			//iterate through rows and columns creating required rects
			for (int row = 0, rowLimit = rowHeights.Length; row < rowLimit; row++)
			{
				for (int column = 0, columnLimit = columnWidths.Length; column < columnLimit; column++)
				{
					//create new rect with required dimensions
					returnGrid[row, column] = new Rect(
						x: posX,
						y: posY,
						width: columnWidths[column],
						height: rowHeights[row]
					);
					//advance column position
					posX += columnWidths[column] + columnMargin;
				}
				//reset column and advance row positions
				posX = self.xMin;
				posY += rowHeights[row] + rowMargin;
			}

			//return results
			return returnGrid;
		}
//ENDOF public static space

//private static space
	//returns an array of floats according to splitRequirements
		//splitRequirements >= 0 will equal received value
		//splitRequirements < 0 will proportionally split free space
		private static float[] SplitSizes (float baseSize, float[] splitRequirements, float margin)
		{
			float requiredSizeSum = GetSumPredefinedRequirements(splitRequirements, margin);

		/* //size check removed because unity keeps providing negatively-sized rects
			//check if size requirements do not exceed alloted size
			if (requiredSizeSum > baseSize)
			{ Debug.LogError(string.Format("RectExtensions.SplitSizes(baseSize, splitRequirements): splitRequirements sum exceeds available baseSize. baseSize: {0}, requiredSizeSum: {1}", baseSize, requiredSizeSum)); }
			//{ throw new System.ArgumentException(string.Format("RectExtensions.SplitSizes(baseSize, splitRequirements): splitRequirements sum exceeds available baseSize. baseSize: {0}, requiredSizeSum: {1}", baseSize, requiredSizeSum), "baseSize"); }
		//*/
			//allocate a new array so as to not alter entrant data
			float[] splitResults = (float[]) splitRequirements.Clone();

			//calculate available free size per free unit
			float freeUnitSize =
				(baseSize - requiredSizeSum)
				/ GetSumFreeRequirements(splitRequirements, 0);

			//find each free sized element and split available size between them
			for (int i = 0, iLimit = splitResults.Length; i < iLimit; i++)
			{
				if (splitResults[i] < 0)
				{ splitResults[i] = freeUnitSize * splitResults[i] * -1; }
			}

			return splitResults;
		}

		//sums every value above zero within a float array, returns the total sum. Adds margin between each
		private static float GetSumPredefinedRequirements (float[] splitRequirements, float margin)
		{
			return GetPositiveScaledSum(1f, splitRequirements, margin);
		}

		//sums the total of free units from negative values
		private static float GetSumFreeRequirements (float[] splitRequirements, float margin)
		{
			return GetPositiveScaledSum(-1f, splitRequirements, margin);
		}

		//sums every entry with value above 0 after scaling by scale
		private static float GetPositiveScaledSum (float scale, float[] splitRequirements, float margin)
		{
			float sum = 0;
			float scaledMargin = margin * scale;

			//sum every requirement and margin
			foreach (float requirement in splitRequirements)
			{
				float scaledRequirement = requirement * scale;
				if (scaledRequirement > 0) { sum += scaledRequirement; }
				if (scaledMargin > 0) { sum += scaledMargin; }
			}

			//remove surplus margin
			if (splitRequirements.Length > 0 && scaledMargin > 0)
			{ sum -= scaledMargin; }

			return sum;
		}
//ENDOF private static space
	}
}