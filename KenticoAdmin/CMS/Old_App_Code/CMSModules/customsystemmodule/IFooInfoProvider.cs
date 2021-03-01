using CMS.DataEngine;

namespace Test
{
    /// <summary>
    /// Declares members for <see cref="FooInfo"/> management.
    /// </summary>
    public partial interface IFooInfoProvider : IInfoProvider<FooInfo>, IInfoByIdProvider<FooInfo>, IInfoByNameProvider<FooInfo>
    {
    }
}