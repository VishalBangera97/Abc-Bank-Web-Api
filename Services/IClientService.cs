using AbcBankDalLayer.Models;
using System.Collections.Generic;

namespace ABCBankWebApi.Services
{
    public interface IClientService
    {
        void AddClient(ClientProfile clientProfile);
        ClientProfile GetClientByClientId(long longClientId);
        ClientProfile ClientLogin(ClientProfile clientProfile);
        void ClientLogout(long longinId);
        IEnumerable<ClientProfile> GetClientsBasedOnStatus(string stringStatus);

        void ChangePhoneNumber(long longClientId, string stringPhoneNumber);




    }
}
