using AutoBogus;
using Domain.Entities;

namespace Tests.Utils.Factories
{
    public static class OperationFactory
    {
        public static Operation GenerateOperation()
        {
            var fakeOperation = AutoFaker.Generate<Operation>();

            return fakeOperation;
        }
    }
}
