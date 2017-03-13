using System;
using System.Collections.Generic;
using System.Linq;
using Dream.Space.Data.Entities.Indicators;
using Dream.Space.Data.Enums;
using Dream.Space.Indicators;
using Dream.Space.Reader.Models;

namespace Dream.Space.Calculators
{

    public class NHNLCalculator : IIndicatorCalculator<NHNLModel>, IIndicatorCalculatorAny
    {
        private readonly NHNL _calculator;

        public NHNLCalculator()
        {
            _calculator = new NHNL();
        }

        public bool CanCalculate(Indicator indicator)
        {
            return string.Compare(indicator.Name, _calculator.Name, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public List<NHNLModel> Calculate(Indicator indicator, List<QuotesModel> quotes)
        {
            Validate(indicator, quotes);

            return _calculator.Calculate(quotes, ExtractPeriod(indicator));
        }


        public void Validate(Indicator indicator, List<QuotesModel> quotes)
        {
            if (!CanCalculate(indicator))
            {
                throw new NotSupportedException($"Calculator '{_calculator.Name}' does not support indicator '{indicator.Name}'");
            }

            var param = indicator.Params.FirstOrDefault(p => p.ParamName == IndicatorParamName.Period.ToString());
            if (param == null || param.Value == 0)
            {
                throw new ArgumentException($"Period parameter value is not set. Params: {indicator.JsonParams}");
            }
        }

        private int ExtractPeriod(Indicator indicator)
        {
            var param = indicator.Params.First(p => p.ParamName == IndicatorParamName.Period.ToString());
            return param.Value;
        }
    }

    public interface IIndicatorCalculatorAny
    {
    }
}