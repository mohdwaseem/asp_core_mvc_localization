using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CultureDemo.Models;
using CultureDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CultureDemo.Controllers
{
    public class ServiceController : Controller
    {
        private readonly CultureDemoContext _cultureDemoContext;
        public ServiceController(CultureDemoContext cultureDemoContext)
        {
            _cultureDemoContext = cultureDemoContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ServiceProfiles()
        {
            var result = await GetServices();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> AddServiceProfile(int csid)
        {
            var contractService = await _cultureDemoContext.ContractService.FirstOrDefaultAsync(x => x.ContracServiceId == csid);
            var authorities = await _cultureDemoContext.Authority.ToListAsync();
            var banks = await _cultureDemoContext.Bank.ToListAsync();
            var service = await _cultureDemoContext.Service.ToListAsync();
            var serviceProfileVM = new ServiceProfileVM();
            if (contractService!=null)
            {
                serviceProfileVM.AuthorityList = new SelectList(authorities, "AuthorityId", "AuthorityName", contractService.AuthorityId);
                serviceProfileVM.ServiceList = new SelectList(service, "ServiceId", "ServiceName", contractService.ServiceId);
                serviceProfileVM.BankList = new SelectList(banks, "BankId", "BankName", contractService.BankId);
                serviceProfileVM.Title = contractService.Title;
            }
            else
            {
                serviceProfileVM.AuthorityList = new SelectList(authorities, "AuthorityId", "AuthorityName");
                serviceProfileVM.ServiceList = new SelectList(service, "ServiceId", "ServiceName");
                serviceProfileVM.BankList = new SelectList(banks, "BankId", "BankName");
            }
            

            
            return View(serviceProfileVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddServiceProfile(ServiceProfileVM serviceProfileVM)
        {
           
           
                ContractService contractService = new ContractService();
                contractService.Title = serviceProfileVM.Title;
                contractService.AuthorityId = serviceProfileVM.AuthorityId;
                contractService.ServiceId = serviceProfileVM.ServiceId;
                contractService.BankId = serviceProfileVM.BankId;
                contractService.IsActive = true;
                _cultureDemoContext.ContractService.Add(contractService);
                await _cultureDemoContext.SaveChangesAsync();
             serviceProfileVM = await GetDropDowns(serviceProfileVM);
            return View(serviceProfileVM);
        }

        private async Task<List<ServiceProfileVM>> GetServices()
        {
            var contractService =await _cultureDemoContext.ContractService.ToListAsync();
            var banks = await _cultureDemoContext.Bank.ToListAsync();
            var authority = await _cultureDemoContext.Authority.ToListAsync();
            var service = await _cultureDemoContext.Service.ToListAsync();

            ServiceProfileVM serviceProfileVM = null;

            List<ServiceProfileVM> serviceProfileVMs = new List<ServiceProfileVM>();

            foreach (var item in contractService)
            {
                serviceProfileVM = new ServiceProfileVM();
                serviceProfileVM.Service = service.FirstOrDefault(x => x.ServiceId == item.ServiceId).ServiceName;
                serviceProfileVM.Authority = authority.FirstOrDefault(x => x.AuthorityId == item.AuthorityId).AuthorityName;
                serviceProfileVM.Bank = banks.FirstOrDefault(x => x.BankId == item.BankId).BankName;
                serviceProfileVM.ContracServiceId = item.ContracServiceId;
                serviceProfileVM.Title = item.Title;
                serviceProfileVMs.Add(serviceProfileVM);
            }
            return serviceProfileVMs;
        }

        private async Task<ServiceProfileVM> GetDropDowns(ServiceProfileVM serviceProfile)
        {
            var authorities = await _cultureDemoContext.Authority.ToListAsync();
            var banks = await _cultureDemoContext.Bank.ToListAsync();
            var service = await _cultureDemoContext.Service.ToListAsync();
            var serviceProfileVM = new ServiceProfileVM();
            serviceProfileVM.AuthorityList = new SelectList(authorities, "AuthorityId", "AuthorityName",serviceProfile.AuthorityId);
            serviceProfileVM.ServiceList = new SelectList(service, "ServiceId", "ServiceName",serviceProfile.ServiceId);
            serviceProfileVM.BankList = new SelectList(banks, "BankId", "BankName",serviceProfile.BankId);

            return serviceProfileVM;
        }
    }
}
