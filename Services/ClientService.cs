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
using Microsoft.AspNetCore.Http;

namespace ABCBankWebApi.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper mapper;

        public ClientService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public void AddClient(ClientProfile clientProfile)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Name", clientProfile.Name));
            sqlParameters.Add(new SqlParameter("@Dob", clientProfile.Dob));
            sqlParameters.Add(new SqlParameter("@PhoneNumber", clientProfile.PhoneNumber));
            sqlParameters.Add(new SqlParameter("@AdhaarNumber", clientProfile.AdhaarNumber));
            sqlParameters.Add(new SqlParameter("@Email", clientProfile.Email));
            sqlParameters.Add(new SqlParameter("@NomineeName", clientProfile.NomineeName));
            sqlParameters.Add(new SqlParameter("@PanCardNumber", clientProfile.PanCardNumber));
            sqlParameters.Add(new SqlParameter("@NomineeRelation", clientProfile.NomineeRelation));
            sqlParameters.Add(new SqlParameter("@NomineeAdhaarCardNumber", clientProfile.NomineeAdhaarNumber));
            sqlParameters.Add(new SqlParameter("@Address", clientProfile.Address));
            sqlParameters.Add(new SqlParameter("@CityName", clientProfile.CityName));
            sqlParameters.Add(new SqlParameter("@ZipCode", clientProfile.ZipCode));
            sqlParameters.Add(new SqlParameter("@Gender", clientProfile.Gender));
            using var hmac = new HMACSHA512();
            clientProfile.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(clientProfile.Password));
            clientProfile.PasswordSalt = hmac.Key;
            sqlParameters.Add(new SqlParameter("@PasswordHash", clientProfile.PasswordHash));
            sqlParameters.Add(new SqlParameter("@PasswordSalt", clientProfile.PasswordSalt));
            StoredProcedure.ExecuteStoredProcedure("Sp_AddClient", sqlParameters);
        }
        public ClientProfile ClientLogin(ClientProfile clientProfile)
        {
            var loginResult = StoredProcedure.ExecuteStoredProcedureWithResult<ClientLoginResult>("Sp_ClientLogin", new List<SqlParameter> { new SqlParameter("@Email", clientProfile.Email) }).FirstOrDefault();
            if (loginResult == null)
                throw new UnauthorizedAccessException("Invalid Credentials");
            using var hmac = new HMACSHA512(loginResult.PasswordSalt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(clientProfile.Password));
            for (int i = 0; i < loginResult.PasswordHash.Length; i++)
                if (passwordHash[i] != loginResult.PasswordHash[i])
                    throw new UnauthorizedAccessException("Invalid Credentials");
            var loginEntryResult = StoredProcedure.ExecuteStoredProcedureWithResult<LoginEntryResult>("Sp_ClientLoginEntry", new List<SqlParameter> { new SqlParameter("@ClientId", loginResult.ClientId) }).FirstOrDefault();
            clientProfile = null;
            var mapLogin = mapper.Map(loginResult, clientProfile);
            return mapper.Map(loginEntryResult, mapLogin);
        }

        public void ClientLogout(long longinId)
        {
            StoredProcedure.ExecuteStoredProcedure("Sp_ClientLogoutEntry", new List<SqlParameter> { new SqlParameter("@LoginId", longinId) });
        }

        public ClientProfile GetClientByClientId(long longClientId)
        {
            var clientResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetClientByClientIdResult>("Sp_GetClientByClientId", new List<SqlParameter> { new SqlParameter("@ClientId", longClientId) }).FirstOrDefault();
            if (clientResult == null) return null;
            var mapClient = mapper.Map(clientResult, new ClientProfile());
            var clientLastLoginResult = StoredProcedure.ExecuteStoredProcedureWithResult<LastLoginResult>("Sp_ClientLastLoginDate", new List<SqlParameter> { new SqlParameter("@ClientId", longClientId) }).FirstOrDefault();
            return mapper.Map(clientLastLoginResult, mapClient);
        }

        public IEnumerable<ClientProfile> GetClientsBasedOnStatus(string stringStatus)
        {
            var clientBasedOnStatusResult = StoredProcedure.ExecuteStoredProcedureWithResult<GetClientBasedOnClientStatusResult>("Sp_GetClientsBasedOnStatus", new List<SqlParameter> { new SqlParameter("@ClientStatus", stringStatus) });
            return (clientBasedOnStatusResult == null) ? null : mapper.Map<IEnumerable<ClientProfile>>(clientBasedOnStatusResult);
            
        }

        public void ChangePhoneNumber(long longClientId,string stringPhoneNumber)
        {
            StoredProcedure.ExecuteStoredProcedure("Sp_ChangeClientPhoneNumber", new List<SqlParameter> { new SqlParameter("@ClientId", longClientId), new SqlParameter("@PhoneNumber", stringPhoneNumber) });
        }


    }
}
