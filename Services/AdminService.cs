using AbcBankDalLayer.Models;
using ABCBankWebApi.Sp_Results;
using AutoMapper;
using BankDAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ABCBankWebApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper mapper;

        public AdminService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void AddAdmin(AdminProfile adminProfile)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Name", adminProfile.Name));
            sqlParameters.Add(new SqlParameter("@Dob", adminProfile.Dob));
            sqlParameters.Add(new SqlParameter("@PhoneNumber", adminProfile.PhoneNumber));
            sqlParameters.Add(new SqlParameter("@AdhaarNumber", adminProfile.AdhaarNumber));
            sqlParameters.Add(new SqlParameter("@Email", adminProfile.Email));
            sqlParameters.Add(new SqlParameter("@PanCardNumber", adminProfile.PanCardNumber));
            sqlParameters.Add(new SqlParameter("@Address", adminProfile.Address));
            sqlParameters.Add(new SqlParameter("@CityName", adminProfile.CityName));
            sqlParameters.Add(new SqlParameter("@ZipCode", adminProfile.ZipCode));
            sqlParameters.Add(new SqlParameter("@Gender", adminProfile.Gender));
            using var hmac = new HMACSHA512();
            adminProfile.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminProfile.Password));
            adminProfile.PasswordSalt = hmac.Key;
            sqlParameters.Add(new SqlParameter("@PasswordHash", adminProfile.PasswordHash));
            sqlParameters.Add(new SqlParameter("@PasswordSalt", adminProfile.PasswordSalt));
            StoredProcedure.ExecuteStoredProcedure("Sp_AddAdmin", sqlParameters);
        }

        public AdminProfile AdminLogin(AdminProfile adminProfile)
        {
            var loginResult = StoredProcedure.ExecuteStoredProcedureWithResult<AdminLoginResult>("Sp_AdminLogin", new List<SqlParameter> { new SqlParameter("@Email", adminProfile.Email) }).FirstOrDefault();
            if (loginResult == null)
                throw new UnauthorizedAccessException("Invalid Credentials");
            using var hmac = new HMACSHA512(loginResult.PasswordSalt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(adminProfile.Password));
            for (int i = 0; i < loginResult.PasswordHash.Length; i++)
                if (passwordHash[i] != loginResult.PasswordHash[i])
                    throw new UnauthorizedAccessException("Invalid Credentials");
            var loginEntryResult = StoredProcedure.ExecuteStoredProcedureWithResult<LoginEntryResult>("Sp_AdminLoginEntry", new List<SqlParameter> { new SqlParameter("@AdminId", loginResult.AdminId) }).FirstOrDefault();
            adminProfile = null;
            var mapLogin = mapper.Map(loginResult, adminProfile);
            return mapper.Map(loginEntryResult, mapLogin);
        }

        public void AdminLogout(long longinId)
        {
            StoredProcedure.ExecuteStoredProcedure("Sp_AdminLogoutEntry", new List<SqlParameter> { new SqlParameter("@LoginId", longinId) });
        }

        public AdminProfile GetAdminByAdminId(long longAdminId)
        {
            var adminResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetAdminByAdminIdResult>("Sp_GetAdminByAdminId", new List<SqlParameter> { new SqlParameter("@AdminId", longAdminId) }).FirstOrDefault();
            if (adminResult == null) throw new ArgumentException("Admin Not Found");
            var mapClient = mapper.Map(adminResult, new AdminProfile());
            var adminLastLoginResult = StoredProcedure.ExecuteStoredProcedureWithResult<LastLoginResult>("Sp_AdminLastLoginDate", new List<SqlParameter> { new SqlParameter("@AdminId", longAdminId) }).FirstOrDefault();
            return mapper.Map(adminLastLoginResult, mapClient);
        }

        public void ApproveClient(long longClientId, string stringStatus)
        {
            StoredProcedure.ExecuteStoredProcedure("Sp_ApproveClient", new List<SqlParameter> { new SqlParameter("@ClientId", longClientId), new SqlParameter("@Status", stringStatus) });
        }

        public void ApproveAccount(string stringAccountNumber, string stringStatus, decimal decimalBalance)
        {
            StoredProcedure.ExecuteStoredProcedure("Sp_ApproveAccount", new List<SqlParameter> { new SqlParameter("@AccountNumber", stringAccountNumber), new SqlParameter("@Status", stringStatus), new SqlParameter("@Balance", decimalBalance) });
        }

        public void CloseAccount(string stringAccountNumber)
        {
            StoredProcedure.ExecuteStoredProcedure("Sp_CloseAccount", new List<SqlParameter> { new SqlParameter("@AccountNumber", stringAccountNumber) });
        }
    }
}
