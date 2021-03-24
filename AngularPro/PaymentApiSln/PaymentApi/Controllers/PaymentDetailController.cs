using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly PaymentContext _context;

        public PaymentDetailController(PaymentContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            return await _context.paymentDetails.ToListAsync();
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<PaymentDetail>> GetPaymentDetailById(int id)
        {
            var paymentDetail = await _context.paymentDetails.FindAsync(id);
            if(paymentDetail==null)
            {
                return NotFound();
            }
            return paymentDetail;
        }
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.paymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetails",new { id=paymentDetail.PaymentDetailId}, paymentDetail);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletepaymentDetail(int id)
        {
            var paymentDetail = await _context.paymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            _context.paymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutpaymentDetail(int id, PaymentDetail obj)
        {
            if(id!=obj.PaymentDetailId)
            {
                return BadRequest();
            }
            _context.Entry(obj).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if(!PaydetailsExists(id))
                {
                    return NotFound();
                }
                else { throw; }
                
            }
            return NoContent();
        }

        private bool PaydetailsExists(int id)
        {
            return _context.paymentDetails.Any(p => p.PaymentDetailId == id);
        }
    }
}
