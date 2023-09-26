using ShoeApi.Models;

namespace ShoeApi.Repositories.Interfaces;
public interface IShoeRepository
{
    List<Shoe> GetShoes();

    bool DeleteShoe(int id);
}