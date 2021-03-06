﻿using Dream.Space.Data.Services;
using Dream.Space.Models.Companies;

namespace Dream.Space.Data.Managers
{
    public class CompanyManager
    {
        private readonly CompanyModel _company;
        private readonly ICompanyService _service;

        internal CompanyManager(CompanyModel company, ICompanyService service)
        {
            _company = company;
            _service = service;
        }

        public void Import()
        {
            _service.Register(_company);
        }

        public void MarkAsSP500()
        {
            _service.MarkAsSP500(_company);
        }
    }
}