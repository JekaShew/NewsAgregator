using AutoMapper;
using Mapper.Mappers.PropertiesMappers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NewsAgregator.Abstract.AccountInterfaces;
using NewsAgregator.Data;
using NewsAgregator.Data.Models;
using NewsAgregator.Mapper.DataMappers;
using NewsAgregator.ViewModels.Additional;
using NewsAgregator.ViewModels.Data;
using System; 
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text; 
using System.Threading.Tasks;

namespace NewsAgregator.Services.AccountServices
{
    public class AccountServices : IAccountServices
    {
        private readonly AppDBContext _appDBContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly byte[] bytesKey = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10 };
        private readonly byte[] bytesIV = new byte[] { 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20 };

        //private readonly byte[] Key = Encoding.UTF8.GetBytes("NewsAggregator_KEY");
        //private readonly byte[] IV = Encoding.UTF8.GetBytes("NewsAggregator_IV");
        public AccountServices(AppDBContext appDBContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _appDBContext = appDBContext;
        }

        public async Task<AccountParameters> GetAccountParametersAsync()
        {
            var accountParameters = new AccountParameters()
            {
                AccountStatuses = (await _appDBContext.AccountStatuses.AsNoTracking().ToListAsync()).Select(ac => AccountParametersMapper.AccountStatusToParameter(ac)).ToList(),
                Roles = (await _appDBContext.Roles.AsNoTracking().ToListAsync()).Select(r => AccountParametersMapper.RoleToParameter(r)).ToList(),
            };
            return accountParameters;
        }

