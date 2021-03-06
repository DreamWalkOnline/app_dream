﻿using System.Collections.Generic;
using System.Linq;
using Dream.Space.Models.Indicators;
using Dream.Space.Models.Quotes;

namespace Dream.Space.Indicators
{
    /*

     Most moving averages are based on closing prices. 
     A 5-day simple moving average is the five day sum of closing prices divided by five
    
    */


    public class SMA : IIndicator<IndicatorResult, int>
    {
        public string Name => "SMA";

        public List<IndicatorResult> Calculate(List<QuotesModel> quotes, int period)
        {
            if (!Validate(quotes, period))
            {
                return null;
            }

            var result = new List<IndicatorResult>();
            var ordered = quotes.OrderBy(q => q.Date).ToList();

            for (var index = 0; index < quotes.Count-period; index++)
            {
                var periodQuotes = ordered.Skip(index).Take(period).ToList();
                var item = periodQuotes.Last();

                result.Insert(0, new IndicatorResult(item.Date) { Value = periodQuotes.Sum(q => q.Close) / period });
            }

            return result;
        }

        private bool Validate(List<QuotesModel> quotes, int period)
        {
            if (period < 2 || quotes == null || quotes.Count <= period)
            {
                return false;
            }
            return true;
        }


    }
}
