namespace ExoticClient.Classes
{
    public class UserManager
    {
        public static UserManager Instance { get; private set; }

        private User _currentUser;

        public UserManager()
        {
            if (Instance == null)
                Instance = this;

            _currentUser = new User();
        }

        public User CurrentUser { get { return _currentUser; } }
    }
}
