using APIProject.Data;
using APIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProject.Provider
{
    public class FoodProvider : IProvider
    {
       

        private readonly FoodContext fd;
        public FoodProvider(FoodContext fd)
        {
            this.fd=fd;
        }

        public UserList AddNewUser(UserList U)
        {
            fd.UserList.Add(U);
            fd.SaveChanges();
            return U;
        }
        #region
        //public Cart AddtoCart(int Qnt, int FoodId, int UserId)
        //{
        //    #region
        //    //var F = fd.Food.FirstOrDefault(i => i.FoodId == C.FoodId);
        //    //var  U= fd.UserList.FirstOrDefault(i => i.UserId == C.UserId);
        //    #endregion
        //    Cart C = new Cart();
        //    var F = fd.Food.FirstOrDefault(i => i.FoodId == FoodId);
        //    var U = fd.UserList.FirstOrDefault(i => i.UserId == UserId);

        //    C.UserId =UserId;
        //    C.FoodId = F.FoodId;
        //    C.Qnt = Qnt;
        //    fd.Add(C);
        //    fd.SaveChanges();
        //    return C;
        //}

        public Cart AddtoCart(Cart C)
        {
            Cart T = new Cart();
            var F = fd.Food.FirstOrDefault(i => i.FoodId == C.FoodId);
            var U = fd.UserList.FirstOrDefault(i => i.UserId == C.UserId);
            T.UserId = U.UserId;
            T.FoodId = F.FoodId;
            T.Qnt = C.Qnt;
            fd.Add(T);
            fd.SaveChanges();
            return T;
        }
        #endregion

        public OrderMaster Buy(int UserId)
        {
            return (fd.OrderMaster.FirstOrDefault(m => m.UserId == UserId));
            // List<OrderMaster> o = (from i in fd.OrderMaster
            //         where i.UserId == UserId
            //         select i).ToList();
            //return o;
        }

        public Cart Delete(int CartId)
        {
            return  (fd.Cart.FirstOrDefault(m => m.CartId == CartId));
            
        }

        public void DeleteConfirmed(int CartId)
        {
            //var val = ;
            fd.Cart.Remove(fd.Cart.Find(CartId));
            fd.SaveChanges();
        }

        public void EmptyList(int UserId)
        {
            List<Cart> list = (from i in fd.Cart
                               where i.UserId == UserId
                               select i).ToList();
            foreach (var item in list)
            {
                var val = fd.Cart.Find(item.CartId);
                fd.Cart.Remove(val);
                fd.SaveChanges();
            }
        }

        public List<Food> GetAll()
        {
            return fd.Food.ToList();
        }

        public List<Cart> GetCartById(int UserId)
        {
            return (from i in fd.Cart.Include(x => x.Food)
                    where i.UserId == UserId
                    select i).ToList();


        }

        public Food GetFoodById(int? id)
        {
            return (fd.Food.Find(id));
        }

        public UserList Login(UserList U)
        {
            var result = (from i in fd.UserList
                          where i.FName == U.FName && i.Password == U.Password && i.Role == U.Role
                          select i).SingleOrDefault();

            return result;

        }
    

        public OrderMaster Offline(int OrderId)
        {
             var result = fd.OrderMaster.SingleOrDefault(m => m.OrderId == OrderId);
            return result;
        }

        public OrderMaster Online(int OrderId)
        {
            var result = fd.OrderMaster.SingleOrDefault(m => m.OrderId == OrderId);
            return result;
        }

        public void Online(int OrderId, string BankName, int CardNo, int ccv)
        {
            var result = fd.OrderMaster.SingleOrDefault(m => m.OrderId == OrderId);
            result.BankName = BankName;
            result.CardNo = CardNo;
            result.CCV = ccv;
            fd.SaveChanges();
        }

        public List<OrderDetails> OrderDetails()
        {
           var C=fd.OrderDetails.ToList();
            return C;
        }

        //public void Payment(int OrderId, string Type)
        //{
        //    var result = fd.OrderMaster.SingleOrDefault(m => m.OrderId == OrderId);
        //    result.Type = Type;
        //    fd.SaveChanges();
        //}
        public void Payment(OrderMaster O)
        {
            var result = fd.OrderMaster.SingleOrDefault(m => m.OrderId == O.OrderId);
            result.Type = O.Type;
            fd.SaveChanges();
        }

        public void ViewCart(int? UserId)
        {
            var c = (from i in fd.Cart
                     where i.UserId == UserId
                     select i.FoodId).SingleOrDefault();

            var F = fd.Food.FirstOrDefault(i => i.FoodId == c);
            List<Cart> list = (from i in fd.Cart
                               where i.UserId == UserId
                               select i).ToList();
            OrderMaster orderMaster = new OrderMaster();
            orderMaster.UserId = UserId;
            fd.Add(orderMaster);
            fd.SaveChanges();
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            foreach (var item in list)
            {
                OrderDetails od = new OrderDetails();
                od.OrderId = orderMaster.OrderId;
                od.FoodId = item.FoodId;
                od.Qnt = item.Qnt;
                od.Price = F.price;
                od.TotalPrice = od.Qnt * od.Price;
                fd.OrderDetails.Add(od);
                fd.SaveChanges();

            }
            orderDetails.AddRange(fd.OrderDetails);
            orderMaster.TotalPrice = orderDetails.Sum(i => i.TotalPrice);
            fd.SaveChanges();
            
        }
        public Food AddNewFood(Food food)
        {
            fd.Add(food);
            fd.SaveChanges();
            return food;
        }



    }
}
