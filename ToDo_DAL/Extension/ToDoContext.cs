namespace ToDo_DAL
{
    public partial class ToDoContext
    {
        public static ToDoContext Create()
        {
            return new ToDoContext();
        }
    }
}
