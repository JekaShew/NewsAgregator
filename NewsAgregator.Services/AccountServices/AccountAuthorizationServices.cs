using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.Mapper.DataMappers;
using NewsAgregator.ViewModels.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NewsAgregator.Services.AccountServices
{
    public class AccountAuthorizationServices : IAccountAuthorizationServices
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountServices _accountServices;
        private readonly IRoleServices _roleServices;
        private readonly IPolicyServices _policyServices;
        private readonly AppDBContext _appDBContext;

        public AccountAuthorizationServices(AppDBContext appDBContext, IConfiguration configuration, IAccountServices accountServices, IRoleServices roleServices, IPolicyServices policyServices)
        {
            _configuration = configuration;
            _accountServices = accountServices;
            _roleServices = roleServices;
            _policyServices = policyServices;
            _appDBContext = appDBContext;
        }

        public Task<IActionResult> SignOutAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateJwtTokenString(Guid accountId)
        {

            var accountVM = await _accountServices.TakeAccountByIdAsync(accountId);
            
            var roleVM = await _roleServices.TakeRoleByIdAsync(accountVM.RoleId.Value);
            var rolePolicyVMs = roleVM.RolePolicies.Select( rp => rp.PolicyId).ToList();
            var policies1 = new List<string>();
            foreach(var rpvmID in rolePolicyVMs)
            {
                var pId = rpvmID.Value;
                policies1.Add((await _policyServices.TakePolicyByIdAsync(rpvmID.Value)).Title);
            }

            //string?[] policies = (await _roleServices.TakeRoleByIdAsync(accountVM.RoleId.Value))
            //                        .RolePolicies.Select(rp => rp.Policy.Text)
            //                        .ToArray();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, accountVM.Email),
                new Claim(ClaimTypes.Name, accountVM.FIO),
                new Claim(ClaimTypes.NameIdentifier, accountVM.Id.ToString()),
                new Claim(ClaimTypes.Role, accountVM.Role.Text),
                new Claim("RolePolicies", string.Join(";",policies1)),
            };

          
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["JWT:Iss"],
                Audience = _configuration["JWT:Aud"],
                Expires = DateTime.UtcNow.AddMinutes(12),
                SigningCredentials =
                    new SigningCredentials(key,
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = jwtHandler.CreateToken(tokenDescriptor);
            var tokenString = jwtHandler.WriteToken(jwtToken);
            return tokenString;
        }

        public async Task<string> GenerateRefreshToken(Guid accountId)
        {
            var refreshToken = new RefreshToken()
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                IsRevoked = false,
                ExpireDateTime = DateTime.UtcNow.AddMinutes(120),
            };
            await _appDBContext.RefreshTokens.AddAsync(refreshToken);
            await _appDBContext.SaveChangesAsync();
            return refreshToken.Id.ToString("D");
        }

        public async Task<RefreshToken> TakeRefreshTokenByIdAsync(Guid refreshTokenId)
        {
            return await _appDBContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == refreshTokenId);
        }   

        public async Task RevokeTokenByIdAsync(Guid refreshTokenId)
        {
            var rToken = await TakeRefreshTokenByIdAsync(refreshTokenId);
            rToken.IsRevoked = true;
            await _appDBContext.SaveChangesAsync();
        }

        public async Task RemoveRTokenByIdAsync(Guid refreshTokenId)
        {
            var rToken = await _appDBContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == refreshTokenId);
            _appDBContext.RefreshTokens.Remove(rToken);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<bool> RefreshTokenCorrect(Guid refreshTokenId)
        {
            var rToken = await TakeRefreshTokenByIdAsync(refreshTokenId); 
            return rToken
                is { IsRevoked: false }
                   && (rToken.ExpireDateTime <= DateTime.UtcNow || rToken.ExpireDateTime == null);
        }
    }
}