        public async Task AddAccountAsync(CreateAccountVM createAccountVM)
        {
            var newAccount = AccountMapper.CreateAccountVMToAccount(createAccountVM);

            newAccount.Id = Guid.NewGuid();
            newAccount.SecretWord = AESEncrypt(await GetHashAsync(createAccountVM.SecretWord), bytesKey, bytesIV);
            newAccount.SecurityStamp = await GetHashAsync(createAccountVM.SecretWord);
            newAccount.PasswordHash = await GetPasswordHashAsync(createAccountVM.Password, newAccount.SecurityStamp);
            newAccount.AccountStatusId = await _appDBContext.AccountStatuses.AsNoTracking().Where(accs => accs.Title.Equals("Disabled")).Select(accs => accs.Id).FirstOrDefaultAsync();
            newAccount.RoleId = await _appDBContext.Roles.AsNoTracking().Where(r => r.Title.Equals("User")).Select(r => r.Id).FirstOrDefaultAsync();

            await _appDBContext.Accounts.AddAsync(newAccount);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            _appDBContext.Accounts.Remove(await _appDBContext.Accounts.FirstOrDefaultAsync(acc => acc.Id == id));
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<AccountVM> TakeAccountByIdAsync(Guid id)
        {
            var account = AccountMapper.AccountToAccountVM(await _appDBContext.Accounts
                .AsNoTracking()
                .Include(acc => acc.AccountStatus)
                .Include(acc => acc.Role)
                    .ThenInclude(rp => rp.RolePolicies)
                        .ThenInclude(p => p.Policy)
                .FirstOrDefaultAsync(acc => acc.Id == id));

            var accountParameters = await GetAccountParametersAsync();
            account.AccountStatuses = accountParameters.AccountStatuses;
            account.Roles = accountParameters.Roles;

            return account;
        }

        public async Task<List<AccountVM>> TakeAccountsAsync()
        {
            var accountVMs = (await _appDBContext.Accounts
                .AsNoTracking()
                .Include(acc => acc.AccountStatus)
                .Include(acc => acc.Role)
                .ToListAsync())
                    .Select(a => AccountMapper.AccountToAccountVM(a)).ToList();

            return accountVMs;
        }

        public async Task UpdateAccountAsync(AccountVM updatedAccountVM)
        {
            var account = await _appDBContext.Accounts.FirstOrDefaultAsync(pc => pc.Id == updatedAccountVM.Id);
            //account = AccountMapper.AccountVMToAccount(updatedAccountVM);

            await _appDBContext.SaveChangesAsync();
            if (account != null)
            {
                account.UserName = updatedAccountVM.UserName;
                account.Login = updatedAccountVM.Login;
                account.FIO = updatedAccountVM.FIO;
                account.Email = updatedAccountVM.Email;
                account.Phone = updatedAccountVM.Phone;
                account.DesiredNewsRating = updatedAccountVM.DesiredNewsRating;
                if(updatedAccountVM.AccountStatusId == null)
                {
                    if(account.AccountStatusId == null)
                    {
                        account.AccountStatusId = await _appDBContext.AccountStatuses.AsNoTracking().Where(accs => accs.Title.Equals("Disabled")).Select(accs => accs.Id).FirstOrDefaultAsync();
                    }
                }
                else
                    account.AccountStatusId = updatedAccountVM.AccountStatusId;
                if (updatedAccountVM.RoleId == null)
                {
                    if (account.RoleId == null)
                    {
                        account.RoleId = await _appDBContext.Roles.AsNoTracking().Where(r => r.Title.Equals("User")).Select(r => r.Id).FirstOrDefaultAsync();
                    }
                }
                else
                    account.RoleId = updatedAccountVM.RoleId;

                await _appDBContext.SaveChangesAsync();
            }
        }
        private async Task<string> GetPasswordHashAsync(string password, string securityStamp)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes($"{password}{securityStamp}");
                var ms = new MemoryStream(inputBytes);
                var hashBytes = await md5.ComputeHashAsync(ms);
                var passwordHash = Encoding.UTF8.GetString(hashBytes);
                return passwordHash;
            }
        }

        private async Task<string> GetHashAsync(string stringToHash)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes($"{stringToHash}");
                var ms = new MemoryStream(inputBytes);
                var hashBytes = await md5.ComputeHashAsync(ms);
                var passwordHash = Encoding.UTF8.GetString(hashBytes);
                return passwordHash;
            }
        }

        public async Task<bool> CheckIsLoginRegisteredAsync(string login)
        {
            return await _appDBContext.Accounts.AnyAsync(a => a.Login.Equals(login));
        }

        public async Task<bool> CheckLoginPasswordAsync(string login, string password)
        {
            if (await CheckIsLoginRegisteredAsync(login))
            {
                var account = await _appDBContext.Accounts.Where(a => a.Login.Equals(login)).FirstOrDefaultAsync();
                var enteredPasswordHash = await GetPasswordHashAsync(password, account.SecurityStamp);

                return await _appDBContext.Accounts.AnyAsync(a => a.Login.Equals(account.Login) && a.PasswordHash.Equals(enteredPasswordHash));
            }
            else
                return false;
            
        }

        public async Task<bool> CanChangePasswordAsync(string login, string password)
        {
            if(await CheckIsLoginRegisteredAsync(login))
            {
                return await CheckLoginPasswordAsync(login, password);
            }
            else 
                return false;
        }

        public async Task<bool> CanChangeForgottenPasswordAsync(string login, string secretWord)
        {
            if (await CheckIsLoginRegisteredAsync(login))
            {
                var account = await _appDBContext.Accounts.FirstOrDefaultAsync(a => a.Login.Equals(login));

                if (GetHashAsync(secretWord).Equals(AESDecrypt(Encoding.UTF8.GetBytes(account.SecretWord), bytesKey, bytesIV)))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task ChangePasswordAsync(string password)
        {
            var accoutId = GetCurrentAccountId();
            var account = await _appDBContext.Accounts.FirstOrDefaultAsync(a => a.Id == accoutId);
            account.PasswordHash = await GetPasswordHashAsync(password, account.SecurityStamp);

            await _appDBContext.SaveChangesAsync();
        }

        
        public Guid? GetCurrentAccountId()
        {
            if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return null;

            var claim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            return Guid.Parse(claim.Value);
        }


        private string AESEncrypt(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Encoding.UTF8.GetString(encrypted);
        }

        private string AESDecrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException(nameof(Key));
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException(nameof(IV));

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public async Task<Guid?> TakeAccountIdByLoginAsync(string login)
        {
           return await _appDBContext.Accounts.Where(a => a.Login.Equals(login)).Select(a => a.Id).FirstOrDefaultAsync();
        }

        public async Task<AccountVM> TakeAccountByRTokenIdAsync(Guid refreshTokenId)
        {
            var userId = await _appDBContext.RefreshTokens.Where(rt => rt.Id == refreshTokenId).Select(rt => rt.AccountId).FirstOrDefaultAsync();            
            return await TakeAccountByIdAsync(userId);
        }
    }
}


