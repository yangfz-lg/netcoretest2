namespace _14_cacle01
{
    public class BookService
    {
        public static Book? GetBookById(long id)
        {
            if (id == 1111111111)
            {
                return new Book(1111111111, "eb青年是怎样形成的");
            }
            if (id == 2222222222)
            {
                return new Book(2222222222, "eb青年是这样形成的");
            }
            return null;

        }

        public static Task<Book?>  GetBookByIdAsync(long id)
        {
            var result = GetBookById(id);
            return Task.FromResult(result);
        }
    }
}
