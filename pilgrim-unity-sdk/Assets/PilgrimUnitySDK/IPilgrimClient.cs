
namespace Foursquare
{

    public interface IPilgrimClient
    {

        void SetUserInfo(PilgrimUserInfo userInfo);
        
        void Start();

        void Stop();

        void ClearAllData();

    }

}