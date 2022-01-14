using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bogus;

namespace CA.RemitHub.Domain.Test.Fakes
{
    public abstract class FakerBase<TEntity> where TEntity : class
    {
        private static IEnumerable<TEntity> _fakeData;
        private static object _locker = new();
        private static Faker<TEntity> _faker = new Faker<TEntity>();
        private static float _nullPercent = 0.05f;
        public static Faker<TEntity> Faker { get { return _faker; } }
        public static float NullPercent { get { return _nullPercent; } }

        public virtual IEnumerable<TEntity> GenerateRandomData(int records = 100000)
        {
            if (_fakeData == null)
            {
                lock (_locker)
                {
                    if (_fakeData == null)
                    {
                        _fakeData = _faker.Generate(records);
                    }
                }
            }
            return _fakeData;
        }

        public void AddRule<TProperty>(Expression<Func<TEntity, TProperty>> property, Func<Faker, TEntity, TProperty> setter)
        {
            _faker.RuleFor(property, setter);
        }
    }
}
