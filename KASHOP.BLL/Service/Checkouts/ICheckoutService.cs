using KASHOP.DAL.DTO.Request.Checkouts;
using KASHOP.DAL.DTO.Response.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service.Checkouts
{
    public interface ICheckoutService
    {
        Task<CheckoutResponse> ProcessCheckout(string userId, CheckoutRequest request);
        Task<CheckoutResponse> HandleSuccess(string sessionId);
    }
}
