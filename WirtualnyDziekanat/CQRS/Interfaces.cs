namespace VirtualDeansOffice.CQRS
{
    // Command - changes data in the database, returns nothing
    public interface ICommandHandler<TCommand>
    {
        void Handle(TCommand command);
    }

    // Query - only reads data, changes nothing
    public interface IQueryHandler<TQuery, TResult>
    {
        TResult Handle(TQuery query);
    }
}
