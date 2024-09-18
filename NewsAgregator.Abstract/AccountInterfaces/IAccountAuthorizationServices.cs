using Microsoft.AspNetCore.Mvc;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAgregator.Abstract.AccountInterfaces
{
    public interface IAccountAuthorizationServices
    {
        public Task<string> GenerateJwtTokenString(Guid accountId);
        public Task<string> GenerateRefreshToken(Guid accountId);
        public Task RevokeTokenByIdAsync(Guid refreshTokenId);
        Task<bool> RefreshTokenCorrect(Guid refreshTokenId);
        Task RemoveRTokenByIdAsync(Guid refreshTokenId);

    }
}

