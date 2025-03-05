namespace MusicApp.Classes
{
    public interface IPermissionService
    {
        Task<bool> CheckAndRequestPermissionsAgain();
    }
}