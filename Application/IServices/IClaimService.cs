using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Services.ClaimService;

namespace Application.IServices
{
    public interface IClaimService
    {
        ClaimResponse getUserClaim();
    }
}
