namespace HubOfChess.Application.Common.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(
            string existingName, 
            string fromName, object fromKey, 
            string toName, object toKey)
            : base($"Entity \"{existingName}\" " +
                  $"from entity \"{fromName}\" ({fromKey}) " +
                  $"to \"{toName}\" ({toKey}) is already exist.") { }
    }
}
