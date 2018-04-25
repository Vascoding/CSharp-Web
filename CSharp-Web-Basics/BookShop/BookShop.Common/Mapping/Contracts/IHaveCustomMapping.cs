using AutoMapper;

namespace BookShop.Common.Mapping.Contracts
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}
