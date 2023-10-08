using System.Windows.Forms;

namespace ExoticClient.Classes
{
    public class UserManager
    {
        public static UserManager Instance { get; private set; }

        private UserDetails _userDetails;

        public UserManager()
        {
            Instance = this;
            _userDetails = new UserDetails();
        }

        public void SetUsername(string username)
        {
            _userDetails.Username = username;
        }

        public void SetUserDetails(UserDetails userDetails)
        {
            _userDetails = userDetails;
        }

        public UserDetails GetUserDetails()
        {
            if (_userDetails == null)
            {
                MessageBox.Show("User details returned is null."); // Log Instead
                return null;
            }

            return _userDetails;
        }
    }
}
