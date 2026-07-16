using hardwareAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hardwareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }


        // GET: api/billing/invoices
        [HttpGet("invoices")]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _billingService.GetInvoicesAsync();

            return Ok(invoices);
        }


        // GET: api/billing/invoices/1
        [HttpGet("invoices/{saleId}")]
        public async Task<IActionResult> GetInvoice(int saleId)
        {
            var invoice = await _billingService.GetInvoiceAsync(saleId);

            if (invoice == null)
            {
                return NotFound(new
                {
                    message = "Invoice not found."
                });
            }

            return Ok(invoice);
        }
    }
}