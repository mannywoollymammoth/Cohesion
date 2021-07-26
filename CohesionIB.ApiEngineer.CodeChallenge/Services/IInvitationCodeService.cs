using Lot.O.Invitation.Codes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CohesionIB.ApiEngineer.CodeChallenge.Services
{
    public interface IInvitationCodeService
    {
        bool HasCode { get; }
        ulong Code { get; }
    }
    /// <summary>
    /// here i noteced that the issue was that enumerables cant be counted efficiently.
    /// im not 100% certain why this is. but if we want a faster way to just check 
    /// if the enumerable isn't empty then we can use the .any() function.
    /// </summary>
    public class InvitationCodeService : IInvitationCodeService
    {
        private readonly Lazy<ulong?> _code;

        public InvitationCodeService(IEntropyService entropyService)
        {
            _code = new Lazy<ulong?>(() =>
            {
                if (entropyService.Any())
                {
                    var bytes = entropyService.Take(8).ToList();
                    return BitConverter.ToUInt64(bytes.ToArray());
                }
                return null;
            });
        }

        public bool HasCode => _code.Value.HasValue;

        public ulong Code => _code.Value.Value;
    }
}
