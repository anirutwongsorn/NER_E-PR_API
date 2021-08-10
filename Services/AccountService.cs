using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ner_api.Data;
using ner_pr_api.Dtos.InputDtos;
using ner_pr_api.Interfaces;
using static ner_pr_api.Installer.JWTInstaller;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ner_pr_api.Services
{
    public class AccountService : IaccountService
    {
        private readonly DatabaseContext databaseContext;
        private readonly JwtSettings jwtSettings;
        public AccountService(DatabaseContext databaseContext, JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
            this.databaseContext = databaseContext;
        }

        public async Task Register(RegisterRequest account)
        {
            var existingAccount = await databaseContext.TbUsers.SingleOrDefaultAsync(a => a.Email == account.Username);
            if (existingAccount == null)
            {
                throw new Exception("Account not exist!");
            }
            account.Password = CreatePasswordHash(account.Password);
            existingAccount.EncryptPass = account.Password;
            await databaseContext.SaveChangesAsync();
        }

        public async Task<Account> Login(string username, string password)
        {
            var account = await databaseContext.TbUsers.SingleOrDefaultAsync(a => a.Email == username);
            if (account != null && VerifyPassword(account.EncryptPass, password))
            {
                var _acc = Account.FromTbUser(account);
                return Account.FromTbUser(account);
            }
            return null;
        }

        private string CreatePasswordHash(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 258 / 8
            ));
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        private bool VerifyPassword(string hash, string password)
        {
            var parts = hash.Split('.', 2);
            if (parts.Length != 2)
            {
                return false;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var passwordHash = parts[1];
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
              password: password,
              salt: salt,
              prf: KeyDerivationPrf.HMACSHA512,
              iterationCount: 10000,
              numBytesRequested: 258 / 8
            ));

            return passwordHash == hashed;
        }

        public string GenerateToken(Account account)
        {
            // key is case-sensitive
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, account.UserID),
                new Claim("role", account.FullName),
                 new Claim("expire", DateTime.Now.AddDays(1).ToString()),
            };

            return BuildToken(claims);
        }

        public Account GetInfo(string accessToken)
        {
            var token = new JwtSecurityTokenHandler().ReadToken(accessToken) as JwtSecurityToken;

            // key is case-sensitive
            var userId = token.Claims.First(claim => claim.Type == "sub").Value;
            var role = token.Claims.First(claim => claim.Type == "role").Value;
            var expire = token.Claims.First(claim => claim.Type == "expire").Value;
            var _expireDate = DateTime.Parse(expire);
            if (_expireDate < DateTime.Now)
            {
                return null;
            }

            var account = new Account
            {
                UserID = userId,
                FullName = role,
                expireDate = _expireDate
            };

            return account;
        }

        private string BuildToken(Claim[] claims)
        {
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtSettings.Expire));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}