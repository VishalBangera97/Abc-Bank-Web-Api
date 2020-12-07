using AbcBankDalLayer.Models;

namespace ABCBankWebApi.Services
{
    public interface IAdminService
    {
        AdminProfile AdminLogin(AdminProfile adminProfile);
        void AdminLogout(long longinId);
        AdminProfile GetAdminByAdminId(long longAdminId);
        void AddAdmin(AdminProfile adminProfile);
        void ApproveClient(long longClientId, string stringStatus);
        void ApproveAccount(string stringAccountNumber, string stringStatus, decimal decimalBalance);
        void CloseAccount(string stringAccountNumber);

    }
}
