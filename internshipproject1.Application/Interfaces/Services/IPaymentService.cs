using internshipproject1.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Services
{
    public interface IPaymentService
    {
        Task ProcessPaymentAsync(PaymentEventDTO dto, CancellationToken cancellationToken);
    }
}
