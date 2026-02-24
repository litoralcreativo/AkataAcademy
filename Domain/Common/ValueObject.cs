namespace AkataAcademy.Domain.Common
{
    public interface IValueObject<TSelf, TValue>
    where TSelf : IValueObject<TSelf, TValue>
    {
        static abstract TSelf From(TValue value);
    }
}
