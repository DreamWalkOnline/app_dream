﻿using System.Collections.Generic;
using Dream.Space.Data.Entities.Indicators;
using Dream.Space.Models.Indicators;

namespace Dream.Space.Playground.Models
{
    public class IndicatorChartData
    {
        public Indicator Indicator { get; set; }
        public List<IndicatorModel> IndicatorValues { get; set; }
    }
}