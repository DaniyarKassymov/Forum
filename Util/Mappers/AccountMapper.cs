using Forum.Models;
using Forum.Util.Services;
using Forum.ViewModels.AccountVms;

namespace Forum.Util.Mappers;

public static class AccountMapper
{
    public static User RegisterVmUser(RegisterVm vm)
    {
        return new User()
        {
            UserName = vm.UserName,
            Email = vm.Email,
            Avatar = FileUpload.Upload(vm.UserName, vm.Avatar)
        };
    }
}