using APIProject.Models;

namespace APIProject.Provider
{
    public interface IProvider
    {
        public UserList AddNewUser(UserList U);
        public void Edit(int CartId, Cart C);
        public void EditFood(int Id, Food C);

        public void DeleteCart(int CartId);
        public UserList Login(UserList U);
        public List<Food> GetAll();
        public Food GetFoodById(int? id);

        public Cart AddtoCart(Cart C);
        public List<Cart> GetCartById(int UserId);
        public void ViewCart(int? UserId);
        public Cart Delete(int CartId);
        public void DeleteConfirmed(int CartId);
        public void EmptyList(int UserId);
        public List<OrderDetails> OrderDetails();
        public OrderMaster Buy(int UserId);
        public OrderMaster Payment(int OrderId, string Type);
       
        public void Pay(int OrderId, OrderMaster O);

        public OrderMaster Pay(int OrderId);
        public Food AddNewFood(Food food);

        public Cart GetCartByCartId(int CartId);

        public void DeleteFood(int FoodId);
        public List<UserList> UserDetails();

        public List<Content> GetReportById(int? UserId);
    }
}
