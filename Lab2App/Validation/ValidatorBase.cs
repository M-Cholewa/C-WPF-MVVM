using System.Collections.Generic;
using System.Linq;

namespace Lab2App.Validation
{
    public abstract class ValidatorBase<T>
    {
        protected readonly List<ISpecification<T>> Specifications = new List<ISpecification<T>>();

        public void Validate(T entity)
        {

            foreach (var spec in Specifications)
            {
                if (!spec.IsSatisfiedBy(entity))
                {
                    throw new ValidationException(spec.ErrorMessage);
                }
            }

        }
    }
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
