using System;

namespace Lab2App.Validation
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
        string ErrorMessage { get; }
    }
}
