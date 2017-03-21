﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Dream.Space.Data.Entities.Strategies;
using Dream.Space.Data.Services;
using Dream.Space.Models.Enums;

namespace Dream.Space.Controllers
{
    [RoutePrefix("api/rule")]
    public class RulesApiController : ApiController
    {
        private readonly IRuleService _ruleService;

        public RulesApiController(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        [ResponseType(typeof(Rule))]
        public async Task<IHttpActionResult> GetRule(int id)
        {
            var rule = await _ruleService.GetRuleAsync(id);

            return Ok(rule);
        }

        [HttpGet]
        [Route("{period:int}/all")]
        [ResponseType(typeof(List<Rule>))]
        public async Task<IHttpActionResult> GetRules(int period)
        {
            var rules = await _ruleService.GetRules((QuotePeriod)period);

            return Ok(rules);
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Rule))]
        public async Task<IHttpActionResult> SaveRule([FromBody] Rule model)
        {

            var rule = await _ruleService.SaveRuleAsync(model);

            return Ok(rule);
        }

        [HttpDelete]
        [Route("{id:int:min(1)}")]
        public async Task<IHttpActionResult> DeleteRule(int id)
        {
            await _ruleService.DeleteRuleAsync(id);
            return Ok();
        }
    }
}
