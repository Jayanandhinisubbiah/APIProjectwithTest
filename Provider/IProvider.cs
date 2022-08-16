using APIProject.Models;

namespace APIProject.Provider
{
    public interface IProvider
    {
        public UserList AddNewUser(UserList U);
        public UserList Login(UserList U);
        public List<Food> GetAll();
        public Food GetFoodById(int? id);

        public Cart AddtoCart(Cart C);
        //public Cart AddtoCart(Cart C);
        public List<Cart> GetCartById(int UserId);
        public void ViewCart(int UserId);
        public Cart Delete(int CartId);
        public void DeleteConfirmed(int CartId);
        public void EmptyList(int UserId);
        public List<OrderDetails> OrderDetails();
        public OrderMaster Buy(int UserId);
        //public void Payment(int OrderId, string Type);
        public void Payment(OrderMaster O);
        public OrderMaster Online(int OrderId);
        public void Online(int OrderId,  string BankName, int CardNo, int ccv);
        public OrderMaster Offline(int OrderId);



    }
}
