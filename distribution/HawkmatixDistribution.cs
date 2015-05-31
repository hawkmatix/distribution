/*
 * Hawkmatix Distribution 1.0.0.3
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

// User menu choices
public enum Distribution
{
	Gaussian,
	Laplace,
	Logistic
}

// Begin indicator script
namespace NinjaTrader.Indicator
{
	// Description
	[Description("Hawkmatix Distribution solves for the price given a probability.")]

	// Begin class script
	public class HawkmatixDistribution : Indicator
	{
		// Declare class variables
		private int period                = 21;
		private double probability        = 0.0025;
		private Distribution distribution = Distribution.Gaussian;

		// Override Initialize method
		protected override void Initialize()
		{
			// Add plots
			Add(new Plot(new Pen(Color.Green, 3), PlotStyle.Line, "Upper Price"));
			Add(new Plot(new Pen(Color.Red, 3), PlotStyle.Line, "Lower Price"));

			// Default parameters
			CalculateOnBarClose = false;
			MaximumBarsLookBack = MaximumBarsLookBack.Infinite;
			Overlay             = true;
		}

		// Override OnBarUpdate method
		protected override void OnBarUpdate()
		{
			// Protect against too few bars
			if (CurrentBar < period)
			{
				Upper.Reset();
				Lower.Reset();
				return;
			}

			// Solve for the price given a probability
			if (distribution == Distribution.Gaussian)
			{
				double side = Math.Sqrt(-2 * Math.Pow(StdDev(period)[0] / TickSize, 2) * Math.Log(probability * StdDev(Period)[0] / TickSize * Math.Sqrt(2 * Math.PI)));
				Upper.Set((SMA(period)[0] / TickSize + side) * TickSize);
				Lower.Set((SMA(period)[0] / TickSize - side) * TickSize);
			}
			else if (distribution == Distribution.Laplace)
			{
				double side = -HawkmatixAvgDiff(period)[0] / TickSize * Math.Log(probability * 2 * HawkmatixAvgDiff(period)[0] / TickSize);
				Upper.Set((SMA(period)[0] / TickSize + side) * TickSize);
				Lower.Set((SMA(period)[0] / TickSize - side) * TickSize);
			}
			else if (distribution == Distribution.Logistic)
			{
				double s      = Math.Sqrt(3) / Math.PI * StdDev(period)[0] / TickSize;
				double left   = 1 - 2 * probability * s;
				double side   = Math.Sqrt((2 * probability * s - 1) * (2 * probability * s - 1) - 4 * probability * probability * s * s);
				double bottom = 2 * probability * s;
				Upper.Set((SMA(period)[0] / TickSize - s * Math.Log((left - side) / bottom)) * TickSize);
				Lower.Set((SMA(period)[0] / TickSize - s * Math.Log((left + side) / bottom)) * TickSize);
			}
		}

		// Properties of plots and inputs
		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Upper
		{
			get { return Values[0]; }
		}

		[Browsable(false)]
		[XmlIgnore()]
		public DataSeries Lower
		{
			get { return Values[1]; }
		}

		[Description("Amount of bars used for calculations.")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Period")]
		public int Period
		{
			get { return period; }
			set { period = Math.Max(1, value); }
		}

		[Description("The probability of price reaching a band.")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Probability")]
		public double Probability
		{
			get { return probability; }
			set
			{
				if (value > 1)
				{
					probability = 1;
				}
				else if (value < 0)
				{
					probability = 0;
				}
				else
				{
					probability = value;
				}
			}
		}

		[Description("Type of probability distribution.")]
		[Category("Parameters")]
		[Gui.Design.DisplayNameAttribute("Distribution")]
		public Distribution Distribution
		{
			get { return distribution; }
			set { distribution = value; }
		}
	}
}
