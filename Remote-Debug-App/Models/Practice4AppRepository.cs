namespace Remote_Debug_App.Models
{
    public class Practice4AppRepository
    {
        public static Practice4AppRepository Instance;
        public Practice4AppRepository()
        {
            Instance = new Practice4AppRepository("https://github.com/customaccount/Practice4Repo");
        }
        private Practice4AppRepository(string repository)
        {
            Repository = repository;
        }

        public string Repository ;
    }
}
