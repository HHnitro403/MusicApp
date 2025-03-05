using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Classes
{
    public class PermissionService : IPermissionService
    {
        public async Task<bool> CheckAndRequestPermissionsAgain()
        {
            try
            {
                // List of permissions to check and request
                var permissions = new List<Type>
        {
            typeof(Permissions.StorageRead), // For accessing files
            typeof(Permissions.StorageWrite), // For modifying files
            typeof(Permissions.Media) // For media access (if supported by your platform)
        };

                bool allPermissionsGranted = true;

                foreach (var permissionType in permissions)
                {
                    var status = await RequestPermissionAsync(permissionType);

                    if (status != PermissionStatus.Granted)
                    {
                        allPermissionsGranted = false;
                    }
                }

                return allPermissionsGranted;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Permission error: {ex.Message}");
                return false;
            }
        }

        private async Task<PermissionStatus> RequestPermissionAsync(Type permissionType)
        {
            if (permissionType == typeof(Permissions.StorageRead))
            {
                return await Permissions.RequestAsync<Permissions.StorageRead>();
            }
            else if (permissionType == typeof(Permissions.StorageWrite))
            {
                return await Permissions.RequestAsync<Permissions.StorageWrite>();
            }
            else if (permissionType == typeof(Permissions.Media))
            {
                return await Permissions.RequestAsync<Permissions.Media>();
            }

            throw new InvalidOperationException("Unsupported permission type.");
        }
    }
}