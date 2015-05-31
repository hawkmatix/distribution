/*
 * Hawkmatix Average Difference 1.0.0.1
 * Official project page: http://www.hawkmatix.com/distribution.html
 *
 * Copyright (C) 2013 Andrew Hawkins
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

// Using declaration
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Data;
using NinjaTrader.Gui.Chart;

// Begin indicator script
namespace NinjaTrader.Indicator
{
	// Description
	[Description("Hawkmatix Average Difference determines the mean of the distance between price and the average.")]

	// Begin class script
	public class HawkmatixAvgDiff : Indicator
	{
		// Declare class variables
		private int period = 21;

		// Override Initialize method
		protected override void Initialize()
		{
			// Add plots
			Add(new Plot(new Pen(Color.Honeydew, 2), PlotStyle.Line, "Average Difference"));

			// Default parameters
			CalculateOnBarClose = false;
			Overlay             = false;
			MaximumBarsLookBack = MaximumBarsLookBack.Infinite;
		}

		// Override OnBarUpdate method
		protected override void OnBarUpdate()
		{
			// Protect against too few bars
			if (CurrentBar < period)
			{
				Value.Reset();
			}

			// Determine the average difference
			double mu         = SMA(period)[0];
			double difference = 0;
			for (int i = Math.Min(CurrentBar, period - 1); i >= 0; i--)
			{
				difference += Math.Abs(Close[i] - mu);
			}
			Value.Set(difference / period);
		}

		// Properties of plots and inputs
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Value
		{
			get { return Values[0]; }
		}

		[Description("Amount of bars used for calculations.")]
		[GridCategory("Parameters")]
		public int Period
		{
			get { return period; }
			set { period = Math.Max(1, value); }
		}
	}
}
