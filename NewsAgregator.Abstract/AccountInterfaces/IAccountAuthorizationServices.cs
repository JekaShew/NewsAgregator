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
        public Task<string> GenerateJwtTokenString(Guid userId);
        public Task<string> GenerateRefreshToken(Guid userId);
        public Task RevokeToken(Guid refreshTokenId);
        Task<bool> RefreshTokenCorrect(Guid tokenId);
        Task RemoveToken(Guid id);

    }
}

